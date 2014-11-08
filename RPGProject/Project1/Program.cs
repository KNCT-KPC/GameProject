using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DxLibDLL;

//================================
//|                              |
//| 釧路高専プログラミング同好会 |
//|                              |
//================================

//**************************************
//* このファイルの内容は変更しないこと *
//**************************************

//********************************************************
//* 使用 : DXライブラリ.net                              *
//* http://homepage2.nifty.com/natupaji/DxLib/index.html *
//* DX Library Copyright (C) 2001-2014 Takumi Yamada.    *
//********************************************************

namespace Project1 {
	class Program {
		/// <summary>
		/// メインメソッド
		/// </summary>
		public static void Main(){
			DX.ChangeWindowMode(DX.TRUE);	//ウィンドウモードに

			if(DX.DxLib_Init() == -1){		//DXライブラリの初期化
				AssertExit("DXライブラリの初期化に失敗しました。");
				//初期化に失敗したら、エラー表示して終了
			}
			DX.SetDrawScreen( DX.DX_SCREEN_BACK );	//ダブルバッファのためのセッタ

			GameGraph.LoadGraph();	//画像のロード
			GameSound.LoadSound();	//音声のロード
			GameFont.LoadFont();	//フォントのロード
	
			GameMain main = new GameMain();
			while(true){
				if(!LoopStart()) break;	//初期化処理
				Keyboard.Update();

				if(main.Update() != 0) break;	//更新メソッド
				main.Draw();					//描画メソッド
			}

			DX.DxLib_End();	//DXライブラリの終了メソッド
		}



		/// <summary>
		/// ゲーム本体の開始処理を行う。
		/// </summary>
		/// <returns>ループを継続するかどうか</returns>
		private static bool LoopStart(){
			if(DX.ScreenFlip() == -1) return false;			//裏画面処理
			if(DX.ProcessMessage() == -1) return false;		//Windowsへのシステム応答
			if(DX.ClearDrawScreen() == -1) return false;	//画面の初期化

			return true;
		}



		/// <summary>
		/// アプリケーションを強制終了する。
		/// </summary>
		/// <param name="message">表示するエラーメッセージ</param>
		public static void AssertExit(String message){
			if(message != null){
				MessageBox.Show(message);
			} else {
				MessageBox.Show("予期せぬエラーが発生しました。");
			}

			DX.DxLib_End();			//DXライブラリの終了処理
			Environment.Exit(0);	//強制終了
		}
	}
}
