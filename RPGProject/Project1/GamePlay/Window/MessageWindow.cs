using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPGProject.GamePlay.FrontEnd;
using RPGProject.GameSystem;


namespace RPGProject.GamePlay.Window
{
	class MessageWindow : Window
	{
		const int STRING_X = 20;		//1文字目のx座標
		const int STRING_Y = 20;		//1文字目のy座標
		const int LINE_HEIGHT = 30;		//1行あたりのy幅
		String[] message;
		TimeMessage timeMessage;
		int messageCount = 0;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="arg_message">表示させるメッセージ</param>
		public MessageWindow(int arg_px,int arg_py, int arg_width, int arg_height, String[] arg_message )  : base(arg_px,arg_py,arg_width,arg_height){
			message = arg_message;
			timeMessage = new TimeMessage(message[messageCount], "DEBUG_PFONT"); 
		}
		public MessageWindow(String[] arg_message )  : base(10, 350, 620, 120){
			message = arg_message;
			timeMessage = new TimeMessage(message[messageCount], "DEBUG_PFONT"); 
		}
		
		override public void Update(){ 
			timeMessage.NextCount();
			if (timeMessage.isFinished()){
				if (GameSystem.GameInput.GetCount(GameInput.Code.INPUT_DECIDE) == 1) {
					messageCount++;
					if(messageCount < message.Length){
						timeMessage = new TimeMessage(message[messageCount], "DEBUG_PFONT");
					} else {
						this.Break();
					}
				}
			} else {
				if (GameSystem.GameInput.GetCount(GameInput.Code.INPUT_DECIDE) == 1) {
					timeMessage.Finish(); 
				}
			}
		}

		override public void SubDraw(){
			string[] line = timeMessage.GetMessage();
			for (int i = 0; i < line.Length; i++){
				if(line[i] == null) continue;
				 Drawer.DrawString(px+STRING_X, py+STRING_Y + LINE_HEIGHT*i, line[i], new GameColor(0,0,0), "DEBUG_PFONT");
			}
		}
	}
}
