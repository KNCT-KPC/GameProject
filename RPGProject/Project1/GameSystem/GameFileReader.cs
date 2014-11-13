using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DxLibDLL;

namespace RPGProject.GameSystem {
	/// <summary>
	/// ファイル読み込み機能を提供するクラス
	/// このクラスはアーカイバにまとめられたファイルを読み込むことができる
	/// </summary>
	class GameFileReader {
		/// <summary>
		/// ファイルパス
		/// </summary>
		readonly public string FilePath;
		private int fileHandle;	//ファイルハンドル



		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="filePath">読み込むファイル名</param>
		public GameFileReader(string filePath){
			FilePath = filePath;
			fileHandle = DX.FileRead_open(filePath);

			if(fileHandle == 0){
				Program.AssertExit("不正なファイル" + filePath + "が指定されました。");
			}
		}

		/// <summary>
		/// デストラクタ（内部でファイルクローズが呼び出されている）
		/// </summary>
		~GameFileReader(){
			if(fileHandle != -1){
				DX.FileRead_close(fileHandle);
			}
		}

		/// <summary>
		/// ファイルからbyteだけ読み込むメソッド
		/// </summary>
		/// <returns>読み込んだbyteデータ</returns>
		public byte ReadByte(){
			if(fileHandle == -1){
				FileisAlreadyClosed();
			}

			byte result = 0;
			unsafe{
				DX.FileRead_read(&result, sizeof(byte), fileHandle);
			}
			return result;
		}
		
		/// <summary>
		/// ファイルからintだけ読み込むメソッド
		/// </summary>
		/// <returns>読み込んだintデータ</returns>
		public int ReadInt(){
			if(fileHandle == -1){
				FileisAlreadyClosed();
			}

			int result = 0;
			unsafe{
				DX.FileRead_read(&result, sizeof(int), fileHandle);
			}
			return result;
		}

		/// <summary>
		/// ファイルからdoubleだけ読み込むメソッド
		/// </summary>
		/// <returns>読み込んだdoubleデータ</returns>
		public double ReadDouble(){
			if(fileHandle == -1){
				FileisAlreadyClosed();
			}

			double result = 0;
			unsafe{
				DX.FileRead_read(&result, sizeof(double), fileHandle);
			}
			return result;
		}

		/// <summary>
		/// ファイルから一行読み込むメソッド
		/// </summary>
		/// <param name="lineSize">読み込む文字列の最大長</param>
		/// <returns>読み込んだ文字列</returns>
		public string ReadLine(int lineSize){
			if(fileHandle == -1){
				FileisAlreadyClosed();
			}

			StringBuilder input = new StringBuilder(0);
			DX.FileRead_gets(input, lineSize, fileHandle);
			return input.ToString();
		}

		/// <summary>
		/// ファイルを自発的に閉じるメソッド
		/// </summary>
		public void Close(){
			if(fileHandle == -1){
				FileisAlreadyClosed();
			}

			DX.FileRead_close(fileHandle);
			fileHandle = -1;
		}

		private void FileisAlreadyClosed(){
			Program.AssertExit("閉じられたファイル" + FilePath + "の内容を読み込もうとしました。");
		}
	}
}
