﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project1 {
	//ゲームのプログラムを記述するクラス
	class GameMain {
		int Func1(int a, int b){
			if(a < 0) return b;
			if(b < 0) return a;
			return a*b;
		}


		/// <summary>
		/// 更新メソッド
		/// 状態の更新や演算処理などを記述する
		/// 
		/// 戻り値として0以外を返すとゲームが終了する
		/// </summary>
		public int Update(){
			return 0;
		}

		/// <summary>
		/// 描画メソッド
		/// 画像の描画を行う
		/// </summary>
		public void Draw(){
		}
	}
}
