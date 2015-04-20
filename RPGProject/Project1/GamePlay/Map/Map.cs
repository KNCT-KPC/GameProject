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
        public const int TIP_SIZE = 32;
        public const int SCREEN_XSIZE = 20;
        public const int SCREEN_YSIZE = 15;
        public int xSize;
        public int ySize;
        int[,] drawTip;
        int[,] stateTip;
        int scX;
        int scY;
        MapMychar myChar;

		public Map(int arg_xSize, int arg_ySize)
        {
            xSize = arg_xSize;
            ySize = arg_ySize;
            drawTip = new int[ySize, xSize];
            stateTip = new int[ySize, xSize];
            myChar = new  MapMychar(this, 0, 0);
        }

		public void Update()
		{
			myChar.Update();
			myChar.GetScreenCenterPosition(out scX, out scY);

			scX -= TIP_SIZE * SCREEN_XSIZE / 2;
			scY -= TIP_SIZE * SCREEN_YSIZE / 2;
			if(scX < 0) scX = 0;
			if(scX > (xSize - SCREEN_XSIZE)*TIP_SIZE) scX = (xSize - SCREEN_XSIZE)*TIP_SIZE;
			if(scY < 0) scY = 0;
			if(scY > (ySize - SCREEN_YSIZE)*TIP_SIZE) scY = (ySize - SCREEN_YSIZE)*TIP_SIZE;
		}
		public void Draw()
		{
			int maxY = scY/TIP_SIZE+SCREEN_YSIZE+1;
			int maxX = scX/TIP_SIZE+SCREEN_XSIZE+1;
			if(maxY >= ySize) maxY = ySize;
 			if(maxX >= xSize) maxX = xSize;

			for (int y = scY/TIP_SIZE; y < maxY; y++)
			{
				for (int x = scX/TIP_SIZE; x < maxX; x++)
				{
					string graphName = "";
				    if (drawTip[y, x] == 0)
				    {
						graphName = "TEST_FLOOR_IMG";
				    }
				    else if (drawTip[y, x] == 1)
				    {
						graphName = "TEST_OBJECT_IMG";
				    }
					DrawOnDisplay(x * TIP_SIZE, y * TIP_SIZE, TIP_SIZE, TIP_SIZE, graphName);
				}
			}
			myChar.Draw();
		}
		public void DrawOnDisplay(int x, int y, int width, int height, string graphName, int alpha = 255){
			int dx = x - scX;
			int dy = y - scY;
			if(dx < -width || dx > SCREEN_XSIZE*TIP_SIZE || dy < -height || dy > SCREEN_YSIZE*TIP_SIZE) return;

			Drawer.DrawGraph(dx, dy, graphName, true, alpha);
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
        public int GetScreenX(){
            return scX;
            }
        public int GetScreenY(){
            return scY;
            }
       }    
}

