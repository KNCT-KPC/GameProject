using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPGProject.GamePlay.Battle.BattleUnit;
using RPGProject.GameSystem;

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
			this.ord = new BattleOrder();
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



		//以下すべて内部クラス

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
					ord.DEBUG_COMMAND_NAME = ""+(Command)selectIndex;
					//自身対象はターゲットを取らないので、IDで検知してopr = Operate.Finish
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

			public void Draw(){
				Drawer.DrawString(20,120,""+(Command)selectIndex,new GameColor(255,255,255),"DEBUG_PFONT");
			}
		}

		/// <summary>
		/// 敵からターゲットを選択するプロセス
		/// </summary>
		private class SelectTergetInEnemy : MakeProcess{
			private int trgIndex = 0;

			public MakeProcess Update(BattlePlayer player, BattleOrder ord, out bool finish){
				finish = false;

				if(GameInput.GetCount(GameInput.Code.INPUT_DECIDE) == 1){
					ord.trgSide = BattleOrder.TergetSide.EnemySide;
					ord.DEBUG_SELECT_INDEX = trgIndex;
					finish = true;
					return null;
				}

				if(GameInput.GetCount(GameInput.Code.INPUT_RIGHT) == 1){
					if(trgIndex+1 < Battle.EnemyAry.Length){
						trgIndex++;
					}
				}
				if(GameInput.GetCount(GameInput.Code.INPUT_LEFT) == 1){
					if(trgIndex-1 >= 0){
						trgIndex--;
					}
				}

				//DEBUG
				Battle.DEBUG_MESSAGE = Battle.EnemyAry[trgIndex].Name+"に攻撃します";
				//DEBUG

				return null;
			}

			public void Draw(){
				Drawer.DrawString(20,140,Battle.EnemyAry[trgIndex].Name,new GameColor(255,255,255),"DEBUG_PFONT");
			}
		}
	}
}
