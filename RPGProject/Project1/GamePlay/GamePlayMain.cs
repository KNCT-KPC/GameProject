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
					"YesNo",
						"Case Yes",
							"Message",
								"「はい」が選択されました。",
							"End",
							"YesNo",
								"Case Yes",
									"Message",
										"さらに「はい」が選択されました。",
									"End",
								"Break",
								"Case No",
									"Message",
										"こんどは「いいえ」が選択されました。",
									"End",
								"Break",
							"End",
						"Break",
						"Case No",
							"Message",
							"「いいえ」が選択されました。",
							"End",
						"Break",
					"End",
					"ScriptEnd",
				}
			);

			windowMgr = new WindowMgr();
			map = new Map(30,30);
		}

		public void Update(){
			if(!windowMgr.isEnable()){
				map.Update();
			}
			windowMgr.Update();

			if(script != null && !script.isMonitoring()){
				string opr;
				string[] option;
				script.Next(out opr, out option);

				if(opr == null){
					script = null;
					windowMgr.RemoveAllWindow();
				} else {
					switch(opr){
					case "Message":{
						MessageWindow w = windowMgr.addMessage(option);
						script.Monitoring(w);
						/*
						MessageWindow w = new MessageWindow(option);
						windowMgr.addWindow(w);
						script.Monitoring(w);
						*/
						}break;
					case "YesNo":{
						YesNoWindow w = new YesNoWindow();
						windowMgr.addWindow(w);
						script.Monitoring(w);
						}break;
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
