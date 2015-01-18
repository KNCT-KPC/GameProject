using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPGProject.GamePlay.Battle.BattleUnits;
using RPGProject.GamePlay.Database;

namespace RPGProject.GamePlay.Battle {
	//戦闘で発生する行動（＝オーダー）を表すクラス
	class BattleOrder{
		public BattleUnit actor;
		public string actionName;
		public int slctIndex;

		public BattleOrder(BattleUnit actor){
			this.actor = actor;
		}
		public BattleOrder(BattleUnit actor, string actionName, int slctIndex){
			this.actor = actor;
			this.actionName = actionName;
			this.slctIndex = slctIndex;
		}

		/// <summary>
		/// オーダーの実行順を定義する比較クラス
		/// </summary>
		public class OrderComparer : System.Collections.IComparer
		{
			public int Compare(object x, object y)
			{
				BattleOrder xOrd = (BattleOrder)x;
				BattleOrder yOrd = (BattleOrder)y;

				int xPri =  BattleActionDatabase.GetAction(xOrd.actionName).priority;
				int yPri =  BattleActionDatabase.GetAction(yOrd.actionName).priority;

				//オーダーxとオーダーyのアクション優先度が違うなら
				if(xPri != yPri){
					return xPri - yPri;	//引いた結果を返す
				}

				//同じなら、行動速度で決定する
				int xSpd = xOrd.actor.status.行動速度;
				int ySpd = yOrd.actor.status.行動速度;
				return xSpd - ySpd;
			}
		}
	}
}
