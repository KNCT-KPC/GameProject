using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPGProject.GameSystem;
using RPGProject.GamePlay.Battle.BattleUnit;
using RPGProject.GamePlay.Battle.OrderMake;

namespace RPGProject.GamePlay.Battle {
	class Battle {
		//シングルトン記述
		private static Battle battle;
		public static void BattleStart(int DEBUG){
			battle = new Battle(DEBUG);
		}
		public static Battle GetInstance(){
			return battle;
		}

		private Battle(int DEBUG){
			//コンストラクタ
			PlayerAry = new BattlePlayer[4];
			EnemyAry = new BattleEnemy[3];

			PlayerAry[0] = new BattlePlayer("プレイヤー１");
			PlayerAry[1] = new BattlePlayer("プレイヤー２");
			PlayerAry[2] = new BattlePlayer("プレイヤー３");
			PlayerAry[3] = new BattlePlayer("プレイヤー４");
			EnemyAry[0] = new BattleEnemy("エネミー１");
			EnemyAry[1] = new BattleEnemy("エネミー２");
			EnemyAry[2] = new BattleEnemy("エネミー３");
		}
		//ここまで


		private string DEBUG_MESSAGE = "ここにメッセージが表示される";

		public BattlePlayer[] PlayerAry{get; private set;}
		public BattleEnemy[] EnemyAry{get; private set;}

		public OrderMaker ordMaker;

		private bool DEBUG_START = true;

		public void Update(){
			if(DEBUG_START){
				ordMaker = new OrderMaker();
				DEBUG_START = false;
			}

			ordMaker.Update();
		}
		public void Draw(){
			Drawer.DrawString(20,100,DEBUG_MESSAGE,new GameColor(255,255,255),"DEBUG_PFONT");
		}

		public void DEBUG_SetMessage(string str){
			DEBUG_MESSAGE = str;
		}
	}
}
