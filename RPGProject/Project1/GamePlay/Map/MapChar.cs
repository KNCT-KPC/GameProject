using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPGProject.GameSystem;
using DxLibDLL;
using RPGProject.GamePlay.Map;

namespace RPGProject.GamePlay.Map
{
        abstract class MapChar
        {
            public enum MoveDirect { NEUTRAL, UP, DOWN, LEFT, RIGHT };
            protected const int MOVE_TIME = 16; //移動にかかるカウント
            protected Map map; //マップ
            protected int px;  //キャラクタのx位置
            protected int py;  //キャラクタのy位置
            protected MoveDirect moveDir; //移動方向
            protected int moveCount; //移動カウント
            protected MapChar(Map maps, int x, int y)
            {
                map = maps;
                px = x;
                py = y;
                moveDir = MoveDirect.NEUTRAL;
                moveCount = 0;
            }
            public void Update()
            {
                if (moveCount == 0)
                {
                    if (Move() == true) moveCount = MOVE_TIME;
                }
                if (moveCount > 0)
                {
                    moveCount--;
                }
              
            }

            protected abstract bool Move();
            
            public void Draw()
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
				map.DrawGraphOnDisplay(nx, ny, Map.TIP_SIZE, Map.TIP_SIZE, "TEST_CHAR_IMG");
            }
           public int GetX(){
               return(px);
               }
           public int GetY(){
                return(py);
                }
           }
}
        
    

