using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPGProject.GamePlay.Database;
using RPGProject.GamePlay.Battle.BattleUnits;

namespace RPGProject.GamePlay.Battle.OrderExecute {
	/// <summary>
	/// オーダーを解釈して戦闘を進行するクラス
	/// </summary>
	class OrderExecuter {
		//フィールド
		private int index = 0;
		private BattleOrder[] order;
		//割り込みスキルは、↑をスタックにして実現

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="order">実行するオーダー配列</param>
		public OrderExecuter(BattleOrder[] order){
			this.order = order; 

			BattleOrder.OrderComparer comp = new BattleOrder.OrderComparer();
			Array.Sort(order, comp);
			Array.Reverse(order);
		}

		/// <summary>
		/// 更新メソッド
		/// </summary>
		/// <returns>オーダーの実行が終了したか</returns>
		public bool Execute(){
			ActionExecuter.Execute(order[index]);	//アクション実行
				
			do {	//行動可能なオーダーが見つかるまで繰り返し
				index++;
				if(index >= order.Length){	
					//繰り返し途中でオーダーをすべて見切ったら終了
					return true;
				}
			} while(!order[index].actor.isAbleToAction());

			return false;
		}
		public void Draw(){
		
		}
	}
}
