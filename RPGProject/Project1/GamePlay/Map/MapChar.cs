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
            const int TIP_SIZE = 32;
            public enum MoveDirect { NEUTRAL, UP, DOWN, LEFT, RIGHT };
            private const int MOVE_TIME = 32; //移動にかかるカウント
            protected Map map; //マップ
            protected int px;  //キャラクタのx位置
            protected int py;  //キャラクタのy位置
            protected MoveDirect moveDir; //移動方向
            private int moveCount; //移動カウント
            protected MapChar( Map maps, int x, int y)
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
                else if (moveCount > 0)
                {
                    moveCount--;
                }
              
            }

            protected abstract bool Move();
            
            public void Draw()
            {
                int nx, ny;
                nx = px *TIP_SIZE;
                ny = py *TIP_SIZE;
                if (moveCount != 0)
                {
                    switch (moveDir)
                    {
                        case MoveDirect.UP: ny = ny +  moveCount; //y に 足す
                            break;
                        case MoveDirect.DOWN: ny = ny - moveCount;//y から 引く
                            break;
                        case MoveDirect.LEFT: nx = nx + moveCount; //x に 足す
                            break;
                        case MoveDirect.RIGHT: nx =  nx  - moveCount;//x から 引く
                            break;
                        default: break;
                    }
                }
                DxLibDLL.DX.DrawBox(nx,ny, nx + TIP_SIZE, ny + TIP_SIZE, DX.GetColor(0, 0, 255), 1);
            }
        }
    
}
