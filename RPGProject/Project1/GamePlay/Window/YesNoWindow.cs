using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPGProject.GameSystem;

namespace RPGProject.GamePlay.Window {
	class YesNoWindow : Window {
		const int STRING_X = 40;		//1文字目のx座標
		const int CURSOR_X = 10;		//1文字目のx座標
		const int STRING_Y = 15;		//1文字目のy座標
		const int LINE_HEIGHT = 35;		//1行あたりのy幅
		int select = 0;

		public YesNoWindow()  : base(510,250,120,80){
		}
		public YesNoWindow(int arg_px,int arg_py, int arg_width, int arg_height)  : base(arg_px,arg_py,arg_width,arg_height){
		}

		override public void Update(){
			if(GameInput.GetCount(GameInput.Code.INPUT_UP) == 1){
				select = 0;
				//select = (select+3)%2;
			}
			if(GameInput.GetCount(GameInput.Code.INPUT_DOWN) == 1){
				select = 1;
				//select = (select+1)%2;
			}
		}

		override public void SubDraw(){
			int crs_adjy = 0;
			if(select == 1){
				crs_adjy += LINE_HEIGHT;
			}
			Drawer.DrawString(px+CURSOR_X, py+STRING_Y+crs_adjy, "→", new GameColor(0,0,0), "DEBUG_PFONT");

			Drawer.DrawString(px+STRING_X, py+STRING_Y, "はい", new GameColor(0,0,0), "DEBUG_PFONT");
			Drawer.DrawString(px+STRING_X, py+STRING_Y+LINE_HEIGHT, "いいえ", new GameColor(0,0,0), "DEBUG_PFONT");
		}
	}
}
