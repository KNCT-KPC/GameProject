using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPGProject.GamePlay.Battle.BattleUnits {
	class BattlePlayer : BattleUnit {
		public BattlePlayer(string name, BattleUnit.Status status, string[] skills) : base(name, status, skills){}
	}
}
