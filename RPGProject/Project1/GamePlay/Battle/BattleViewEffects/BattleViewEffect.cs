using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPGProject.GamePlay.Battle.BattleViewEffects {
	class BattleViewEffect {
		public const int DEFAULT_TIME = 50;

		private int time;
		private int count;
		private string DEBUG_MESSAGE;

		public BattleViewEffect(string DEBUG_MESSAGE, int time){
			this.DEBUG_MESSAGE = DEBUG_MESSAGE;
			this.time = time;
		}
		public BattleViewEffect(string DEBUG_MESSAGE){
			this.time = DEFAULT_TIME;
			this.DEBUG_MESSAGE = DEBUG_MESSAGE;
		}

		public bool Update(){
			if(count == 0){
				Battle.DEBUG_MESSAGE = DEBUG_MESSAGE;
			}

			count++;
			if(count >= time) return true;
			return false;
		}

		public void Draw(){
		
		}
	}
}
