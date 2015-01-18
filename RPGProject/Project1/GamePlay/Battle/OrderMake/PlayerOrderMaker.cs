using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPGProject.GamePlay.Battle.BattleUnits;
using RPGProject.GameSystem;
using RPGProject.GamePlay.Database;

namespace RPGProject.GamePlay.Battle.OrderMake {
	/// <summary>
	/// プレイヤーユニット１体につき、オーダーを１つ作成するクラス
	/// </summary>
	class PlayerOrderMaker {
		private BattlePlayer player;	//オーダーを作成するプレイヤー
		private BattleOrder ord;		//作成途中のオーダー
		private Stack<MakeProcess> procStack = new Stack<MakeProcess>(0);		//現在の過程クラス（スタックで階層表現）

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="player">オーダーを作成するプレイヤーユニット</param>
		public PlayerOrderMaker(BattlePlayer player){
			this.player = player;
			this.ord = new BattleOrder(player);
			this.procStack.Push(new InputCommand());
		}

		/// <summary>
		/// 更新メソッド
		/// </summary>
		/// <param name="cancel">キャンセルするかどうか</param>
		/// <returns>完成したオーダー</returns>
		public BattleOrder Update(out bool cancel){
			cancel = false;
			bool finish;

			//スタックの最上位のプロセスを更新
			MakeProcess result = procStack.Peek().Update(player, ord, out finish);

			if(finish){
				//終わったなら
				return ord;
			}
			if(result != null){
				//終わってないが、要素がnullでないなら
				procStack.Push(result);	//次のプロセスヘ
			}

			//キャンセルのみ、親が検知する
			if(GameInput.GetCount(GameInput.Code.INPUT_CANCEL) == 1){
				if(procStack.Count <= 1){
					//要素が残り１個なら、このプレイヤー自体をキャンセル
					//ただし、本当にキャンセルするかどうかはこの親が決めるため、要素の削除は行わない（１個残しておく）
					cancel = true;
					return null;
				} else {
					procStack.Pop();	//最上位を削除
				}
			}

			return null;
		}

		/// <summary>
		/// 描画メソッド
		/// </summary>
		public void Draw(){
			foreach(var p in procStack){
				p.Draw();
			}
		}


		//================================================//
		//                                                //
		//　　　　　　　以下すべて内部クラス　　　　　　　//
		//                                                //
		//================================================//
		/// <summary>
		/// オーダー作成プロセス
		/// </summary>
		private interface MakeProcess{
			MakeProcess Update(BattlePlayer player, BattleOrder ord, out bool finish);
			void Draw();
		}

		/// <summary>
		/// コマンド選択プロセス
		/// </summary>
		private class InputCommand : MakeProcess{
			private enum Command{
				Attack,
				Guard,
				Skill,
				Item,
				Escape,
				MAXNUM
			};
			private string[] comMessage = new string[]{
				"持っている武器で通常攻撃します。",
				 "攻撃を防いでダメージを軽減します。",
				 "習得しているスキルを使用します。",
				 "持っているアイテムを使用します。",
				 "戦闘から逃走します。",
			};

			private int selectIndex = (int)Command.Attack;

			public MakeProcess Update(BattlePlayer player, BattleOrder ord, out bool finish){
				finish = false;

				if(GameInput.GetCount(GameInput.Code.INPUT_DECIDE) == 1){
					//決定キーが押されていたら

					Command selectCmd = (Command)selectIndex;	//コマンドを取得
					switch(selectCmd){
					case Command.Attack:
						ord.actionName = "通常攻撃"; break;
					case Command.Escape:
						finish = true; break;
					case Command.Guard:
						ord.actionName = "ガード";
						finish = true; break;
					case Command.Skill:
						return new SelectSkill();
					}
					//Skill,Itemならそれらのセレクト
					return new SelectTergetInEnemy();
				}

				if(GameInput.GetCount(GameInput.Code.INPUT_DOWN) == 1){
					selectIndex = (selectIndex + 1)%(int)Command.MAXNUM;
				}
				if(GameInput.GetCount(GameInput.Code.INPUT_UP) == 1){
					selectIndex = (selectIndex + (int)Command.MAXNUM - 1)%(int)Command.MAXNUM;
				}

				//DEBUG
				Battle.DEBUG_MESSAGE = ""+comMessage[selectIndex];
				//DEBUG

				return null;
			}

			/// <summary>
			/// 描画メソッド
			/// </summary>
			public void Draw(){
				Drawer.DrawString(20,120,""+(Command)selectIndex,new GameColor(255,255,255),"DEBUG_PFONT");
			}
		}

		/// <summary>
		/// 敵からターゲットを選択するプロセス
		/// </summary>
		private class SelectTergetInEnemy : MakeProcess{
			private int trgIndex = 0;

			public SelectTergetInEnemy(){
				while(Battle.EnemyAry[trgIndex].isDead){
					trgIndex++;
				}
			}

			public MakeProcess Update(BattlePlayer player, BattleOrder ord, out bool finish){
				finish = false;

				if(GameInput.GetCount(GameInput.Code.INPUT_DECIDE) == 1){
					//決定キーが押されていたら、ターゲットを追加して終了
					ord.slctIndex = trgIndex;
					finish = true;
					return null;
				}

				//←→キーによる対象選択
				//ただし、戦闘不能者は飛ばし、ループしない
				if(GameInput.GetCount(GameInput.Code.INPUT_RIGHT) == 1){
					//→キーの場合
					int temp = trgIndex;
					do{	//戦闘不能者をスキップする
						temp++;
						if(temp >= Battle.EnemyAry.Length) {
							temp = trgIndex;
							break;
						}
					} while(Battle.EnemyAry[temp].isDead);
					trgIndex = temp;
				}
				if(GameInput.GetCount(GameInput.Code.INPUT_LEFT) == 1){
					//←キーの場合
					int temp = trgIndex;
					do{ //戦闘不能者をスキップする
						temp--;
						if(temp < 0) {
							temp = trgIndex;
							break;
						}
					} while(Battle.EnemyAry[temp].isDead);
					trgIndex = temp;
				}

				//DEBUG
				Battle.DEBUG_MESSAGE = Battle.EnemyAry[trgIndex].Name+"に攻撃します";
				//DEBUG

				return null;
			}

			/// <summary>
			/// 描画メソッド
			/// </summary>
			public void Draw(){
				Drawer.DrawString(20,140,Battle.EnemyAry[trgIndex].Name,new GameColor(255,255,255),"DEBUG_PFONT");
			}
		}

		/// <summary>
		/// 味方からターゲットを選択するプロセス
		/// </summary>
		private class SelectTergetInPlayer : MakeProcess{
			private int trgIndex = 0;

			public SelectTergetInPlayer(){
				while(Battle.PlayerAry[trgIndex].isDead){
					trgIndex++;
				}
			}

			public MakeProcess Update(BattlePlayer player, BattleOrder ord, out bool finish){
				finish = false;

				if(GameInput.GetCount(GameInput.Code.INPUT_DECIDE) == 1){
					//決定キーが押されていたら、ターゲットを追加して終了
					ord.slctIndex = trgIndex;
					finish = true;
					return null;
				}

				//←→キーによる対象選択
				//ただし、戦闘不能者は飛ばし、ループしない
				if(GameInput.GetCount(GameInput.Code.INPUT_RIGHT) == 1){
					//→キーの場合
					int temp = trgIndex;
					do{	//戦闘不能者をスキップする
						temp++;
						if(temp >= Battle.PlayerAry.Length) {
							temp = trgIndex;
							break;
						}
					} while(Battle.EnemyAry[temp].isDead);
					trgIndex = temp;
				}
				if(GameInput.GetCount(GameInput.Code.INPUT_LEFT) == 1){
					//←キーの場合
					int temp = trgIndex;
					do{ //戦闘不能者をスキップする
						temp--;
						if(temp < 0) {
							temp = trgIndex;
							break;
						}
					} while(Battle.PlayerAry[temp].isDead);
					trgIndex = temp;
				}

				//DEBUG
				Battle.DEBUG_MESSAGE = Battle.PlayerAry[trgIndex].Name+"に攻撃します";
				//DEBUG

				return null;
			}

			/// <summary>
			/// 描画メソッド
			/// </summary>
			public void Draw(){
				Drawer.DrawString(20,140,Battle.PlayerAry[trgIndex].Name,new GameColor(255,255,255),"DEBUG_PFONT");
			}
		}

		/// <summary>
		/// スキルを選択するプロセス
		/// </summary>
		private class SelectSkill : MakeProcess{
			private int slctIndex = 0;

			public SelectSkill(){
			}

			public MakeProcess Update(BattlePlayer player, BattleOrder ord, out bool finish){
				finish = false;

				if(GameInput.GetCount(GameInput.Code.INPUT_DECIDE) == 1){
					//決定キーが押されていたら、ターゲットを追加して終了
					string skillName = player.Skills[slctIndex];
					BattleAction action = BattleActionDatabase.GetAction(skillName);

					ord.actionName = skillName;
					if(action.trgRange == BattleAction.TargetRange.All){
						finish = true;
					}
					if(action.trgSide == BattleAction.TargetSide.Rival){
						return new SelectTergetInEnemy();
					} 
					return new SelectTergetInPlayer();
				}

				//←→キーによる対象選択
				//ただし、戦闘不能者は飛ばし、ループしない
				if(GameInput.GetCount(GameInput.Code.INPUT_UP) == 1){
					if(slctIndex-1 >= 0){
						slctIndex--;
					}
				}
				if(GameInput.GetCount(GameInput.Code.INPUT_DOWN) == 1){
					if(slctIndex+1 < player.Skills.Count){
						slctIndex++;
					}
				}

				Battle.DEBUG_MESSAGE = player.Skills[slctIndex]+"";

				return null;
			}

			/// <summary>
			/// 描画メソッド
			/// </summary>
			public void Draw(){
			}
		}
	}
}
