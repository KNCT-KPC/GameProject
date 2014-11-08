using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project1 {
	//フォントデータを管理するクラス

	//フォントの登録方法 :
	// フォントデータ管理配列に、フォントデータを追加する
	// 構文 : new FontData("登録名", "フォントネーム", 文字の大きさ, 文字の太さ[-1でデフォルト], アンチエイリアスをするかどうか),
	//
	// デフォルトの文字の太さは6ptです。
	//
	// 例　 : MS ゴシック 16pt をデフォルトの太さで、アンチエイリアスなしで登録
	//		  名前は"SystemFont"
	// new FontData("SystemFont","MS ゴシック",16,6,false),
	static class GameFont{

		//======================
		//| 画像データ管理配列 |
		//======================
		private static FontData[] fontData = new FontData[]{
			//ここに追加していく
			new FontData("SystemFont","MS ゴシック",16,6,false),
		};



		//*************************
		//*　以下は変更しないこと *
		//*************************

		private struct FontData{
			public string name;
			public string fontName;
			public int size;
			public int thick;
			public bool antiAlias;

			public FontData(string name, string fontName, int size, int thick, bool antiAlias){
				this.name = name;
				this.fontName = fontName;
				this.size = size;
				this.thick = thick;
				this.antiAlias = antiAlias;
			}
		}
		private static Dictionary<string, int> fontTable = new Dictionary<string,int>();
		public static void LoadFont(){
			foreach(var f in fontData){
				int type = DxLibDLL.DX.DX_FONTTYPE_ANTIALIASING;
				if(!f.antiAlias) type = DxLibDLL.DX.DX_FOGMODE_NONE;

				fontTable.Add(f.name, DxLibDLL.DX.CreateFontToHandle(f.fontName, f.size, f.thick, type));
			}
		}
		public static int GetFont(string name){
			return fontTable[name];
		}
	}
}
