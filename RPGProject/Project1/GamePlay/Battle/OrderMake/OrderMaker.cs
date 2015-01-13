using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPGProject.GamePlay.Battle.OrderMake {
	class OrderMaker {
		private PlayerOrderMaker plyOrderMaker;
		private List<BattleOrder> order;
		private int nowPlayer = 0;

		public OrderMaker(){
			plyOrderMaker = new PlayerOrderMaker(Battle.GetInstance().PlayerAry[nowPlayer]);
		}

		public List<BattleOrder> Update(){
			plyOrderMaker.Update();
			return null;
		}
	}
}
