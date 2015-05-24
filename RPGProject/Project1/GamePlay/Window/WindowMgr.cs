using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPGProject.GamePlay.Window
{
    class WindowMgr
    {
		Stack<Window> windows = new Stack<Window>(0);

		/// <summary>
		/// Windowsのすべての要素に対してUpdateを行うメソッド
		/// </summary>
		public int Update(){
			if(windows.Count != 0){
				windows.Peek().Update();

				if(windows.Peek().isBroken()){
					windows.Pop();
				}
			}

			return -1;
        }

        /// <summary>
        /// 描画メソッド
        /// </summary>
        public void Draw()
        {
			foreach(var w in windows){
				w.Draw();
            }
        }

        /// <summary>
        /// Windowの要素が一つでもあるかどうか
        /// </summary>
        /// <returns></returns>
        public bool isEnable()
        {
			if(windows.Count != 0){
				return windows.Peek().isEnable();
			}
            return false;
        }
        /// <summary>
        /// ウィンドウを配列の最後に追加するメソッド
        /// </summary>
        /// <param name="w">Window w</param>
        public void addWindow(Window w)
        {
			windows.Push(w);
        }
        /// <summary>
        /// Window配列にある与えたWindowの値を削除するメソッド(オーバーロード)
        /// </summary>
        /// <param name="w">削除するWindow</param>
        public void removeWindow(Window w)//overload
        {
			/*
            for (int i = 0; i < 32; i++)
            {
                if (w == windows[i])
                {
                    windows[i] = null;
                    packWindowArr(windows);
                }
            }
			*/
        }

		public void RemoveAllWindow(){
			windows.Clear();
		}
        /// <summary>
        /// Windowsのindex番目のインスタンスを削除する(オーバーロード)
        /// </summary>
        /// <param name="index">削除するWindow配列の番号</param>
        public void removeWindow(int index) //overload
        {
			/*
            windows[index] = null;
            packWindowArr(windows);
			*/
        }
    }
}
