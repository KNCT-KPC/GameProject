using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPGProject.GamePlay.Database;
using RPGProject.GamePlay.Battle.BattleUnits;

namespace RPGProject.GamePlay.Battle.OrderExecute {
	/// <summary>
	/// オーダーが指定したアクションを処理するクラス
	/// </summary>
	class ActionExecuter {
		//フィールド
		private BattleOrder order;		//このアクションを指定したオーダー
		private BattleAction action;	//実行するアクション
		private int nowScriptLine = 0;	//アクションスクリプトを読み進めた行数
		private int DEBUG_interval = 0;

		private List<BattleUnit> targets = null;	//アクションのターゲットを保存しておくフィールド

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="order">アクションを指定しているオーダー</param>
		public ActionExecuter(BattleOrder order){
			this.order = order;
			action = BattleActionDatabase.GetAction(order.actionName);
		}

		/// <summary>
		/// 実行メソッド
		/// </summary>
		/// <returns>アクションの実行が終了したかどうか</returns>
		public bool Execute(){
			if(DEBUG_interval != 0){
				DEBUG_interval--;
				return false;
			}

			//アクションスクリプト実行部
			while(nowScriptLine < action.script.Count){
				int line = nowScriptLine++;

				switch(action.script[line][0]){
				//=================================//
				// | ID:Attack | 終端 | 攻撃を行う //
				//=================================//
				case "Attack":
					if(targets.Count == 0) break;

					foreach(var t in targets){
						ActionCalculator.Attack(order.actor, t, new ActionCalculator.AttackData(action.script[line]));
					}
					goto TERMINAL;

				//===============================================//
				// | ID:To | 制御 | ターゲットの範囲指定を受ける //
				//===============================================//
				case "To":
					targets = new List<BattleUnit>(0);
					switch(action.script[line][1]){
					case "Targets":
						targets.AddRange(GetSideParty(action.trgSide, order.actor));
						if(action.trgRange == BattleAction.TargetRange.Single){
							//
							BattleUnit t = targets[order.slctIndex];
							if(action.veer && !t.isLive()){
								t = GetRandomInParty(targets.ToArray());
							}

							targets.RemoveAll((BattleUnit u)=>{return u != t;});
						}
						break;
					case "Actor":
						targets.Add(order.actor);
						break;
					case "Rivals":
						targets.AddRange(GetSideParty(BattleAction.TargetSide.Rival, order.actor));
						break;
					case "Friends":
						targets.AddRange(GetSideParty(BattleAction.TargetSide.Friend, order.actor));
						break;
					case "All":
						targets.AddRange(GetSideParty(BattleAction.TargetSide.All, order.actor));
						break;
					}
					break;

				//========================================================//
				// | ID:EndTo | 制御 | ターゲットの範囲指定をリセットする //
				//========================================================//
				case "EndTo":
					targets = null;
					break;
				}
			}
			return true;

		TERMINAL:
			DEBUG_interval = 30;
			return false;
		}

		/// <summary>
		/// アクションの対象サイドとアクターから、対象となるユニット配列を返すメソッド
		/// </summary>
		/// <param name="side">対象サイド</param>
		/// <param name="actor">アクター</param>
		/// <returns>対象となるユニット配列</returns>
		private BattleUnit[] GetSideParty(BattleAction.TargetSide side, BattleUnit actor){
			List<BattleUnit> targets = new List<BattleUnit>(0);

			switch(action.trgSide){
			//アクターの味方サイドなら
			case BattleAction.TargetSide.Friend:
				if(order.actor is BattlePlayer){
					targets.AddRange(Battle.PlayerAry);
				} else {
					targets.AddRange(Battle.EnemyAry);
				}
				break;
			//アクターの相手サイドなら
			case BattleAction.TargetSide.Rival:
				if(order.actor is BattlePlayer){
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
			}

			return targets.ToArray();
		}


		/// <summary>
		/// ユニット配列から、ランダムに生存しているユニットを選択して返すメソッド
		/// </summary>
		/// <param name="party">ユニット配列</param>
		/// <returns>ランダムに選ばれたユニット</returns>
		private BattleUnit GetRandomInParty(BattleUnit[] party){
			List<BattleUnit> result = new List<BattleUnit>(party);
			result.RemoveAll((BattleUnit u)=>{return !u.isLive();});
				//述語で生存しているかどうかを判定

			Random rand = new Random();
			return result[rand.Next(0, result.Count)];
		}
	}
}
