using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project1 {
	//画像データを管理するクラス

	//画像の登録方法 :
	// 画像データ管理配列に、画像データを追加する
	// 構文 : new GraphData("画像の登録名","画像のファイル名"),
	//
	// 例　 : imgというフォルダにあるenemy.pngを、enemyという名前で登録
	// new GraphData("enemy","img/enemy.png"),
	static class GameGraph{

		//======================
		//| 画像データ管理配列 |
		//======================
		private static GraphData[] graphData = new GraphData[]{
		};



		//*************************
		//*　以下は変更しないこと *
		//*************************

		private struct GraphData{
			public string name;
			public string filePath;

			public GraphData(string name, string filePath){
				this.name = name;
				this.filePath = filePath;
			}
		}
		private static Dictionary<string, int> graphTable = new Dictionary<string,int>();
		public static void LoadGraph(){
			foreach(var g in graphData){
				graphTable.Add(g.name, DxLibDLL.DX.LoadGraph(g.filePath));
			}
		}
		public static int GetGraph(string name){
			int result;
			bool exist = graphTable.TryGetValue(name, out result);

			if(!exist){
				Program.AssertExit("登録されていない画像" + name + "が指定されました。");
			}

			return result;
		}
	}
}
