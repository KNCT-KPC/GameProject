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
        
        int my, mx;
        override bool Move()
        {
             
            if (GameInput.GetCount(GameInput.Code.INPUT_UP) > 0)
            {
                my = py--;
                moveDir = MoveDirect.UP;
            }
            else if (GameInput.GetCount(GameInput.Code.INPUT_DOWN) > 0)
            {
                my = py++;
                moveDir = MoveDirect.DOWN;
            }
            else if (GameInput.GetCount(GameInput.Code.INPUT_LEFT) > 0)
            {
                mx = px--;
                moveDir = MoveDirect.LEFT;
            }
            else if (GameInput.GetCount(GameInput.Code.INPUT_RIGHT) > 0)
            {
                mx = px++;
                moveDir = MoveDirect.RIGHT;
            }
            if (Map.JuggeEnter(mx, my) == 1) return (false);

            px = mx;
            py = my;
            return (true);


        }
    }
}
