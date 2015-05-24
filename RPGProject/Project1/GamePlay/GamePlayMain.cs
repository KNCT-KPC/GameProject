using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPGProject.GamePlay.Map;
using RPGProject.GamePlay.Battle;
using RPGProject.GamePlay.Window;
using RPGProject.GamePlay.Party;

namespace RPGProject {
	class GamePlayMain {
		private Map map;
		private Battle battle;
		private WindowMgr windowMgr;
		
		private Party party;
		private EventScript script;

		public GamePlayMain(){
			script = new EventScript(
				new string[]{
					"Message",
						"これはテスト用のメッセージです。^nこの行は改行されます。^n行の最大は３行の予定です。",
						"これは新しいページで表示されます。",
					"End",
					//"YesNo",
					//"End",
					"ScriptEnd",
				}
			);

			windowMgr = new WindowMgr();
			map = new Map(30,30);
		}

		public void Update(){
			if(windowMgr.isEnable()){
				windowMgr.Update();
			} else {
				map.Update();
			}

			if(windowMgr.isEnable()){
				if(script != null){
					string opr;
					string[] option;
					script.Next(out opr, out option);

					if(opr == null){
						script = null;
						windowMgr.RemoveAllWindow();
					} else {
						switch(opr){
						case "Message":
							windowMgr.addWindow(new MessageWindow(option));
							break;
						case "YesNo":
							windowMgr.addWindow(new YesNoWindow());
						break;
						}
					}
				}
			}
		}

		public void Draw(){
			map.Draw();
			windowMgr.Draw();
		}
	}
}
