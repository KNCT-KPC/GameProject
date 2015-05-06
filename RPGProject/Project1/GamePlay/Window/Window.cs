using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPGProject.GameSystem;

namespace RPGProject.GamePlay.Window
{
	/// <summary>
	///  なにもない空白のウィンドウを表示させるクラス
	/// </summary>
	class Window 
	{
		public int px;	//左上座標
		public int py;	//右上座標
		int width;		//幅
		int height;		//高さ
		bool broken;	//破棄されているかどうか

		//コンストラクタ
		public Window(int arg_px, int arg_py, int arg_width, int arg_height)
		{
			px = arg_px;
			py = arg_py;
			width = arg_width;
			height = arg_height;
			broken = false;
		}

		//更新メソッド
		virtual public void Update(){

			if (GameSystem.GameInput.GetCount(GameInput.Code.INPUT_DECIDE) >= 1)
			{
				broken = true;
			}

		}

		//描画メソッド
		virtual public void Draw(){
			DrawBox();
			SubDraw();
		}

		//四角形を描画するメソッド
		virtual public void DrawBox(){
			Drawer.DrawRect(px, py, width, height, new GameColor(255, 255, 255), true);
		}

		//サブクラス用の描画メソッド
		virtual public void SubDraw(){
		}

		//破棄されているかどうか
		public bool isBroken(){
			return broken;
		}

		//破棄するメソッド
		public void Break(){
			broken = true;
		}
	}
}
