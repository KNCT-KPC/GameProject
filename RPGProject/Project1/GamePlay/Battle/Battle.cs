using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPGProject.GameSystem;
using RPGProject.GamePlay.Battle.BattleUnits;
using RPGProject.GamePlay.Battle.OrderMake;
using RPGProject.GamePlay.Battle.OrderExecute;

namespace RPGProject.GamePlay.Battle {
	//戦闘を担当する大本のクラス
	class Battle {
		public Battle(int DEBUG){
			DEBUG_MESSAGE = "ここにメッセージが表示される";

			//コンストラクタ
			PlayerAry = new BattlePlayer[4];
			EnemyAry = new BattleEnemy[3];

			//DEBUG
			PlayerAry[0] = new BattlePlayer("魔法使い", new BattleUnit.Status(280, 300, 210, 174, 272, 242, 165, 12, 211, 206, 196, 182));
			PlayerAry[1] = new BattlePlayer("戦士", new BattleUnit.Status(380, 120, 315, 276, 144, 170, 135, 17, 189, 180, 183, 197));
			PlayerAry[2] = new BattlePlayer("僧侶", new BattleUnit.Status(350, 280, 235, 245, 224, 223, 108, 13, 162, 202, 237, 250));
			PlayerAry[3] = new BattlePlayer("銃士", new BattleUnit.Status(300, 150, 288, 186, 160, 162, 186, 13, 272, 236, 238, 210));
			EnemyAry[0] = new BattleEnemy("エネミー１", new BattleUnit.Status(900, 200, 200, 200, 200, 200, 200, 200, 200, 200, 200, 200));
			EnemyAry[1] = new BattleEnemy("エネミー２", new BattleUnit.Status(900, 200, 200, 200, 200, 200, 200, 200, 200, 200, 200, 200));
			EnemyAry[2] = new BattleEnemy("エネミー３", new BattleUnit.Status(900, 200, 200, 200, 200, 200, 200, 200, 200, 200, 200, 200));
			//DEBUG

			ordMaker = new OrderMaker();
		}

		//グローバル要素
		public static string DEBUG_MESSAGE{get; set;}
		public static BattlePlayer[] PlayerAry{get; private set;}	//プレイヤー配列
		public static BattleEnemy[] EnemyAry{get; private set;}	//エネミー配列

		//フィールド
		private OrderMaker ordMaker;	
		private OrderExecuter ordExecuter;

		/// <summary>
		/// 更新メソッド
		/// </summary>
		public void Update(){
			//MakerがnullならExecuteの、そうでないならMakerのUpdateを呼ぶ。
			if(ordMaker != null){
				BattleOrder[] result = ordMaker.Update();
				if(result != null){
					ordExecuter = new OrderExecuter(result);
					ordMaker = null;
				}
			} else {
				bool end = ordExecuter.Update();
				if(end){
					ordExecuter = null;
					ordMaker = new OrderMaker();
				}
			}
		}

		/// <summary>
		/// 描画メソッド
		/// </summary>
		public void Draw(){
			//MakerがnullならExecuteの、そうでないならMakerのDrawを呼ぶ。
			if(ordMaker != null){
				ordMaker.Draw();
			} else {
				ordExecuter.Draw();
			}

			//DEBUG
			Drawer.DrawString(20,100,DEBUG_MESSAGE,new GameColor(255,255,255),"DEBUG_PFONT");
			int adj_y = 0;
			foreach(BattlePlayer p in PlayerAry){
				Drawer.DrawString(300, 200+adj_y, p.Name + " | HP:" + p.HP + " TP:" + p.TP,new GameColor(255,255,255),"DEBUG_PFONT");
				adj_y += 22;
			}
			foreach(BattleEnemy e in EnemyAry){
				Drawer.DrawString(300, 200+adj_y, e.Name + " | HP:" + e.HP + " TP:" + e.TP,new GameColor(255,255,255),"DEBUG_PFONT");
				adj_y += 22;
			}
			//DEBUG
		}
	}
}
