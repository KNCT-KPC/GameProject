using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPGProject.GamePlay.Window
{
    class WindowMgr
    {

        Window[] windows = new Window[32];
        /// <summary>
        /// Windowsのすべての要素に対してUpdateを行うメソッド
        /// </summary>
        void Update()
        {

            for (int i = 0; i < 32; i++)
            {
                if (windows[i] == null) break;
                windows[i].Update();
            }

            for (int i = 0; i < 32; i++)
            {
                if (windows[i] == null) break;
                if (windows[i].isBroken()) removeWindow(windows[i]);
            }
        }
        /// <summary>
        /// 描画メソッド
        /// </summary>
        void Draw()
        {
            for (int i = 0; i < 32; i++)
            {
                if (windows[i] == null) break;
                windows[i].Draw();
            }
        }
        /// <summary>
        /// Windowの要素が一つでもあるかどうか
        /// </summary>
        /// <returns></returns>
        bool isEnable()
        {
            for (int i = 0; i < 32; i++)
            {
                if (windows[i] != null) return true;
            }

            return false;
        }
        /// <summary>
        /// ウィンドウを配列の最後に追加するメソッド
        /// </summary>
        /// <param name="w">Window w</param>
        void addWindow(Window w)
        {
            for (int i = 0; i < 32; i++)
            {
                if (windows[i] == null) windows[i] = w;
            }

        }
        /// <summary>
        /// Window配列にある与えたWindowの値を削除するメソッド(オーバーロード)
        /// </summary>
        /// <param name="w">削除するWindow</param>
        void removeWindow(Window w)//overload
        {
            for (int i = 0; i < 32; i++)
            {
                if (w == windows[i])
                {
                    windows[i] = null;
                    packWindowArr(windows);
                }
            }
        }
        /// <summary>
        /// Windowsのindex番目のインスタンスを削除する(オーバーロード)
        /// </summary>
        /// <param name="index">削除するWindow配列の番号</param>
        void removeWindow(int index) //overload
        {
            windows[index] = null;
            packWindowArr(windows);
        }
        

        // Window配列を前に詰める関数
        static void packWindowArr(Window[] w)
        {
            Window numw;
            for (int i = 0; i < w.Length - 1; i++)
            {
                if (w[i] == null)
                {
                    numw = w[i];
                    w[i] = w[i + 1];
                    w[i + 1] = numw;

                }
            }
        }
    }
}
