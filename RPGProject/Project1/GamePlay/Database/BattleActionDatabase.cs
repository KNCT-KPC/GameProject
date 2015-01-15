using System;
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
			new BattleAction("通常攻撃", 0, BattleAction.TargetSide.Rival, BattleAction.TargetType.単体, 0,
				new string[]{
					""
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
