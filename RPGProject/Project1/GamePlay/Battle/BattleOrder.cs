using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPGProject.GamePlay.Battle {
	//戦闘で発生する行動（＝オーダー）を表すクラス
	class BattleOrder {
		public enum TergetSide{
			PlayerSide,
			EnemySide
		}

		public string DEBUG_COMMAND_NAME;
		public TergetSide trgSide;
		public int DEBUG_SELECT_INDEX;
	}
}
