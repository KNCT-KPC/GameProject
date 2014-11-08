using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project1 {
	//音声データを管理するクラス

	//音声の登録方法 :
	// 音声データ管理配列に、音声データを追加する
	// 構文 : new SoundData("音声の登録名","音声のファイル名"),
	//
	// 例　 : soundというフォルダにあるbell15b.wavを、334という名前で登録
	// new SoundData("334","sound/bell15b.wav"),
	static class GameSound{

		//======================
		//| 画像データ管理配列 |
		//======================
		private static SoundData[] soundData = new SoundData[]{
			//ここに追加していく
		};



		//*************************
		//*　以下は変更しないこと *
		//*************************

		private struct SoundData{
			public string name;
			public string filePath;

			public SoundData(string name, string filePath){
				this.name = name;
				this.filePath = filePath;
			}
		}

		private static Dictionary<string, int> soundTable = new Dictionary<string,int>();
		public static void LoadSound(){
			foreach(var s in soundData){
				soundTable.Add(s.name, DxLibDLL.DX.LoadSoundMem(s.filePath));
			}
		}
		public static int GetSound(string name){
			int result;
			bool exist = soundTable.TryGetValue(name, out result);

			if(!exist){
				Program.AssertExit("登録されていない音声" + name + "が指定されました。");
			}

			return result;
		}
	}
}
