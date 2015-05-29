using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DxLibDLL;

namespace RPGProject {
	class Fps{
		private int mStartTime;         //測定開始時刻
		private int mCount;             //カウンタ
		private float mFps;             //fps
		private static int N = 60;//平均を取るサンプル数
		private static int FPS = 60;	//設定したFPS

		public Fps(){
			mStartTime = 0;
			mCount = 0;
			mFps = 0;
		}

		public bool Update(){
			if( mCount == 0 ){ //1フレーム目なら時刻を記憶
				mStartTime = DX.GetNowCount();
			}
			if( mCount == N ){ //60フレーム目なら平均を計算する
				int t = DX.GetNowCount();
				mFps = ((float)(1000.0))/((t-mStartTime)/(float)N);
				mCount = 0;
				mStartTime = t;
			}
			mCount++;
			return true;
		}

		public void Draw(){
			DX.DrawString(0, 0,  "" + mFps, DX.GetColor(255,255,255));
		}
	}
}
