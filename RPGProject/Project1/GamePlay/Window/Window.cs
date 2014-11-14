using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPGProject.GameSystem;
using DxLibDLL;

namespace RPGProject.GamePlay.Window
{
    // <summary>
    //  なにもない空白のウィンドウを表示させるクラス
    // </summary>


    class Window
    {
        public int px;  //左上座標
        public int py;  //右上座標
        int width;      //幅
        int height;     //高さ
        bool broken;    //破棄されているかどうか

        public Window(int arg_px, int arg_py, int arg_width, int arg_height)
        {
            px = arg_px;
            py = arg_py;
            width = arg_width;
            height = arg_height;
            broken = false;
        }

        public void Update()
        //更新させるメソッド
        {

            if (GameSystem.GameInput.GetCount(GameInput.Code.INPUT_DECIDE) >= 1)
            {
                broken = true;
            }

        }

        public void Draw()
        //四角形を描画するメソッド。 (左上の座標(px,py)に wid)
        {

            DxLibDLL.DX.DrawBox(px, py, px + width - 1, py + height - 1, DX.GetColor(255, 255, 255), 1);
        }

        public bool isBroken()
        //破棄されているかどうか
        {
            return broken;
        }
        public void Break()
        //破棄するメソッド
        {
            broken = true;
        }
    }
}
