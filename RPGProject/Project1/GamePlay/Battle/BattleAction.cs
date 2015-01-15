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
			All,
			Ones
		}
		public enum TargetRange{
			Single,
			All,
		}
		public enum Type{
			攻撃,
		}

		readonly public string name;
		readonly public Type type;
		readonly public int TP;
		readonly public TargetSide trgSide;
		readonly public TargetRange trgRange;
		readonly public bool veer;
		readonly public int priority;
		readonly public ReadOnlyCollection<string[]> script;

		public BattleAction(string name, Type type, int TP, TargetSide trgSide, 
							TargetRange trgRange, bool veer, int priority, string[][] script){
			this.name = name;
			this.type = type;
			this.TP = TP;
			this.trgSide = trgSide;
			this.trgRange = trgRange;
			this.priority = priority;
			this.veer = veer;
			this.script = new ReadOnlyCollection<string[]>(script);
		}
	}
}
