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
            moveDir = MoveDirect.NEUTRAL;
            
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
            if (map.JuggeEnter(mx, my) == 1)
            {
                moveDir = MoveDirect.NEUTRAL;
                return (false);
            }

            px = mx;
            py = my;
            
            return (true);


        }
    }
}
