using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPGProject.GamePlay.Map
{
    class MapData
    {
        /// <summary>
        /// マップデータのメンバ
        /// </summary>
        readonly public string mapName;
        readonly public int xSize;
        readonly public int ySize;
        readonly public int[,] stateTip;
        readonly public int[,] drawTip;
        
        /// <summary>
        /// マップデータの初期化
        /// </summary>
        /// <param name="mapname"></param>
        /// <param name="xsize"></param>
        /// <param name="ysize"></param>
        /// <param name="statetip"></param>
        /// <param name="drawtip"></param>
        public MapData(string mapname, int xsize, int ysize, int[,] statetip, int[,] drawtip) {
            this.mapName = mapname;
            this.xSize = xsize;
            this.ySize = ysize;
            this.stateTip = statetip;
            this.drawTip = drawtip;
        }
    }
}
