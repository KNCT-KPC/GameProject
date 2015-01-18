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

				BattleAction xAct = BattleActionDatabase.GetAction(xOrd.actionName);
				BattleAction yAct = BattleActionDatabase.GetAction(yOrd.actionName);

				//オーダーxとオーダーyのアクション優先度が違うなら
				if(xAct.priority != yAct.priority){
					return xAct.priority - yAct.priority;	//引いた結果を返す
				}

				//同じなら、行動速度で決定する
				int xSpd = (int)(xOrd.actor.status.行動速度 * (xAct.speed/100.0));
				int ySpd = (int)(yOrd.actor.status.行動速度 * (yAct.speed/100.0));
				return xSpd - ySpd;
			}
		}
	}
}
