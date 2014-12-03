using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPGProject.GameSystem;
using DxLibDLL;

namespace RPGProject.GamePlay.Map
{
    class Map
    {
        int xSize;
        int ySize;
        int px;
        int py;
        int[,] drawTip;
        int[,] stateTip;
        public void Map(int arg_xSize, int arg_ySize)
        {
            xSize = arg_xSize;
            ySize = arg_ySize;
            drawTip = new int[xSize, ySize];
        }
        public void Update()
        {
            int mx;
            int my;
            if (Input.GetCount(Input.Code.Up) > 0)
            {
                my = py - 1;
            }
            if (Input.GetCount(Input.Code.Down) > 0)
            {
                my = py + 1;
            }
            if (Input.GetCount(Input.Code.Left) > 0)
            {
                mx = px - 1;
            }
            if (Input.GetCount(Input.Code.Right) > 0)
            {
                mx = px + 1;
            }
            if ((mx < 0) || (my < 0) || (mx >= xSize) || (my >= ySize))
            {
                my = py;
                mx = px;
            }
            if (stateTip[my, mx] == 1)
            {
                my = py;	//戻す
                mx = px;	//戻す
            }
            px = mx;
            py = my;
        }
        public void Draw()
        {
            for (int y = 0; y < ySize; y++)
            {
                for (int x = 0; x < xSize; x++)
                {
                    if (drawTip[y, x] == 0)
                    {
                        DxLibDLL.DX.DrawBox(x, y, x + 32, y + 32, DX.GetColor(255, 255, 255), 1);
                    }
                    else if (drawTip[y, x] == 0)
                    {
                        DxLibDLL.DX.DrawBox(x, y, x + 32, y + 32, DX.GetColor(0, 0, 0), 1);
                    }
                }
            }
            DxLibDLL.DX.DrawBox(px, py, px + 32, py + 32, DX.GetColor(0, 255, 0), 1);
        }
    }
}
