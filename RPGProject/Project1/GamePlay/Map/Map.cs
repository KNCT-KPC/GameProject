﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPGProject.GameSystem;
using DxLibDLL;

namespace RPGProject.GamePlay.Map
{
    class Map
    {
		const int TIP_SIZE = 32;
        int xSize;
        int ySize;
        int px;
        int py;
        int[,] drawTip;
        int[,] stateTip;

        public Map(int arg_xSize, int arg_ySize)
        {
            xSize = arg_xSize;
            ySize = arg_ySize;
            drawTip = new int[ySize, xSize];
			stateTip = new int[ySize, xSize];
		}
        public void Update()
        {
            int mx = px;
            int my = py;
            if (GameInput.GetCount(GameInput.Code.INPUT_UP) > 0)
            {
                my = py - 1;
            }
            if (GameInput.GetCount(GameInput.Code.INPUT_DOWN) > 0)
            {
                my = py + 1;
            }
            if (GameInput.GetCount(GameInput.Code.INPUT_LEFT) > 0)
            {
                mx = px - 1;
            }
            if (GameInput.GetCount(GameInput.Code.INPUT_RIGHT) > 0)
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
                        DxLibDLL.DX.DrawBox(x*TIP_SIZE, y*TIP_SIZE, x*TIP_SIZE + TIP_SIZE, y*TIP_SIZE + TIP_SIZE, DX.GetColor(255, 255, 255), 1);
                    }
                    else if (drawTip[y, x] == 1)
                    {
                        DxLibDLL.DX.DrawBox(x*TIP_SIZE, y*TIP_SIZE, x*TIP_SIZE + TIP_SIZE, y*TIP_SIZE + TIP_SIZE, DX.GetColor(0, 0, 0), 1);
                    }
                }
            }
            DxLibDLL.DX.DrawBox(px*TIP_SIZE, py*TIP_SIZE, px*TIP_SIZE + TIP_SIZE, py*TIP_SIZE + TIP_SIZE, DX.GetColor(0, 255, 0), 1);
        }
    }
}
