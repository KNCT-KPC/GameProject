using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPGProject.GamePlay.FrontEnd {
	class TimeMessage{
		const int DEBUG_INTERVAL = 10;
		const int DEBUG_LINE_LENGTH = 100;
		const int DEBUG_LINE_NUM = 3;

		private int interval;
		private int lineLength;
		private int lineNum;

		private int count = 0;
		private int line = 0;
		private int charIndex = 0;
		private bool finish = false;
		private string message;
		private string useFont;
		private string[] subMessage;

		public TimeMessage(string message, string useFont, int maxCount = DEBUG_INTERVAL, int lineLength = DEBUG_LINE_LENGTH, int lineNum = DEBUG_LINE_NUM){
			this.interval = maxCount;
			this.lineLength = lineLength;
			this.lineNum = lineNum;

			this.message = message;
			this.useFont = useFont;
			subMessage = new string[lineNum];
			subMessage[line] = "";
		}

		public void NextCount(){
			if(finish){
				return;
			}

			count++;
			if(count >= interval){
				count = 0;
				subMessage[line] += message[charIndex];
				charIndex++;

				if(GameSystem.GameFont.GetDrawStringWidth(useFont, subMessage[line]) >= lineLength){
					line++;
				}
			}

			if(charIndex >= message.Length || line >= lineNum){
				finish = true;
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
