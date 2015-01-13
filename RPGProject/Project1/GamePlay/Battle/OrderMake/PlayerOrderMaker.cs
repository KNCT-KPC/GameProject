using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPGProject.GamePlay.Battle.BattleUnit;
using RPGProject.GameSystem;

namespace RPGProject.GamePlay.Battle.OrderMake {
	class PlayerOrderMaker {
		public enum Scene{
			InputCommand, 
			SelectSkill, 
			SelectItem,
			SelectTerget, 
			PrevCommand,
			Finish
		};

		private BattlePlayer player;	//オーダーを作成するプレイヤー
		private BattleOrder ord;		//作成途中のオーダー
		private MakeProcess proc;		//現在の過程クラス

		public PlayerOrderMaker(BattlePlayer player){
			this.player = player;
			this.ord = new BattleOrder();
			this.proc = new InputCommand(null);
		}

		public BattleOrder Update(){
			bool finish;

			MakeProcess result = proc.Update(player, ord, out finish);
			if(finish){
				return ord;
			}

			if(result != null){
				proc = result;
			}

			return null;
		}



		/// <summary>
		/// オーダー作成プロセス
		/// </summary>
		private abstract class MakeProcess{
			protected MakeProcess prevProcess;
			public MakeProcess(MakeProcess prevProcess){this.prevProcess = prevProcess;}

			public abstract MakeProcess Update(BattlePlayer player, BattleOrder ord, out bool finish);
		}


		private class InputCommand : MakeProcess{
			private const int DEBUG_SELECT_MAXNUM = 5;
			private int DEBUG_SELECT_INDEX = 0;

			public InputCommand(MakeProcess prevProcess) : base(prevProcess){}

			public override MakeProcess Update(BattlePlayer player, BattleOrder ord, out bool finish){
				finish = false;

				if(GameInput.GetCount(GameInput.Code.INPUT_CANCEL) == 1){
					return prevProcess;
				}
				if(GameInput.GetCount(GameInput.Code.INPUT_DECIDE) == 1){
					ord.DEBUG_COMMAND_INDEX = DEBUG_SELECT_INDEX;
					//自身対象はターゲットを取らないので、IDで検知してopr = Operate.Finish
					return new SelectTergetInEnemy(this);
				}

				if(GameInput.GetCount(GameInput.Code.INPUT_DOWN) == 1){
					DEBUG_SELECT_INDEX = (DEBUG_SELECT_INDEX + 1)%DEBUG_SELECT_MAXNUM;
				}
				if(GameInput.GetCount(GameInput.Code.INPUT_UP) == 1){
					DEBUG_SELECT_INDEX = (DEBUG_SELECT_INDEX + DEBUG_SELECT_MAXNUM - 1)%DEBUG_SELECT_MAXNUM;
				}

				//DEBUG
				Battle.GetInstance().DEBUG_SetMessage(""+DEBUG_SELECT_INDEX);
				//DEBUG

				return null;
			}
		}


		private class SelectTergetInEnemy : MakeProcess{
			private int trgIndex = 0;
			public SelectTergetInEnemy(MakeProcess prevProcess) : base(prevProcess){}

			public override MakeProcess Update(BattlePlayer player, BattleOrder ord, out bool finish){
				finish = false;

				if(GameInput.GetCount(GameInput.Code.INPUT_CANCEL) == 1){
					return prevProcess;
				}
				if(GameInput.GetCount(GameInput.Code.INPUT_DECIDE) == 1){
					ord.trgSide = BattleOrder.TergetSide.EnemySide;
					ord.DEBUG_SELECT_INDEX = trgIndex;
					finish = true;
					return null;
				}

				if(GameInput.GetCount(GameInput.Code.INPUT_RIGHT) == 1){
					if(trgIndex+1 < Battle.GetInstance().EnemyAry.Length){
						trgIndex++;
					}
				}
				if(GameInput.GetCount(GameInput.Code.INPUT_LEFT) == 1){
					if(trgIndex-1 >= 0){
						trgIndex--;
					}
				}

				//DEBUG
				Battle.GetInstance().DEBUG_SetMessage(""+Battle.GetInstance().EnemyAry[trgIndex].Name);
				//DEBUG

				return null;
			}
		}
	}
}
