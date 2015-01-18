using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPGProject.GamePlay.Database;
using RPGProject.GamePlay.Battle.BattleUnits;
using RPGProject.GamePlay.Battle.BattleViewEffects;

namespace RPGProject.GamePlay.Battle.OrderExecute {
	/// <summary>
	/// オーダーが指定したアクションを処理するクラス
	/// </summary>
	class ActionExecuter {
		//フィールド
		private BattleOrder order;		//このアクションを指定したオーダー
		private BattleAction action;	//実行するアクション
		private int nowScriptLine = 0;	//アクションスクリプトを読み進めた行数

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
			//アクションスクリプト実行部
			while(nowScriptLine < action.script.Count){
				int line = nowScriptLine++;

				switch(action.script[line][0]){
				//=================================//
				// | ID:Attack | 終端 | 攻撃を行う //
				//=================================//
				case "Attack":
					if(targets.Count == 0) break;
					Battle.viewEffect.Enqueue(new BattleViewEffect(order.actor.Name + "の攻撃!"));

					foreach(var t in targets){
						AttackCalculator.Calc(order.actor, t, new AttackCalculator.AttackData(action.script[line]));
					}
					goto TERMINAL;

				//================================================//
				// | ID:Support | 終端 | サポート効果の追加を行う //
				//================================================//
				case "Support":{
					if(targets.Count == 0) break;
					Battle.viewEffect.Enqueue(new BattleViewEffect(order.actor.Name + "は" + order.actionName + "を使った!"));

					List<string[]> effect = new List<string[]>(0);
					while(action.script[line][0] != "SupportEnd"){
						effect.Add(action.script[line]);
						line++;
					}

					foreach(var t in targets){
						SupportCalculator.Calc(order.actor, t, effect.ToArray());
					}
					goto TERMINAL;}

				//===============================================//
				// | ID:To | 制御 | ターゲットの範囲指定を受ける //
				//===============================================//
				case "To":
					targets = new List<BattleUnit>(0);
					switch(action.script[line][1]){
					case "Targets":
						targets.AddRange(Battle.GetSideParty(action.trgSide, order.actor));
						if(action.trgRange == BattleAction.TargetRange.Single){
							BattleUnit t = targets[order.slctIndex];
							if(action.veer && t.isDead){
								t = targets.ToArray()[Battle.GetRandomInParty(targets.ToArray())];
							}

							targets.RemoveAll((BattleUnit u)=>{return u != t;});
						}
						break;
					case "Actor":
						targets.Add(order.actor);
						break;
					case "Rivals":
						targets.AddRange(Battle.GetSideParty(BattleAction.TargetSide.Rival, order.actor));
						break;
					case "Friends":
						targets.AddRange(Battle.GetSideParty(BattleAction.TargetSide.Friend, order.actor));
						break;
					case "All":
						targets.AddRange(Battle.GetSideParty(BattleAction.TargetSide.All, order.actor));
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
			return false;
		}
	}
}
