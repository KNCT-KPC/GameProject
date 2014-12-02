using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPGProject.GamePlay.FrontEnd {
	class TimeMessage{
		const int DEBUG_MAX_COUNT = 30;
		const int DEBUG_LINE_CHAR = 10;
		const int DEBUG_LINE_NUM = 3;

		private int count = 0;
		private int line = 0;
		private int charIndex = 0;
		private bool finish = false;
		private string message;
		private string[] subMessage;

		public TimeMessage(string message){
			this.message = message;
			subMessage = new string[DEBUG_LINE_NUM];

			subMessage[line] = "";
		}

		public void NextCount(){
			if(charIndex >= message.Length){
				finish = true;
				return;
			}

			count++;

			if(count >= DEBUG_MAX_COUNT){
				count = 0;
				subMessage[line] += message[charIndex];
				charIndex++;
			}
		}

		public string[] GetMessage(){
			return subMessage;
		}

		public bool isFinished(){
			return finish;
		}
	}
}
