using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPGProject.GameSystem;
using DxLibDLL;

namespace RPGProject.GamePlay.Map
{
    class MapMychar :  MapChar
    {
       
        public MapMychar(Map maps, int x, int y)
            : base (maps,x,y)
        {
        }

        override protected bool Move()
        {
            int my = py;
            int mx = px;
            bool succes;
            if (GameInput.GetCount(GameInput.Code.INPUT_UP) > 0)
            {
                my--;
                moveDir = MoveDirect.UP;
            }
            else if (GameInput.GetCount(GameInput.Code.INPUT_DOWN) > 0)
            {
                my++;
                moveDir = MoveDirect.DOWN;
            }
            else if (GameInput.GetCount(GameInput.Code.INPUT_LEFT) > 0)
            {
                mx--;
                moveDir = MoveDirect.LEFT;
            }
            else if (GameInput.GetCount(GameInput.Code.INPUT_RIGHT) > 0)
            {
                mx++;
                moveDir = MoveDirect.RIGHT;
            }
            else return(false);

            if (map.JudgeEnter(mx, my)) succes = true;
            else succes = false; 
            if (succes)
            {
                px = mx;
                py = my;
                return (true);
            }
            else
            {
                return (false);
            }


        }

		public void GetScreenCenterPosition(out int x, out int y)
		{
			int nx = px * Map.TIP_SIZE;
			int ny = py * Map.TIP_SIZE;
			if (moveCount != 0)
			{
				switch (moveDir)
				{
					case MoveDirect.UP   :  ny = ny + moveCount * (Map.TIP_SIZE / MOVE_TIME); //y に 足す
						break;
					case MoveDirect.DOWN :  ny = ny - moveCount * (Map.TIP_SIZE / MOVE_TIME);//y から 引く
						break;
					case MoveDirect.LEFT :  nx = nx + moveCount * (Map.TIP_SIZE / MOVE_TIME); //x に 足す
						break;
					case MoveDirect.RIGHT:  nx = nx - moveCount * (Map.TIP_SIZE / MOVE_TIME);//x から 引く
						break;
					default: break;
				}
			}

			x = nx + Map.TIP_SIZE/2;
			y = ny + Map.TIP_SIZE/2;
		}
    }
}
