using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPGProject.GamePlay.Battle.OrderMake {
	class BattleOrder {
		public enum TergetSide{
			PlayerSide,
			EnemySide
		}

		public int DEBUG_COMMAND_INDEX;
		public TergetSide trgSide;
		public int DEBUG_SELECT_INDEX;
	}
}
