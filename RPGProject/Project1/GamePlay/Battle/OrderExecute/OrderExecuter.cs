using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPGProject.GamePlay.Battle.OrderExecute {
	class OrderExecuter {
		private BattleOrder[] order;
		private int index = -1;
		private int effectCount = 0;

		public OrderExecuter(BattleOrder[] order){
			this.order = order; 
			BattleOrder.ActionComparer comp = new BattleOrder.ActionComparer();
			Array.Sort(order, comp);
		}

		public bool Update(){
			if(effectCount == 0){
				do {	//行動可能なオーダーが見つかるまで繰り返し
					index++;
					if(index >= order.Length){	
						//繰り返し途中でオーダーをすべて見切ったら終了
						return true;
					}
				} while(!order[index].actor.isAbleToAction());

				//オーダー解釈
				Battle.EnemyAry[order[index].DEBUG_SELECT_INDEX].HP -= 20;
				effectCount = 60;
				//
			}

			if(effectCount != 0){
				effectCount--;
			}

			return false;
		}
		public void Draw(){
		
		}
	}
}
