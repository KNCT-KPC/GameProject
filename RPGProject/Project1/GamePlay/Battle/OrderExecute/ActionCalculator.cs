using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPGProject.GamePlay.Battle.BattleUnits;
using RPGProject.GamePlay.Battle.BattleViewEffects;

namespace RPGProject.GamePlay.Battle.OrderExecute {
	static class AttackCalculator{
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
		/// <param name="actor">攻撃側ユニット</param>
		/// <param name="target">防御側ユニット</param>
		/// <param name="atkData">攻撃データ</param>
		/// <returns>攻撃が成功したかどうか</returns>
		public static bool Calc(BattleUnit actor, BattleUnit target, AttackData atkData){
			List<string[]> status = new List<string[]>(0);	//状態を表す変数配列

			//変数配列に追加していく
			status.Add(new string[]{"属性", atkData.elm+""});
			status.Add(new string[]{"カテゴリ", atkData.ctg+""});

			//攻撃を行ったことの通知
			foreach(var s in Battle.NoticeSupport(BattleUnitSupport.Timing.攻撃を行った, actor, null)){
				switch(s[0]){
				}
			}

			//攻撃成功判定
			int hit = atkData.hit;
			if(atkData.ctg == BattleUnit.Category.物理){
				int difHit = actor.status.物理命中 - target.status.物理回避;
				if(difHit > 30) difHit = 30;
				if(difHit < -30) difHit = -30;
				hit += difHit;
			}

			//命中判定
			if(!GameMath.JudgeProbab(hit)){
				Battle.viewEffect.Enqueue(new BattleViewEffect("しかし" + actor.Name + "の攻撃は外れた!"));
				return false;
			}

			//ダメージ計算部
			int baseValue = 0;	//基本値

			if(atkData.ctg == BattleUnit.Category.物理){
				baseValue = actor.status.物理攻撃 - target.status.物理防御/2;
			} else {
				baseValue = actor.status.術式攻撃 - target.status.術式防御/4;
			}

			int normalDamage = (int)(baseValue * (atkData.power/100.0));	//基本ダメージ
			int timesDamage = normalDamage;	//最終的なダメージ

			//サポート・補助・アビリティによる倍率変化
			foreach(var s in Battle.NoticeSupport(BattleUnitSupport.Timing.攻撃を受けた, target, null)){
				switch(s[0]){
				case "ダメージ変化":
					timesDamage = (int)(timesDamage * int.Parse(s[1])/100.0);
					break;
				}
			}

			//ダメージを与えて終了
			target.Damage(timesDamage);
			Battle.viewEffect.Enqueue(new BattleViewEffect(target.Name + "に" + timesDamage + "のダメージ"));

			return true;
		}
	}

	/// <summary>
	/// サポートアクションの実行を行うメソッド
	/// </summary>
	class SupportCalculator {
		public static bool Calc(BattleUnit actor, BattleUnit target, string[][] line){
			string supportName = line[0][1];
			int maxTurn = int.Parse(line[0][2]);
			BattleUnitSupport.Timing timing = (BattleUnitSupport.Timing)Enum.Parse(typeof(BattleUnitSupport.Timing), line[0][3]);
			BattleUnitSupport.Range range = (BattleUnitSupport.Range)Enum.Parse(typeof(BattleUnitSupport.Range), line[0][4]);

			List<string[]> means = new List<string[]>(0);
			for(int i = 1; i < line.Length; i++){
				means.Add(line[i]);
			}

			target.Support.Add(new BattleUnitSupport(timing, range, means.ToArray(), maxTurn, supportName));
			return true;
		}
	}
}
