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
		/// <summary>
		/// 実行メソッド
		/// </summary>
		/// <returns>アクションの実行が終了したかどうか</returns>
		public static void Execute(BattleOrder order){
			int line = 0;	//アクションスクリプトを読み進めた行数
			List<BattleUnit> targets = null;	//アクションのターゲットを保存しておくフィールド
			BattleAction action = BattleActionDatabase.GetAction(order.actionName);
			string[][] script = action.script.ToArray();	//何度もでるので退避

			order.actor.TP -= action.TP;
			Battle.viewEffect.Enqueue(new BattleViewEffect(order.actor.Name + "の" + order.actionName + "!"));
			bool success = false;

			//アクションスクリプト実行部
			while(line < script.Length){
				switch(script[line][0]){
				//===============================================//
				// | ID:To | 制御 | ターゲットの範囲指定を受ける //
				//===============================================//
				case "To":
					targets = new List<BattleUnit>(0);
					switch(script[line][1]){
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

					foreach(var t in targets){
						if(t.isDead) continue;
						for(int i = line+1; i < script.Length && script[i][0] != "EndTo"; i++){
							switch(script[i][0]){
							//=================================//
							// | ID:If | 制御 | 場合分けを行う //
							//=================================//
							case "If":
								switch(script[i][1]){
								case "Chase":
									if(!success){
										while(line < script.Length && script[line][0] != "EndIf") {
											line++;
										} 
									}
									line++;
									break;
								}
								break;

							//=================================//
							// | ID:Attack | 終端 | 攻撃を行う //
							//=================================//
							case "Attack":
								success = AttackCalculator.Calc(order.actor, t, new AttackCalculator.AttackData(script[i]));
								break;

							//================================================//
							// | ID:Support | 終端 | サポート効果の追加を行う //
							//================================================//
							case "Support":{
								List<string[]> effect = new List<string[]>(0);
								while(script[i][0] != "SupportEnd"){
									effect.Add(script[i]);
									i++;
								}
	
								success = SupportCalculator.Calc(order.actor, t, new SupportCalculator.SupportData(order.actionName, effect.ToArray()));
								break;}

							//===============================//
							// | ID:Heal | 終端 | 回復を行う //
							//===============================//
							case "Heal":
								success = HealCalculator.Calc(order.actor, t, new HealCalculator.HealData(script[i]));
								break;

							//=========================================//
							// | ID:Buff | 終端 | 補助効果の追加を行う //
							//=========================================//
							case "Buff":{
								List<string[]> effect = new List<string[]>(0);
								while(script[i][0] != "BuffEnd"){
									effect.Add(script[i]);
									i++;
								}

								success = BuffCalculator.Calc(order.actor, t, order.actionName, effect.ToArray());
								break;}

							//==============================================//
							// | ID:BadStatus | 終端 | 状態異常の追加を行う //
							//==============================================//
							case "BadStatus":
								success = BadStatusCalculator.Calc(order.actor, t, script[i]);
								break;

							default :
								Program.AssertExit("存在しないステートメント" + script[i][0] + "が指定されました。");
								break;
							}
						}
					}

					while(line < script.Length && script[line][0] != "EndTo") {
						line++;
					} line++;
					break;
				}
			}
		}
	}
}
