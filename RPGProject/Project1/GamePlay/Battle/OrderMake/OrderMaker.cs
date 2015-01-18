using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPGProject.GameSystem;

namespace RPGProject.GamePlay.Battle.OrderMake {
	/// <summary>
	/// 戦闘におけるの各ユニットの行動表を作成するクラス
	/// </summary>
	class OrderMaker {
		//フィールド
		private PlayerOrderMaker plyOrderMaker;	//プレイヤーサイドのオーダー作成クラス
		private Stack<BattleOrder> orderStack = new Stack<BattleOrder>(0);		//オーダー配列
		private int nowPlayer = 0;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public OrderMaker(){
			while(nowPlayer < Battle.PlayerAry.Length && !Battle.PlayerAry[nowPlayer].isAbleToAction()){
				nowPlayer++;
			}
			plyOrderMaker = new PlayerOrderMaker(Battle.PlayerAry[nowPlayer]);
		}

		/// <summary>
		/// 更新メソッド
		/// </summary>
		/// <returns>作成したかどうか</returns>
		public BattleOrder[] Update(){
			bool cancel;

			//プレイヤーのオーダーを作成する
			BattleOrder result = plyOrderMaker.Update(out cancel);

			if(cancel){
				//キャンセルなら、戻れる場合は戻る
				int temp = nowPlayer;
				do {
					temp--;
					if(temp < 0) {
						temp = nowPlayer;
						break;
					}
				} while(!Battle.PlayerAry[temp].isAbleToAction());

				if(nowPlayer != temp){
					nowPlayer = temp;
					orderStack.Pop();	//作ったオーダーを削除
					plyOrderMaker = new PlayerOrderMaker(Battle.PlayerAry[nowPlayer]);
				}
				return null;
			}

			if(result != null){
				//オーダーが完成したなら
				orderStack.Push(result);	//作ったオーダーを追加
				do {
					nowPlayer++;
				} while(nowPlayer < Battle.PlayerAry.Length && !Battle.PlayerAry[nowPlayer].isAbleToAction());

				if(nowPlayer < Battle.PlayerAry.Length){
					plyOrderMaker = new PlayerOrderMaker(Battle.PlayerAry[nowPlayer]);
				} else {
					//エネミーの処理して終了
					foreach(var e in Battle.EnemyAry){
						if(e.isAbleToAction()){
							orderStack.Push(new BattleOrder(e, "通常攻撃", Battle.GetRandomInParty(Battle.PlayerAry)));
						}
					}
					return orderStack.ToArray();
				}
			}

			return null;
		}

		/// <summary>
		/// 描画メソッド
		/// </summary>
		public void Draw(){
			plyOrderMaker.Draw();

			int adj_y = 0;
			foreach(BattleOrder o in orderStack){
				Drawer.DrawString(20,200+adj_y, o.actor.Name.Substring(0,1) + " " + o.actionName + " : " + o.slctIndex,new GameColor(255,255,255),"DEBUG_PFONT");
				adj_y += 20;
			}
		}
	}
}
