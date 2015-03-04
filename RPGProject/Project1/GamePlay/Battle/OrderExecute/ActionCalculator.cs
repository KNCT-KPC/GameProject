using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPGProject.GamePlay.Battle.BattleUnits;
using RPGProject.GamePlay.Battle.BattleViewEffects;

namespace RPGProject.GamePlay.Battle.OrderExecute {
	static class AttackCalculator{
		static public int CalcDamageSwing(int value){
			int swing = DxLibDLL.DX.GetRand(8) - 4;
			value += (int)(value*swing*0.01);

			return value;
		}

		/// <summary>
		/// 攻撃アクションのデータを表すクラス
		/// </summary>
		public class AttackData{
			public readonly int power;	//威力
			public readonly int hit;	//命中率
			public readonly BattleUnit.Category ctg;	//カテゴリ
			public readonly BattleUnit.Element elm;		//属性

			/// <summary>
			/// コンストラクタ
			/// </summary>
			public AttackData(string[] line){
				string ctgStr = line[1];
				string elmStr = line[2];
				power = int.Parse(line[3]);
				hit = int.Parse(line[4]);

				if(ctgStr == "物理"){
					ctg = BattleUnit.Category.物理;
				} else {
					ctg = BattleUnit.Category.術式;
				}

				elm = BattleUnit.Element.無;
				switch(elmStr){
				case "炎" : elm = BattleUnit.Element.炎; break;
				case "氷" : elm = BattleUnit.Element.氷; break;
				case "雷" : elm = BattleUnit.Element.雷; break;
				}
			}
		}
	
		/// <summary>
		/// 計算メソッド
		/// </summary>
		/// <param name="offense">攻撃側ユニット</param>
		/// <param name="defense">防御側ユニット</param>
		/// <param name="atkData">攻撃データ</param>
		/// <returns>攻撃が成功したかどうか</returns>
		public static bool Calc(BattleUnit offense, BattleUnit defense, AttackData atkData, out bool kill, out BattleOrder reaction){
			reaction = null;
			Dictionary<string, string> status = new Dictionary<string,string>(0);	//状態を表す変数配列
			kill = false;

			//変数配列に追加していく
			status["攻撃属性"] = atkData.elm+"";
			status["攻撃カテゴリ"] = atkData.ctg+"";

			//攻撃を行ったことの通知
			foreach(var s in Battle.NoticeSupport(BattleUnitSupport.Timing.攻撃を行った, offense, status)){
				switch(s[0]){
				}
			}

			//攻撃成功判定
			//命中判定
			int hit = atkData.hit;
			if(atkData.ctg == BattleUnit.Category.物理){
				int difHit = offense.status.物理命中 - defense.status.物理回避;
				if(difHit > 30) difHit = 30;
				if(difHit < -30) difHit = -30;
				hit += difHit;
			}

			if(defense.BadStatus != null){
				switch(defense.BadStatus.type){
				case BattleBadStatus.Type.失明:
				case BattleBadStatus.Type.睡眠:
				case BattleBadStatus.Type.混乱:
				case BattleBadStatus.Type.麻痺:
					hit = 100;
					break;
				}
			}
			if(offense.BadStatus != null && offense.BadStatus.type == BattleBadStatus.Type.失明){
				hit = 0;
			}

			if(!GameMath.JudgeProbab(hit)){
				Battle.viewEffect.Enqueue(new BattleViewEffect("しかし" + offense.Name + "の攻撃は外れた!"));
				return false;
			}

			//ダメージ計算部
			int baseValue = 0;	//基本値

			if(atkData.ctg == BattleUnit.Category.物理){
				baseValue = offense.status.物理攻撃 - defense.status.物理防御/2;
			} else {
				baseValue = offense.status.術式攻撃 - defense.status.術式防御/4;
			}

			int normalDamage = (int)(baseValue * (atkData.power/100.0));	//基本ダメージ
			int timesDamage = normalDamage;	//最終的なダメージ

			//サポート・補助・アビリティによる倍率変化
			foreach(var s in Battle.NoticeSupport(BattleUnitSupport.Timing.攻撃を受けた, defense, status)){
				switch(s[0]){
				case "ダメージ変化":
					timesDamage = (int)(timesDamage * int.Parse(s[1])/100.0);
					break;
				case "カウンター":

					break;
				}
			}
			foreach(var s in Battle.NoticeSupport(BattleUnitSupport.Timing.攻撃を行った, offense, status)){
				switch(s[0]){
				case "ダメージ変化":
					timesDamage = (int)(timesDamage * int.Parse(s[1])/100.0);
					break;
				}
			}
			foreach(var b in offense.GetBuffEffect()){
				//攻撃側の補助効果チェック
				foreach(var e in b.effects){
					switch(e.type){
					case BattleUnitBuff.UnitEffect.Type.物理攻撃:
						if(atkData.ctg == BattleUnit.Category.物理){
							timesDamage = (int)(timesDamage * e.value/100.0);
						} break;
					case BattleUnitBuff.UnitEffect.Type.術式攻撃:
						if(atkData.ctg == BattleUnit.Category.術式){
							timesDamage = (int)(timesDamage * e.value/100.0);
						} break;
					}
				}
			}
			foreach(var b in defense.GetBuffEffect()){
				//防御側の補助効果チェック
				foreach(var e in b.effects){
					switch(e.type){
					case BattleUnitBuff.UnitEffect.Type.物理防御:
						if(atkData.ctg == BattleUnit.Category.物理){
							timesDamage = (int)(timesDamage * e.value/100.0);
						} break;
					case BattleUnitBuff.UnitEffect.Type.術式防御:
						if(atkData.ctg == BattleUnit.Category.術式){
							timesDamage = (int)(timesDamage * e.value/100.0);
						} break;
					}
				}
			}

			int swingDamage = CalcDamageSwing(timesDamage);

			//ダメージを与えて終了
			if(defense.Damage(swingDamage)){
				kill = true;
			}
			Battle.viewEffect.Enqueue(new BattleViewEffect(defense.Name + "に" + swingDamage + "のダメージ"));

			return true;
		}
	}

	/// <summary>
	/// サポートアクションの実行を行うメソッド
	/// </summary>
	class SupportCalculator {
		public class SupportData{
			public readonly string name;
			public readonly int maxTurn;
			public readonly BattleUnitSupport.Timing timing;
			public readonly BattleUnitSupport.Range range;
			public readonly string[][] script;

			public SupportData(string name, string[][] line){
				this.name = name;
				maxTurn = int.Parse(line[0][1]);
				timing = (BattleUnitSupport.Timing)Enum.Parse(typeof(BattleUnitSupport.Timing), line[0][2]);
				range = (BattleUnitSupport.Range)Enum.Parse(typeof(BattleUnitSupport.Range), line[0][3]);

				List<string[]> means = new List<string[]>(0);
				for(int i = 1; i < line.Length; i++){
					means.Add(line[i]);
				}

				script = means.ToArray();
			}
		}

		public static bool Calc(BattleUnit actor, BattleUnit target, SupportData data){
			target.Support.Add(new BattleUnitSupport(data.timing, data.range, data.script, data.maxTurn, data.name));
			return true;
		}
	}

	/// <summary>
	/// バフアクションの実行を行うメソッド
	/// </summary>
	class BuffCalculator {
		public static bool Calc(BattleUnit actor, BattleUnit target, string name, string[][] line){
			int MaxTurn = int.Parse(line[0][1]);
			var ctg = (BattleUnitBuff.Category)Enum.Parse(typeof(BattleUnitBuff.Category), line[0][2]);
			int mainValue = 0;

			List<BattleUnitBuff.UnitEffect> effects = new List<BattleUnitBuff.UnitEffect>(0);
			for(int i = 1; i < line.Length; i++){
				var type = (BattleUnitBuff.UnitEffect.Type)Enum.Parse(typeof(BattleUnitBuff.UnitEffect.Type), line[i][0]);
				int value = int.Parse(line[i][1]);

				if(i == 1) mainValue = value;

				effects.Add(new BattleUnitBuff.UnitEffect(type, value));
			}

			target.AddBuffEffect(new BattleUnitBuff(effects.ToArray(), ctg, MaxTurn, name));

			//DEBUG
			switch(ctg){
			case BattleUnitBuff.Category.攻撃力:
			case BattleUnitBuff.Category.回避力:
			case BattleUnitBuff.Category.命中力:
				if(mainValue < 100){
					Battle.viewEffect.Enqueue(new BattleViewEffect(target.Name + "の" + ctg + "が低下した!"));
				} else {
					Battle.viewEffect.Enqueue(new BattleViewEffect(target.Name + "の" + ctg + "が上昇した!"));
				}
				break;
			case BattleUnitBuff.Category.防御力:
				if(mainValue > 100){
					Battle.viewEffect.Enqueue(new BattleViewEffect(target.Name + "の" + ctg + "が低下した!"));
				} else {
					Battle.viewEffect.Enqueue(new BattleViewEffect(target.Name + "の" + ctg + "が上昇した!"));
				}
				break;
			}
			//DEBUG

			return true;
		}
	}

	/// <summary>
	/// 回復アクションの実行を行うメソッド
	/// </summary>
	class HealCalculator {
		public class HealData{
			public readonly string healType;
			public readonly int value;
			public readonly string valueType;

			public HealData(string[] line){
				healType = line[1];
				valueType = line[2];
				value = int.Parse(line[3]);
			}
		}

		public static bool Calc(BattleUnit actor, BattleUnit target, HealData data){
			int baseValue = 0;

			switch(data.valueType){
			case "INT" :
				baseValue = data.value;
				break;
			case "PERCENT" :
				if(data.healType == "HP"){
					baseValue = (int)(actor.status.MHP * (data.value/100.0));
				} else {
					baseValue = (int)(actor.status.MTP * (data.value/100.0));
				}
				break;
			}

			if(data.healType == "HP"){
				target.HealHP(baseValue);
				Battle.viewEffect.Enqueue(new BattleViewEffect(target.Name + "のHPを" + baseValue + "回復した"));
			} else {
				target.TP += baseValue;
				Battle.viewEffect.Enqueue(new BattleViewEffect(target.Name + "のTPを" + baseValue + "回復した"));
			}

			return true;
		}	
	}

	class BadStatusCalculator{
		public static bool Calc(BattleUnit actor, BattleUnit target, string[] line){
			int probab = int.Parse(line[2]);

			if(GameMath.JudgeProbab(probab)){
				BattleBadStatus.Type type = (BattleBadStatus.Type)Enum.Parse(typeof(BattleBadStatus.Type), line[1]);
				return target.SetBadStatus(type);
			}

			return false;
		}
	}

	class KillCalculator{
		public static bool Calc(BattleUnit actor, BattleUnit target, string[] line){
			int probab = int.Parse(line[1]);

			if(GameMath.JudgeProbab(probab)){
				return target.Kill();
			}

			return false;
		}
	}
}
