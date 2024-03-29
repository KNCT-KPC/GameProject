﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPGProject.GamePlay.Battle;
using System.Collections.ObjectModel;

namespace RPGProject.GamePlay.Database {
	class BattleActionDatabase {
		private static BattleActionDatabase instance;

		public static void MakeInstance(){
			instance = new BattleActionDatabase();
		}

		public static BattleAction GetAction(string name){
			BattleAction r;
			bool exist = instance.actions.TryGetValue(name, out r);

			if(!exist){
				Program.AssertExit("存在しないオーダー : " + name +"が参照されました。 （場所 BattleActionDatabase");
				return null;
			}

			return r;
		}

		//完全ダミー。ファイルによる処理を想定
		private readonly BattleAction[] DUMMY_ACT = new BattleAction[]{
			new BattleAction("通常攻撃", BattleAction.Type.攻撃, 0, BattleAction.TargetSide.Rival, BattleAction.TargetRange.Single, true, 0, 100,
				new string[][]{},
				new string[][]{
					new string[]{"To", "Targets"},
					new string[]{"Attack","物理", "無", "100", "100"}
				}),
			new BattleAction("ガード", BattleAction.Type.補助, 0, BattleAction.TargetSide.Ones, BattleAction.TargetRange.Single, false, 5, 100,
				new string[][]{},
				new string[][]{
					new string[]{"To", "Targets"},
					new string[]{"Support", "0", "攻撃を受けた", "自分自身"},
					new string[]{"Effect", "ダメージ変化", "60"},
					new string[]{"EndSupport"}
				}),
			new BattleAction("ファイア", BattleAction.Type.攻撃, 4, BattleAction.TargetSide.Rival, BattleAction.TargetRange.Single, true, 0, 100,
				new string[][]{},
				new string[][]{
					new string[]{"To", "Targets"},
					new string[]{"Attack","術式", "炎", "130", "100"}					
				}),
			new BattleAction("バーニング", BattleAction.Type.攻撃, 16, BattleAction.TargetSide.Rival, BattleAction.TargetRange.Single, true, 0, 100,
				new string[][]{},
				new string[][]{
					new string[]{"To", "Targets"},
					new string[]{"Attack","術式", "炎", "150", "100"},
					new string[]{"If", "Chase"},
					new string[]{"BadStatus","炎上", "60"},
				}),
			new BattleAction("ファイアストーム", BattleAction.Type.攻撃, 32, BattleAction.TargetSide.Rival, BattleAction.TargetRange.All, true, 0, 100,
				new string[][]{},
				new string[][]{
					new string[]{"To", "Targets"},
					new string[]{"Attack","術式", "炎", "140", "100"}					
				}),
			new BattleAction("アイス", BattleAction.Type.攻撃, 4, BattleAction.TargetSide.Rival, BattleAction.TargetRange.Single, true, 0, 100,
				new string[][]{},
				new string[][]{
					new string[]{"To", "Targets"},
					new string[]{"Attack","術式", "氷", "130", "100"}					
				}),
			new BattleAction("ブリザード", BattleAction.Type.攻撃, 16, BattleAction.TargetSide.Rival, BattleAction.TargetRange.Single, true, 0, 100,
				new string[][]{},
				new string[][]{
					new string[]{"To", "Targets"},
					new string[]{"Attack","術式", "氷", "150", "100"},
					new string[]{"If", "Chase"},
					new string[]{"Kill","20"},
				}),
			new BattleAction("アイスストーム", BattleAction.Type.攻撃, 32, BattleAction.TargetSide.Rival, BattleAction.TargetRange.All, true, 0, 100,
				new string[][]{},
				new string[][]{
					new string[]{"To", "Targets"},
					new string[]{"Attack","術式", "氷", "140", "100"}					
				}),
			new BattleAction("サンダー", BattleAction.Type.攻撃, 4, BattleAction.TargetSide.Rival, BattleAction.TargetRange.Single, true, 0, 100,
				new string[][]{},
				new string[][]{
					new string[]{"To", "Targets"},
					new string[]{"Attack","術式", "雷", "130", "100"}					
				}),
			new BattleAction("ライトニング", BattleAction.Type.攻撃, 16, BattleAction.TargetSide.Rival, BattleAction.TargetRange.Single, true, 0, 100,
				new string[][]{},
				new string[][]{
					new string[]{"To", "Targets"},
					new string[]{"Attack","術式", "雷", "150", "100"},
					new string[]{"If", "Chase"},
					new string[]{"BadStatus","麻痺", "60"},
				}),
			new BattleAction("サンダーストーム", BattleAction.Type.攻撃, 32, BattleAction.TargetSide.Rival, BattleAction.TargetRange.All, true, 0, 100,
				new string[][]{},
				new string[][]{
					new string[]{"To", "Targets"},
					new string[]{"Attack","術式", "雷", "140", "100"}					
				}),
			new BattleAction("コンセントレーション", BattleAction.Type.補助, 3, BattleAction.TargetSide.Ones, BattleAction.TargetRange.Single, true, 2, 100,
				new string[][]{},
				new string[][]{
					new string[]{"To", "Targets"},
					new string[]{"Support", "1", "攻撃を行った", "自分自身"},
					new string[]{"If", "攻撃カテゴリ", "術式"},
					new string[]{"Effect", "ダメージ変化", "220"},
					new string[]{"EndIf"},
					new string[]{"EndSupport"}
				}),
			new BattleAction("メテオストライク", BattleAction.Type.攻撃, 12, BattleAction.TargetSide.Rival, BattleAction.TargetRange.Single, true, 0, 100,
				new string[][]{},
				new string[][]{
					new string[]{"To", "Targets"},
					new string[]{"Attack","物理", "無", "250", "80"},
					new string[]{"If", "Chase"},
					new string[]{"BadStatus", "混乱", "40"},
				}),
			new BattleAction("エナジースマイト", BattleAction.Type.攻撃, 6, BattleAction.TargetSide.Rival, BattleAction.TargetRange.Single, true, 0, 100,
				new string[][]{},
				new string[][]{
					new string[]{"To", "Targets"},
					new string[]{"Attack","物理", "無", "180", "80"},
					new string[]{"EndTo"},
					new string[]{"To", "Actor"},
					new string[]{"If", "Kill"},
					new string[]{"Heal", "TP", "PERCENT", "10"},
				}),
			new BattleAction("リフレクション", BattleAction.Type.補助, 18, BattleAction.TargetSide.Friend, BattleAction.TargetRange.Single, true, 5, 100,
				new string[][]{},
				new string[][]{
					new string[]{"To", "Targets"},
					new string[]{"Support", "0", "攻撃を受けた", "味方全員"},
					new string[]{"If", "攻撃カテゴリ", "物理"},
					new string[]{"Effect", "ダメージ変化", "60"},
					new string[]{"Effect", "カウンター", "術式", "無", "150"},
					new string[]{"EndIf"},
					new string[]{"EndSupport"},
				}),
			new BattleAction("ヒーリング", BattleAction.Type.回復, 6, BattleAction.TargetSide.Friend, BattleAction.TargetRange.Single, false, 0, 100,
				new string[][]{},
				new string[][]{
					new string[]{"To", "Targets"},
					new string[]{"Heal", "HP", "INT", "80"},
				}),
			new BattleAction("ハイヒーリング", BattleAction.Type.回復, 15, BattleAction.TargetSide.Friend, BattleAction.TargetRange.Single, false, 0, 100,
				new string[][]{},
				new string[][]{
					new string[]{"To", "Targets"},
					new string[]{"Heal", "HP", "INT", "260"},
				}),
			new BattleAction("ワイドヒーリング", BattleAction.Type.回復, 36, BattleAction.TargetSide.Friend, BattleAction.TargetRange.All, false, 0, 100,
				new string[][]{},
				new string[][]{
					new string[]{"To", "Targets"},
					new string[]{"Heal", "HP", "INT", "180"},
				}),
			new BattleAction("アタックオーラ", BattleAction.Type.補助, 12, BattleAction.TargetSide.Friend, BattleAction.TargetRange.All, false, 0, 150,
				new string[][]{},
				new string[][]{
					new string[]{"To", "Targets"},
					new string[]{"Buff", "5", "攻撃力"},
					new string[]{"物理攻撃", "120"},
					new string[]{"BuffEnd"},
				}),
			new BattleAction("エロードミスト", BattleAction.Type.補助, 12, BattleAction.TargetSide.Rival, BattleAction.TargetRange.All, false, 0, 150,
				new string[][]{},
				new string[][]{
					new string[]{"To", "Targets"},
					new string[]{"Buff", "5", "防御力"},
					new string[]{"物理防御", "120"},
					new string[]{"BuffEnd"},
				}),
		};

		private Dictionary<string, BattleAction> actions;
		private BattleActionDatabase(){
			actions = new Dictionary<string,BattleAction>();
			foreach(var a in DUMMY_ACT){
				actions.Add(a.name, a);
			}
		}
	}
}
