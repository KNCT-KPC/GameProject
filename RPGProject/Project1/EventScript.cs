using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPGProject {
	interface EventMonitor{
		bool State(out int value);
	}

	class EventScript {
		private readonly string[] eventScript;
		private int index;
		private bool pause;
		private EventMonitor monitor;
		
		public string TopOperator{get; private set;}
		public string[] TopOption{get; private set;}
		private int topIndex;

		public EventScript(string[] eventScript){
			this.eventScript = eventScript;
			pause = false;
			monitor = null;
		}

		private string[] Operators = new string[]{
			"Message",
			"YesNo",
		};
		public void Next(out string opr, out string[] option){
			//
			//=================DEBUG=================
			//       後でメソッドを整理すること。
			List<string> newOption = new List<string>(0);

			int value = -1;
			if(monitor != null){
				if(!monitor.State(out value)){
					Program.AssertExit("モニタリングオブジェクトが有効でない状態で、次のスクリプトが読み込まれました。（EventScript.cs）");
				}

				if(TopOperator == "YesNo"){
					string searchStr = "Case No";
					if(value == 0) searchStr = "Case Yes";

					int optIndex = 0;
					int caseCount = 0;
					while(true){
						if(caseCount == 0 && TopOption[optIndex] == searchStr){
							break;
						}
						if(TopOption[optIndex].IndexOf("Case") == 0){
							caseCount++;
						}
						if(TopOption[optIndex] == "Break"){
							caseCount--;
						}

						optIndex++;
					}

					index = topIndex+optIndex+1;
					/*
					optIndex++;

					opr = TopOption[optIndex];
					TopOperator = opr;
					int caseCount = 0;

					optIndex++;

					while(true){
						if(TopOption[optIndex].IndexOf("Case") == 0){
							caseCount++;
						}
						if(TopOption[optIndex] == "Break"){
							caseCount--;
							if(caseCount == 0){
								break;
							}
						}

						newOption.Add(TopOption[optIndex]);
						optIndex++;
					}

					option = newOption.ToArray();
					TopOption = newOption.ToArray();
					return;
					*/
				}
			}
			


			if(eventScript[index] == "ScriptEnd"){
				opr = null;
				option = null;
				return;
			} else {
				opr = eventScript[index];
				TopOperator = opr;
				index++;

				topIndex = index;
				int oprCount = 1;
				while(true){
					if(Array.IndexOf(Operators, eventScript[index]) != -1){
						oprCount++;
					}
					if(eventScript[index] == "End"){
						oprCount--;
						if(oprCount == 0){
							index++;
							break;
						}
					}

					newOption.Add(eventScript[index]);
					index++;
				}

				option = newOption.ToArray();
				TopOption = newOption.ToArray();
			}
		}

		public void Monitoring(EventMonitor monitor){
			this.monitor = monitor;
		}

		public bool isMonitoring(){
			int DUMMY;
			return (monitor != null && !monitor.State(out DUMMY));
		}
	}
}
