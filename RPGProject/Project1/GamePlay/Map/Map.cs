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
        public int xSize;
        public int ySize;
        int[,] drawTip;
        int[,] stateTip;
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
            drawTip[3, 1] = 1;
            stateTip[3,1] = 1;
            myChar = new  MapMychar(this, 0, 0);
        }
        public void Update()
        {
            myChar.Update();
        }
        public void Draw()
        {
            
            for (int y = 0; y < ySize; y++)
            {
                for (int x = 0; x < xSize; x++)
                {
                    if (drawTip[y, x] == 0)
                    {
                        r = g = b = 255;

                    }
                    else if (drawTip[y, x] == 1)
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
            if ((x < 0) || (x > xSize-1) || (y < 0) || (y > ySize-1))
            {
                return (false);
            }
            if (stateTip[y, x] == 1) return (false);
            else return (true);
        }
        
       
      
           
           

       }    


    
}

