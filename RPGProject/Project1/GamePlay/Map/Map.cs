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
        const int TIP_SIZE = 32;
        const int SCREEN_XSIZE = 20;
        const int SCREEN_YSIZE = 15;
        public int xSize;
        public int ySize;
        int[,] drawTip;
        int[,] stateTip;
        int scX;
        int scY;
        MapMychar myChar;
        public int r, g, b;
        public Map(int arg_xSize, int arg_ySize)
        {
            xSize = arg_xSize;
            ySize = arg_ySize;
            drawTip = new int[ySize, xSize];
            stateTip = new int[ySize, xSize];
            drawTip[1, 0] = 0;
            drawTip[0, 0] = 0;
            drawTip[3, 5] = 1;
            stateTip[3,1] = 1;
            drawTip[10, 1] = 1;
            myChar = new  MapMychar(this, 0, 0);
        }
        public void Update()
        {
            myChar.Update();
            
            scX = myChar.GetX() - SCREEN_XSIZE / 2;
            if(scX < 0) scX = 0;
            else if(scX >= xSize-SCREEN_XSIZE) scX = xSize-SCREEN_XSIZE;
            scY = myChar.GetY() - SCREEN_YSIZE / 2;
            if (scY < 0) scY = 0;
            else if (scY >= ySize - SCREEN_YSIZE) scY = ySize - SCREEN_YSIZE;
        }
        public void Draw()
        {
            
            for (int y = 0; y < SCREEN_YSIZE; y++)
            {
                for (int x = 0; x < SCREEN_XSIZE; x++)
                {
                    if (drawTip[y + scY, x + scX] == 0)
                    {
                        r = g = b = 255;

                    }
                    else if (drawTip[y + scY, x + scX] == 1)
                    {
                        r = g = b = 0;
                    }
                    DxLibDLL.DX.DrawBox(x * TIP_SIZE, y * TIP_SIZE, x * TIP_SIZE + TIP_SIZE, y * TIP_SIZE + TIP_SIZE, DX.GetColor(r, g, b), 1);
                }
            }
            myChar.Draw();
        }
        public bool JudgeEnter(int x, int y)
        {
            if ((x < 0) || (x > xSize-scX-1) || (y < 0) || (y > ySize-scY-1))
            {
                return (false);
            }
            if (stateTip[y, x] == 1) return (false);
            else return (true);
        }
        public int GetScreenX(){
            return scX;
            }
        public int GetScreenY(){
            return scY;
            }
       }    
}

