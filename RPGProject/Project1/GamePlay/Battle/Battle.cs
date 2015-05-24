using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPGProject.GameSystem;
using RPGProject.GamePlay.Battle.BattleUnits;
using RPGProject.GamePlay.Battle.OrderMake;
using RPGProject.GamePlay.Battle.OrderExecute;
using RPGProject.GamePlay.Battle.BattleViewEffects;
using RPGProject.GamePlay.Database;

namespace RPGProject.GamePlay.Battle {
	//戦闘を担当する大元のクラス
	class Battle {
		public Battle(int DEBUG){
			BattleActionDatabase.MakeInstance();

			DEBUG_MESSAGE = "ここにメッセージが表示される";

			//コンストラクタ
			PlayerAry = new BattlePlayer[4];
			EnemyAry = new BattleEnemy[3];

			//DEBUG
			PlayerAry[0] = new BattlePlayer("魔法使い", new BattleUnit.Status(280, 300, 210, 174, 272, 242, 165, 12, 211, 206, 196, 182), 
								new string[]{"ファイア","バーニング","ファイアストーム","アイス", "ブリザード", "アイスストーム", "サンダー", "ライトニング", "サンダーストーム",
											 "コンセントレーション","メテオストライク","エナジースマイト","リフレクション","コンセントレーション",});
			PlayerAry[1] = new BattlePlayer("戦士", new BattleUnit.Status(380, 120, 315, 276, 144, 170, 135, 17, 189, 180, 183, 197), 
								new string[]{"スキル１","スキル２","スキル３","スキル４","スキル５","クロスカウンター","攻勢の構え","守勢の構え","庇護の盾","挑発"});
			PlayerAry[2] = new BattlePlayer("僧侶", new BattleUnit.Status(350, 280, 235, 245, 224, 223, 108, 13, 162, 202, 237, 250), 
								new string[]{"ヒーリング","ハイヒーリング","ワイドヒーリング","リカバリー","ワイドリカバリー","リバイバル","ファーストエイド","ディスペル",
											 "アタックオーラ","バイタルオーラ","セイントオーラ","ディヴァイン","ストーンカーシス"});
			PlayerAry[3] = new BattlePlayer("銃士", new BattleUnit.Status(300, 150, 288, 186, 160, 162, 186, 13, 272, 236, 238, 210), 
								new string[]{"ハニーショット","ベノムショット","パララシスショット","スリープショット","ブラインドショット","ファイアショット","フリーズショット",
											 "サンダーショット","ウィークミスト","エロードミスト","照明弾","掃射体制"});
			EnemyAry[0] = new BattleEnemy("エネミー１", new BattleUnit.Status(800, 200, 200, 200, 200, 200, 10, 200, 200, 200, 200, 200), 
								new string[]{});
			EnemyAry[1] = new BattleEnemy("エネミー２", new BattleUnit.Status(800, 200, 200, 200, 200, 200, 10, 200, 200, 200, 200, 200), 
								new string[]{});
			EnemyAry[2] = new BattleEnemy("エネミー３", new BattleUnit.Status(800, 200, 200, 200, 200, 200, 10, 200, 200, 200, 200, 200), 
								new string[]{});
			//DEBUG

			ordMaker = new OrderMaker();
			viewEffect = new Queue<BattleViewEffect>();
		}

		//グローバル要素
		public static string DEBUG_MESSAGE{get; set;}
		public static BattlePlayer[] PlayerAry{get; private set;}	//プレイヤー配列
		public static BattleEnemy[] EnemyAry{get; private set;}	//エネミー配列
		public static Queue<BattleViewEffect> viewEffect{get; private set;}

		//フィールド
		private OrderMaker ordMaker;	
		private OrderExecuter ordExecuter;
		private Scene scene = Scene.BattleStart;

		private enum Scene{
			BattleStart,
			TurnStart,
			OrderMake,
			OrderExecute,
			TurnEnd,
		}

		/// <summary>
		/// 更新メソッド
		/// </summary>
		public void Update(){
			//MakerがnullならExecuteの、そうでないならMakerのUpdateを呼ぶ。
			//全部DEBUG実装
			//ちゃんとシーケンス処理する
			if(viewEffect.Count != 0){
				bool end = viewEffect.Peek().Update();
				if(end){
					viewEffect.Dequeue();
				}
			} else {
				switch(scene){
				case Scene.BattleStart :
					scene = Scene.TurnStart;
					break;
				case Scene.TurnStart:
					scene = Scene.OrderMake;
					break;
				case Scene.OrderMake :
					BattleOrder[] result = ordMaker.Update();
					if(result != null){
						ordExecuter = new OrderExecuter(result);
						ordMaker = null;
						scene = Scene.OrderExecute;
					}
					break;
				case Scene.OrderExecute :
					if(ordExecuter.Execute()){
						ordExecuter = null;
						scene = Scene.TurnEnd;
					}
					break;
				case Scene.TurnEnd:
					ordMaker = new OrderMaker();

					foreach(var p in PlayerAry){
						p.NextTurn();
					}
					foreach(var e in EnemyAry){
						e.NextTurn();
					}

					scene = Scene.TurnStart;
					break;
				}
			}
		}

		/// <summary>
		/// 描画メソッド
		/// </summary>
		public void Draw(){
			//MakerがnullならExecuteの、そうでないならMakerのDrawを呼ぶ。
			switch(scene){
			case Scene.OrderMake:
				ordMaker.Draw();
				break;
			case Scene.OrderExecute:
				ordExecuter.Draw();
				break;
			}

			//DEBUG
			Drawer.DrawString(20,100,DEBUG_MESSAGE,new GameColor(255,255,255),"DEBUG_PFONT");
			int adj = 0;
			foreach(BattlePlayer p in PlayerAry){
				string DEBUG = "";

				if(p.BadStatus != null){
					DEBUG += "["+p.BadStatus.type.ToString().Substring(0,1)+"]";
				}

				foreach(var s in p.Support){
					DEBUG += s.Name.Substring(0,1);
				}
				foreach(var b in p.GetBuffEffect()){
					DEBUG += b.Name.Substring(0,1) + b.nowTurn;
				}

				int d = 0;
				if(adj >= 600) d = 80;
				Drawer.DrawString(20+adj%600, 300+d, p.Name+ "|" + DEBUG, new GameColor(255,255,255),"DEBUG_PFONT");
				Drawer.DrawString(20+adj%600, 322+d, "HP : " + p.HP + "/" + p.status.MHP,new GameColor(255,255,255),"DEBUG_PFONT");
				Drawer.DrawString(20+adj%600, 344+d, "TP : " + p.TP + "/" + p.status.MTP,new GameColor(255,255,255),"DEBUG_PFONT");
				adj += 300;
			}

			int adj_y = 0;
			foreach(BattleEnemy e in EnemyAry){
				string DEBUG = "";

				if(e.BadStatus != null){
					DEBUG += "["+e.BadStatus.type.ToString().Substring(0,1)+"]";
				}

				foreach(var s in e.Support){
					DEBUG += s.Name.Substring(0,1);
				}
				foreach(var b in e.GetBuffEffect()){
					DEBUG += b.Name.Substring(0,1) + b.nowTurn;
				}
				Drawer.DrawString(300, 200+adj_y, e.Name + " | HP:" + e.HP + DEBUG,new GameColor(255,255,255),"DEBUG_PFONT");
				adj_y += 22;
			}
			//DEBUG
		}

		/// <summary>
		/// ユニット配列から、ランダムに生存しているユニットを選択して返すメソッド
		/// </summary>
		/// <param name="party">ユニット配列</param>
		/// <returns>ランダムに選ばれたユニット</returns>
		public static int GetRandomInParty(BattleUnit[] party){
			List<int> list = new List<int>(GameMath.GetRandomArray(party.Length));
			for(int i = 0; i < party.Length; i++){
				if(party[i].isDead){
					list.Remove(i);
				}
			}

			return list[0];
		}

		/// <summary>
		/// アクションの対象サイドとアクターから、対象となるユニット配列を返すメソッド
		/// </summary>
		/// <param name="side">対象サイド</param>
		/// <param name="actor">アクター</param>
		/// <returns>対象となるユニット配列</returns>
		public static BattleUnit[] GetSideParty(BattleAction.TargetSide side, BattleUnit actor){
			List<BattleUnit> targets = new List<BattleUnit>(0);

			switch(side){
			//アクターの味方サイドなら
			case BattleAction.TargetSide.Friend:
				if(actor is BattlePlayer){
					targets.AddRange(Battle.PlayerAry);
				} else {
					targets.AddRange(Battle.EnemyAry);
				}
				break;
			//アクターの相手サイドなら
			case BattleAction.TargetSide.Rival:
				if(actor is BattlePlayer){
					targets.AddRange(Battle.EnemyAry);
				} else {
					targets.AddRange(Battle.PlayerAry);
				}
				break;
			//戦闘参加者全員なら
			case BattleAction.TargetSide.All:
				targets.AddRange(Battle.PlayerAry);
				targets.AddRange(Battle.EnemyAry);
				break;
			case BattleAction.TargetSide.Ones:
				targets.Add(actor);
				break;
			}

			return targets.ToArray();
		}

		/// <summary>
		/// サポート効果のトリガーを通達するメソッド
		/// </summary>
		/// <param name="timing">トリガーの発生タイミング</param>
		/// <param name="actor">発生させたユニット</param>
		/// <param name="status">状況を表す変数配列</param>
		/// <returns>発生したサポート効果を列挙した配列</returns>
		public static string[][] NoticeSupport(BattleUnitSupport.Timing timing, BattleUnit actor, Dictionary<string, string> status){
			List<string[]> eft = new List<string[]>(0);

			foreach(var p in PlayerAry){
				eft.AddRange(p.GetSupportEffect(timing, actor, status));
			}
			foreach(var e in EnemyAry){
				eft.AddRange(e.GetSupportEffect(timing, actor, status));
			}

			return eft.ToArray();
		}

		public static BattleUnit GetBattleUnit(string name){
			foreach(var p in PlayerAry){
				if(p.Name == name) return p;
			}

			foreach(var e in EnemyAry){
				if(e.Name == name) return e;
			}

			return null;
		}
	}
}
