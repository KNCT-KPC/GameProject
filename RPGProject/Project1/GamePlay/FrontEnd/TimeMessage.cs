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

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="message">表示するメッセージ</param>
		/// <param name="useFont">使用するフォントハンドル</param>
		/// <param name="interval">表示間隔</param>
		/// <param name="lineLength">一行あたりの文字数（目安）</param>
		/// <param name="lineNum">行数</param>
		public TimeMessage(string message, string useFont, int interval = DEBUG_INTERVAL, int lineLength = DEBUG_LINE_LENGTH, int lineNum = DEBUG_LINE_NUM){
			this.interval = interval;
			this.lineLength = lineLength;
			this.lineNum = lineNum;

			this.message = message;
			this.useFont = useFont;
			subMessage = new string[lineNum];
			subMessage[line] = "";
		}

		/// <summary>
		/// 次のカウントに進む
		/// </summary>
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

		/// <summary>
		/// 現在カウントにおけるメッセージを受け取るメソッド
		/// </summary>
		/// <returns>メッセージ</returns>
		public string[] GetMessage(){
			return subMessage;
		}

		/// <summary>
		/// すべての文字を表示し終えたかどうかを判定するメソッド
		/// </summary>
		public bool isFinished(){
			return finish;
		}
	}
}
