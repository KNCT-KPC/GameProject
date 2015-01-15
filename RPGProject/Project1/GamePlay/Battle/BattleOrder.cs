using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPGProject.GamePlay.Battle.BattleUnits;
using RPGProject.GamePlay.Database;

namespace RPGProject.GamePlay.Battle {
	//戦闘で発生する行動（＝オーダー）を表すクラス
	class BattleOrder{
		public enum TergetSide{
			PlayerSide,
			EnemySide
		}

		public BattleUnit actor;
		public string DEBUG_ACTION_NAME;
		public TergetSide trgSide;
		public int DEBUG_SELECT_INDEX;

		public BattleOrder(BattleUnit actor){
			this.actor = actor;
		}


		public class ActionComparer : System.Collections.IComparer
		{
			public int Compare(object x, object y)
			{
				BattleOrder xOrd = (BattleOrder)x;
				BattleOrder yOrd = (BattleOrder)y;

				int xPri =  BattleActionDatabase.GetAction(xOrd.DEBUG_ACTION_NAME).priority;
				int yPri =  BattleActionDatabase.GetAction(yOrd.DEBUG_ACTION_NAME).priority;

				if(xPri != yPri){
					return xPri - yPri;
				}

				int xSpd = xOrd.actor.status.行動速度;
				int ySpd = yOrd.actor.status.行動速度;
				return xSpd - ySpd;
			}
		}
	}
}
