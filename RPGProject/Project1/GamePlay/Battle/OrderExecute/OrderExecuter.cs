using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPGProject.GamePlay.Battle.OrderExecute {
	class OrderExecuter {
		private BattleOrder[] order;

		public OrderExecuter(BattleOrder[] order){
			this.order = order;
		}

		public bool Update(){
			return true;
		}

		public void Draw(){
		
		}
	}
}
