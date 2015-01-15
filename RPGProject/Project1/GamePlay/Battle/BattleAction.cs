using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace RPGProject.GamePlay.Battle {
	class BattleAction {
		public enum TargetSide{
			Friend,
			Rival,
			All
		}
		public enum TargetType{
			単体,
			全体,
			自身
		}

		readonly public string name;
		readonly public int TP;
		readonly public TargetSide trgSide;
		readonly public TargetType trgType;
		readonly public int priority;
		readonly public ReadOnlyCollection<string> script;

		public BattleAction(string name, int TP, TargetSide trgSide, 
							TargetType trgType, int priority, string[] script){
			this.name = name;
			this.TP = TP;
			this.trgSide = trgSide;
			this.trgType = trgType;
			this.priority = priority;
			this.script = new ReadOnlyCollection<string>(script);
		}
	}
}
