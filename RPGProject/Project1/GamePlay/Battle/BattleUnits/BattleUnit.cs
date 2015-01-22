using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace RPGProject.GamePlay.Battle.BattleUnits {
	class BattleUnit {
		public struct Status{
			public readonly int MHP;
			public readonly int MTP;
			public readonly int 物理攻撃;
			public readonly int 物理防御;
			public readonly int 術式攻撃;
			public readonly int 術式防御;
			public readonly int 行動速度;
			public readonly int 会心補正;
			public readonly int 物理命中;
			public readonly int 物理回避;
			public readonly int 変化命中;
			public readonly int 変化回避;

			public Status(int MHP, int MTP, int 物理攻撃, int 物理防御, int 術式攻撃, int 術式防御, int 行動速度, 
				int 会心補正, int 物理命中, int 物理回避, int 変化命中, int 変化回避){
				this.MHP = MHP;
				this.MTP = MTP;
				this.物理攻撃 = 物理攻撃;
				this.物理防御 = 物理防御;
				this.術式攻撃 = 術式攻撃;
				this.術式防御 = 術式防御;
				this.行動速度 = 行動速度;
				this.会心補正 = 会心補正;
				this.物理命中 = 物理命中;
				this.物理回避 = 物理回避;
				this.変化命中 = 変化命中;
				this.変化回避 = 変化回避;				
			}
		}

		public enum Category{
			物理,
			術式
		}
		public enum Element{
			無,
			炎,
			氷,
			雷
		}

		private int tp;
		private int hp;

		public string Name{get; set;}
		public int HP{
			get{return hp;}
			set{
				if(value == 0) isDead = true;
				hp = value;
			}
		}
		public int TP{
			get{return tp;}
			set{
				tp = value;
				if(tp < 0) tp = 0;
				if(tp > status.MTP) tp = status.MTP;
			}
		}
		public Status status{get; private set;}
		public bool isDead{get; private set;}
		private List<BattleUnitBuff> buff;
		public List<BattleUnitSupport> Support{get; set;}
		public readonly ReadOnlyCollection<string> Skills;
		public BattleBadStatus BadStatus{get; private set;}
		public int random;

		public BattleUnit(string name, Status status, string[] skills){
			this.status = status;
			Name = name;
			HP = status.MHP;
			TP = status.MTP;
			buff = new List<BattleUnitBuff>(0);
			Support = new List<BattleUnitSupport>(0);
			this.Skills = new ReadOnlyCollection<string>(skills);
			random = DxLibDLL.DX.GetRand(100);
		}

		public bool Damage(int damage){
			HP -= damage;
			if(HP <= 0){
				HP = 0;
				isDead = true;
				return true;
			}
			return false;
		}
		public void HealHP(int hp){
			HP += hp;
			if(HP >= status.MHP){
				HP = status.MHP;
			}
		}

		public bool isAbleToAction(out string message){
			message = null;
			bool result = true;

			if(this.isDead){
				result = false;
			}
			if(this.BadStatus != null){
				switch(this.BadStatus.type){
				case BattleBadStatus.Type.麻痺:
					if(random < 60){
						message = this.Name + "は麻痺して動けない";
						result = false;
					}
					break;
				case BattleBadStatus.Type.睡眠:
					message = this.Name + "は眠っている";
					result = false;
					break;
				case BattleBadStatus.Type.混乱:
				case BattleBadStatus.Type.石化:
					result = false;
					break;
				}
			}

			return result;
		}
		public bool isAbleToMakeOrder(){
			if(this.isDead){
				return false;
			}
			if(this.BadStatus != null){
				switch(this.BadStatus.type){
				case BattleBadStatus.Type.睡眠:
				case BattleBadStatus.Type.混乱:
				case BattleBadStatus.Type.石化:
					return false;
				}
			}

			return true;
		}

		public void NextTurn(){
			foreach(var b in buff){
				b.NextTurn();
			}
			foreach(var s in Support){
				s.NextTurn();
			}

			buff.RemoveAll((b)=>{return b.isEnd();});
			Support.RemoveAll((s)=>{return s.isEnd();});

			if(BadStatus != null){
				bool end = BadStatus.NextTurn();
				if(end){
					BadStatus = null;
				}
			}

			if(BadStatus != null && BadStatus.type == BattleBadStatus.Type.炎上){
				int fireDamage = (int)(status.MHP*0.3);
				Damage(fireDamage);
				Battle.viewEffect.Enqueue(new BattleViewEffects.BattleViewEffect(Name + "は炎で" + fireDamage + "ダメージを受けた"));
			}
			if(BadStatus != null && BadStatus.type == BattleBadStatus.Type.猛毒){
				int psnDamage = (int)(status.MHP*0.6);
				Damage(psnDamage);
				Battle.viewEffect.Enqueue(new BattleViewEffects.BattleViewEffect(Name + "は毒に侵され" + psnDamage + "ダメージを受けた"));
			}

			random = DxLibDLL.DX.GetRand(100);
		}

		public bool SetBadStatus(BattleBadStatus.Type bs){
			if(BadStatus != null){
				if(!BadStatus.JudgeOverWrited(bs)){
					return false;
				}
			}

			BadStatus = new BattleBadStatus(bs);
			return true;
		}

		public bool Kill(){
			if(this.isDead){
				return false;
			}

			HP = 0;
			return true;
		}

		public string[][] GetSupportEffect(BattleUnitSupport.Timing t, BattleUnit actor, Dictionary<string,string> status){
			List<string[]> eft = new List<string[]>(0);
			foreach(var s in Support){
				var result = s.Notice(t, this, actor, status);
				if(result != null){
					eft.AddRange(result);
				}
			}

			return eft.ToArray(); 
		}

		public void AddBuffEffect(BattleUnitBuff add){
			foreach(var b in buff){
				if(b.Name == add.Name){
					b.AddTurn(add.maxTurn);
					return;
				}
			}
			buff.Add(add);
		}
		public BattleUnitBuff[] GetBuffEffect(){
			return buff.ToArray();
		}
	}
}
