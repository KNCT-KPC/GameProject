using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project1 {
	/// <summary>
	/// キーボード入力を受け付けるクラス
	/// </summary>
	static class Keyboard {
		public enum KeyCode {
		  CODE_BACK = 14,
		  CODE_TAB = 15,
		  CODE_RETURN = 28,
		  CODE_LSHIFT = 42,
		  CODE_RSHIFT = 54,
		  CODE_LCONTROL = 29,
		  CODE_RCONTROL = 157,
		  CODE_ESCAPE = 1,
		  CODE_SPACE = 57,
		  CODE_PGUP = 201,
		  CODE_PGDN = 209,
		  CODE_END = 207,
		  CODE_HOME = 199,
		  CODE_LEFT = 203,
		  CODE_UP = 200,
		  CODE_RIGHT = 205,
		  CODE_DOWN = 208,
		  CODE_INSERT = 210,
		  CODE_DELETE = 211,
		  CODE_MINUS = 12,
		  CODE_YEN = 125,
		  CODE_PREVTRACK = 144,
		  CODE_PERIOD = 52,
		  CODE_SLASH = 53,
		  CODE_LALT = 56,
		  CODE_RALT = 184,
		  CODE_SCROLL = 70,
		  CODE_SEMICOLON = 39,
		  CODE_COLON = 146,
		  CODE_LBRACKET = 26,
		  CODE_RBRACKET = 27,
		  CODE_AT = 145,
		  CODE_BACKSLASH = 43,
		  CODE_COMMA = 51,
		  CODE_KANJI = 148,
		  CODE_CONVERT = 121,
		  CODE_NOCONVERT = 123,
		  CODE_KANA = 112,
		  CODE_APPS = 221,
		  CODE_CAPSLOCK = 58,
		  CODE_SYSRQ = 183,
		  CODE_PAUSE = 197,
		  CODE_LWIN = 219,
		  CODE_RWIN = 220,
		  CODE_NUMLOCK = 69,
		  CODE_NUMPAD0 = 82,
		  CODE_NUMPAD1 = 79,
		  CODE_NUMPAD2 = 80,
		  CODE_NUMPAD3 = 81,
		  CODE_NUMPAD4 = 75,
		  CODE_NUMPAD5 = 76,
		  CODE_NUMPAD6 = 77,
		  CODE_NUMPAD7 = 71,
		  CODE_NUMPAD8 = 72,
		  CODE_NUMPAD9 = 73,
		  CODE_MULTIPLY = 55,
		  CODE_ADD = 78,
		  CODE_SUBTRACT = 74,
		  CODE_DECIMAL = 83,
		  CODE_DIVIDE = 181,
		  CODE_NUMPADENTER = 156,
		  CODE_F1 = 59,
		  CODE_F2 = 60,
		  CODE_F3 = 61,
		  CODE_F4 = 62,
		  CODE_F5 = 63,
		  CODE_F6 = 64,
		  CODE_F7 = 65,
		  CODE_F8 = 66,
		  CODE_F9 = 67,
		  CODE_F10 = 68,
		  CODE_F11 = 87,
		  CODE_F12 = 88,
		  CODE_A = 30,
		  CODE_B = 48,
		  CODE_C = 46,
		  CODE_D = 32,
		  CODE_E = 18,
		  CODE_F = 33,
		  CODE_G = 34,
		  CODE_H = 35,
		  CODE_I = 23,
		  CODE_J = 36,
		  CODE_K = 37,
		  CODE_L = 38,
		  CODE_M = 50,
		  CODE_N = 49,
		  CODE_O = 24,
		  CODE_P = 25,
		  CODE_Q = 16,
		  CODE_R = 19,
		  CODE_S = 31,
		  CODE_T = 20,
		  CODE_U = 22,
		  CODE_V = 47,
		  CODE_W = 17,
		  CODE_X = 45,
		  CODE_Y = 21,
		  CODE_Z = 44,
		  CODE_0 = 11,
		  CODE_1 = 2,
		  CODE_2 = 3,
		  CODE_3 = 4,
		  CODE_4 = 5,
		  CODE_5 = 6,
		  CODE_6 = 7,
		  CODE_7 = 8,
		  CODE_8 = 9,
		  CODE_9 = 10,	
		}

		/// <summary>
		/// 与えられたキーコードに対応したキーが、どれだけ押されているかを返すメソッド
		/// </summary>
		/// <param name="code">キーコード</param>
		/// <returns>押されているフレーム数</returns>
		public static int GetCount(KeyCode code){
			return countBuf[(int)code];
		}


		private static int[] countBuf = new int[256];	
		public static void Update(){
			foreach(var e in Enum.GetValues(typeof(KeyCode))){
				if(DxLibDLL.DX.CheckHitKey((int)e) == 1){
					countBuf[(int)e]++;
				} else countBuf[(int)e] = 0;
			}
		}
	}
}
