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
				if(nowPlayer > 0){
					nowPlayer--;
					orderStack.Pop();	//作ったオーダーを削除
					return null;
				}
			}

			if(result != null){
				//オーダーが完成したなら
				orderStack.Push(result);	//作ったオーダーを追加
				nowPlayer++;

				if(nowPlayer < Battle.PlayerAry.Length){
					plyOrderMaker = new PlayerOrderMaker(Battle.PlayerAry[nowPlayer]);
				} else {
					//エネミーの処理して終了
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
				Drawer.DrawString(20,200+adj_y,o.DEBUG_ACTION_NAME + " : " + o.DEBUG_SELECT_INDEX,new GameColor(255,255,255),"DEBUG_PFONT");
				adj_y += 20;
			}
		}
	}
}
