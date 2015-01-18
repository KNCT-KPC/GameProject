using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

		public string Name{get; set;}
		public int HP{get; set;}
		public int TP{get; set;}
		public Status status{get; private set;}
		public bool isDead{get; private set;}
		public List<BattleUnitBuff> Buff{get; set;}
		public List<BattleUnitSupport> Support{get; set;}

		public BattleUnit(string name, Status status){
			Name = name;
			HP = status.MHP;
			TP = status.MTP;
			this.status = status;
			Buff = new List<BattleUnitBuff>(0);
			Support = new List<BattleUnitSupport>(0);
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
		public bool isAbleToAction(){
			return !this.isDead;
		}

		public void NextTurn(){
			foreach(var b in Buff){
				b.NextTurn();
			}
			foreach(var s in Support){
				s.NextTurn();
			}

			Buff.RemoveAll((b)=>{return b.isEnd();});
			Support.RemoveAll((s)=>{return s.isEnd();});
		}

		public string[][] GetSupportEffect(BattleUnitSupport.Timing t, BattleUnit actor, string[][] status){
			List<string[]> eft = new List<string[]>(0);
			foreach(var s in Support){
				var result = s.Notice(t, this, actor, status);
				if(result != null){
					eft.AddRange(result);
				}
			}

			return eft.ToArray();
		}

		public int GetBuffEffect(){
			return 0;
		}
	}
}
