using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace RPGProject.GamePlay.Battle.BattleUnits {
	class BattleBadStatus {
		public enum Type{
			失明,
			炎上,
			猛毒,
			麻痺,
			混乱,
			睡眠,
			石化,
		}

		public readonly Type type;
		private int turn;

		public BattleBadStatus(Type type){
			this.type = type;
			turn = 0;
		}

		private static ReadOnlyCollection<int> healProb = new ReadOnlyCollection<int>(new int[]{0,0,50,80,100});
		public bool NextTurn(){
			bool heal = GameMath.JudgeProbab(healProb[turn]);
			
			turn++;
			return heal;
		}

		public bool JudgeOverWrited(Type argType){
			int t0 = (int)argType;
			int t1 = (int)type;

			return (t1 < t0);
		}
	}
}
