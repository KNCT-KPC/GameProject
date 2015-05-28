using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPGProject.GameSystem;
using DxLibDLL;
using System.IO;

using System.Collections;

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
		List<MapNPChar> mapNpc = new List<MapNPChar>(0);
        int[,] image;
		private static Dictionary<string, string> mapTable = new Dictionary<string,string>(){
			//key					value
			{"最初の村",			"map/test1.txt"},
			{"最初のダンジョン",	"map.dat"},
		};
        
		//Map map = new Map("最初の村");	
	
        public Map()
        {

            char[] cs;
            int count = 0;
            int i = 0;
            string[] sizes;
            int m = 0;
            string[] chara = { };
            string[] data = { };
            string line = "";

            ArrayList all = new ArrayList();
            string filename = mapTable["最初の村"];
            image = Drawer.DivGraph("TEST_MAP", 32, 32, 2, 2);
            StreamReader sr = new StreamReader(@"map/test1.txt");                    
            while ((line = sr.ReadLine()) != null){
                   all.Add(line);
                   count++;
            }
            sr.Close();
            
            sizes= all[i].ToString().Split(' ');
            xSize = int.Parse(sizes[0]);
            ySize = int.Parse(sizes[1]);
            drawTip = new int[ySize, xSize];
            stateTip = new int[ySize, xSize];
            for (int n = 0; n < ySize; n++){
                cs = all[n + 1].ToString().ToCharArray();
                for (int v = 0; v < xSize; v++){
                    drawTip[n, v] = int.Parse(cs[v].ToString());
                }
                i++;
            }
            for (int n = 0; n < ySize; n++){
                i++;
                cs = all[n + 1].ToString().ToCharArray();
                for (int v = 0; v < xSize; v++){
                    stateTip[n, v] = int.Parse(cs[v].ToString());
                }
            }
                        
            myChar = new MapMychar(this, 0, 0);
            
            while(all.Count  > i){
               if(m == 0){string lines = (string)all[i];
                   chara = lines.Split(' '); }
               else if(all[i].ToString() == "Scriptend"){
                   data[m] = (string)all[i];
                   mapNpc.Add(new MapNPChar(this, int.Parse(chara[1]), int.Parse(chara[2]), chara[0], data));
                   m = 0;
               }
               else {
                   data[m] = (string)all[i];
                   m++;
               } 
               i++;
            }
            

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
					
                    int imgX= 1,imgY = 1;
				    if (drawTip[y, x] == 0)
				    {
						imgX = 0;
                        imgY = 0;
				    }else if (drawTip[y, x] == 1)
				    {
                        imgX = 1;
                        imgY = 1;
				    }else if (drawTip[y, x] == 2)
                    {
                        imgX = 0;
                        imgY = 1;
                    }
                    DrawDivGraphOnDisplay(x * TIP_SIZE, y * TIP_SIZE, TIP_SIZE, TIP_SIZE, imgX, imgY);
					
				}
			}
			myChar.Draw();
		}
        public void DrawDivGraphOnDisplay(int x, int y, int width, int height, int imgX,int imgY)
        {
            int dx = x - scX;
            int dy = y - scY;
            if (dx < -width || dx > SCREEN_XSIZE * TIP_SIZE || dy < -height || dy > SCREEN_YSIZE * TIP_SIZE) return;

            Drawer.DrawDivGraph(image, imgX, imgY, dx, dy, true);
        }
		public void DrawGraphOnDisplay(int x, int y, int width, int height, string graphName, int alpha = 255){
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

