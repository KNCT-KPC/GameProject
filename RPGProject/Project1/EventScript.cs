using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPGProject {
	class EventScript {
		private readonly string[] eventScript;
		private int index;

		
		public string TopOperator{get; private set;}
		public string[] TopOption{get; private set;}

		public EventScript(string[] eventScript){
			this.eventScript = eventScript;
		}

		public void Next(out string opr,out string[] option){
			List<string> result = new List<string>(0);

			if(eventScript[index] == "ScriptEnd"){
				opr = null;
				option = null;
				return;
			} else {
				opr = eventScript[index];
				TopOperator = opr;
				index++;

				while(eventScript[index] != "End"){
					result.Add(eventScript[index]);
					index++;
				}

				option = result.ToArray();
				TopOption = result.ToArray();
				index++;
			}
		}
	}
}
