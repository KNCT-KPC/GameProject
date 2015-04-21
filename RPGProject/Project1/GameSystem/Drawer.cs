using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DxLibDLL;

namespace RPGProject.GameSystem {
	/// <summary>
	/// rgb値を指定して色を得るクラス
	/// </summary>
	public struct GameColor{
		public readonly int r;
		public readonly int g;
		public readonly int b;
		public readonly int value;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="r">赤値</param>
		/// <param name="g">緑値</param>
		/// <param name="b">青値</param>
		public GameColor(int r, int g, int b){
			this.r = r;
			this.g = g;
			this.b = b;
			this.value = DX.GetColor(r,g,b);
		}
	}

	static class Drawer {
		/// <summary>
		/// 画像を描画するメソッド
		/// </summary>
		/// <param name="x">描画x位置</param>
		/// <param name="y">描画y位置</param>
		/// <param name="graphName">登録画像名</param>
		/// <param name="trans">背景を透過するかどうか</param>
		/// <param name="alpha">画像の透過具合 255で透過しない 0で完全に透過</param>
		public static void DrawGraph(int x, int y, string graphName, bool trans, int alpha = -1){
			int transFlag = 1;
			if(!trans) transFlag = 0;
			
			if(alpha != -1) DX.SetDrawBlendMode(DX.DX_BLENDMODE_ALPHA, alpha);
			DX.DrawGraph(x, y, GameGraph.GetGraph(graphName), transFlag);
			if(alpha != -1) DX.SetDrawBlendMode(DX.DX_BLENDMODE_NOBLEND, 0);
		}



		/// <summary>
		/// 画像を拡縮・回転させて描画するメソッド
		/// </summary>
		/// <param name="x">描画中心x位置</param>
		/// <param name="y">描画中心y位置</param>
		/// <param name="exRate">拡大率 1.0で通常</param>
		/// <param name="angle">回転ラジアン角</param>
		/// <param name="graphName">登録画像名</param>
		/// <param name="trans">背景を透過するかどうか</param>
		/// <param name="alpha">画像の透過具合 255で透過しない/0で完全に透過</param>
		public static void DrawRotaGraph(int x, int y, double exRate, double angle, string graphName, bool trans, int alpha = -1){
			int transFlag = 1;
			if(!trans) transFlag = 0;

			if(alpha != -1) DX.SetDrawBlendMode(DX.DX_BLENDMODE_ALPHA, alpha);
			DX.SetDrawMode(DX.DX_DRAWMODE_BILINEAR);
			DX.DrawRotaGraph(x, y, exRate, angle, GameGraph.GetGraph(graphName), transFlag);
			DX.SetDrawMode(DX.DX_DRAWMODE_NEAREST);
			if(alpha != -1) DX.SetDrawBlendMode(DX.DX_BLENDMODE_NOBLEND, 0);
		}



		/// <summary>
		/// 画像を拡縮・回転させて描画するメソッド　引数として度数を与える
		/// </summary>
		/// <param name="x">描画中心x位置</param>
		/// <param name="y">描画中心y位置</param>
		/// <param name="exRate">拡大率 1.0で通常</param>
		/// <param name="angle">回転角（度数法）</param>
		/// <param name="graphName">登録画像名</param>
		/// <param name="trans">背景を透過するかどうか</param>
		/// <param name="alpha">画像の透過具合 255で透過しない/0で完全に透過</param>
		public static void DrawRotaGraphBy360(int x, int y, double exRate, double angle, string graphName, bool trans, int alpha = -1){
			int transFlag = 1;
			if(!trans) transFlag = 0;

			double draw_angle = angle/180 * Math.PI;

			if(alpha != -1) DX.SetDrawBlendMode(DX.DX_BLENDMODE_ALPHA, alpha);
			DX.SetDrawMode(DX.DX_DRAWMODE_BILINEAR);
			DX.DrawRotaGraph(x, y, exRate, draw_angle, GameGraph.GetGraph(graphName), transFlag);
			DX.SetDrawMode(DX.DX_DRAWMODE_NEAREST);
			if(alpha != -1) DX.SetDrawBlendMode(DX.DX_BLENDMODE_NOBLEND, 0);
		}



		/// <summary>
		/// 文字列を描画するメソッド
		/// </summary>
		/// <param name="x">描画x位置</param>
		/// <param name="y">描画y位置</param>
		/// <param name="str">描画文字列</param>
		/// <param name="col">描画色オブジェクト</param>
		/// <param name="fontName">登録フォント名</param>
		public static void DrawString(int x, int y, string str, GameColor col, string fontName){
			DX.DrawStringToHandle(x, y, str, col.value, GameFont.GetFont(fontName));
		}



		/// <summary>
		/// 四角形を描画するメソッド
		/// </summary>
		/// <param name="x">描画x位置</param>
		/// <param name="y">描画y位置</param>
		/// <param name="width">四角形の横幅</param>
		/// <param name="height">四角形の縦幅</param>
		/// <param name="col">描画色オブジェクト</param>
		/// <param name="fill">塗りつぶすかどうか</param>
		/// <param name="alpha">画像の透過具合 255で透過しない 0で完全に透過</param>
		public static void DrawRect(int x, int y, int width, int height, GameColor col, bool fill, int alpha = -1){
			int fillFlag = 1;
			if(!fill) fillFlag = 0;

			if(alpha != -1) DX.SetDrawBlendMode(DX.DX_BLENDMODE_ALPHA, alpha);
			DX.DrawBox(x, y, x+width, y+height, col.value, fillFlag);
			if(alpha != -1) DX.SetDrawBlendMode(DX.DX_BLENDMODE_NOBLEND, 0);
		}



		/// <summary>
		/// 円を描画するメソッド
		/// </summary>
		/// <param name="x">描画中心x位置</param>
		/// <param name="y">描画中心y位置</param>
		/// <param name="r">円の半径</param>
		/// <param name="col">描画色オブジェクト</param>
		/// <param name="fill">塗りつぶすかどうか</param>
		/// <param name="alpha">画像の透過具合 255で透過しない 0で完全に透過</param>
		public static void DrawCircle(int x, int y, int r, GameColor col, bool fill, int alpha = -1){
			int fillFlag = 1;
			if(!fill) fillFlag = 0;

			if(alpha != -1) DX.SetDrawBlendMode(DX.DX_BLENDMODE_ALPHA, alpha);
			DX.DrawCircle(x, y, r, col.value,fillFlag);
			if(alpha != -1) DX.SetDrawBlendMode(DX.DX_BLENDMODE_NOBLEND, 0);
		}



		/// <summary>
		/// 線を描画するメソッド
		/// </summary>
		/// <param name="x1">始点x</param>
		/// <param name="y1">始点y</param>
		/// <param name="x2">終点x</param>
		/// <param name="y2">終点y</param>
		/// <param name="col">描画色オブジェクト</param>
		public static void DrawLine(int x1, int y1, int x2, int y2, GameColor col){
			DX.DrawLine(x1,y1,x2,y2,col.value);
		}




		/// <summary>
		/// 画像データの横幅を得るメソッド
		/// </summary>
		/// <param name="graphName">登録画像名</param>
		public static int GetGraphWidth(string graphName){
			int SizeXBuf,SizeYBuf;
			DxLibDLL.DX.GetGraphSize(GameGraph.GetGraph(graphName), out SizeXBuf, out SizeYBuf);		
			return SizeXBuf;
		}

		/// <summary>
		/// 画像データの縦幅を得るメソッド
		/// </summary>
		/// <param name="graphName">登録画像名</param>
		public static int GetGraphHeight(string graphName){
			int SizeXBuf,SizeYBuf;
			DxLibDLL.DX.GetGraphSize(GameGraph.GetGraph(graphName), out SizeXBuf, out SizeYBuf);		
			return SizeYBuf;
		}

		/// <summary>
		/// 画像の横幅・縦幅を得るメソッド
		/// </summary>
		/// <param name="graphName">登録画像名</param>
		/// <param name="widthBuf">横幅バッファ</param>
		/// <param name="heightBuf">縦幅バッファ</param>
		public static void GetGraphSize(string graphName, out int widthBuf, out int heightBuf){
			DxLibDLL.DX.GetGraphSize(GameGraph.GetGraph(graphName), out widthBuf, out heightBuf);		
		}

		/// <summary>
		/// 描画可能領域を変更するメソッド
		/// </summary>
		/// <param name="x">領域左上x位置</param>
		/// <param name="y">領域左上y位置</param>
		/// <param name="width">横幅</param>
		/// <param name="height">縦幅</param>
		public static void SetDrawArea(int x, int y, int width, int height){
			DxLibDLL.DX.SetDrawArea(x, y, x+width, y+height);
		}

		public static int[,] DivGraph(string graphName, int width, int height, int xNum, int yNum){
			int handle = GameGraph.GetGraph(graphName);	

			int[,] result = new int[yNum,xNum];
			for(int y = 0; y < yNum; y++){
				for(int x = 0; x < xNum; x++){
					result[y,x] = DxLibDLL.DX.DerivationGraph(x*width, y*height, width, height, handle);	
				}
			}

			return result;
		}

		public static void DrawDivGraph(int[,] handle, int imgX, int imgY, int x, int y, bool trans){
			int transFlag = 1;
			if(!trans) transFlag = 0;

			DxLibDLL.DX.DrawGraph(x, y, handle[imgY,imgX], transFlag);
		}
	}
}
