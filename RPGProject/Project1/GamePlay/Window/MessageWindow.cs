using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPGProject.GamePlay.FrontEnd;
using RPGProject.GameSystem;


namespace RPGProject.GamePlay.Window
{
    class MessageWindow 
    {
        const int STRING_X = 40;       //1文字目のx座標
        const int STRING_Y = 320;       //1文字目のy座標
        const int LINE_HEIGHT = 30;   //1行あたりのy幅
        String[] message;
        TimeMessage timeMessage;
        Window window = new Window(STRING_X-20, STRING_Y-20, 600, 160);
        int messageCount = 0;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="arg_message">表示させるメッセージ</param>
        public MessageWindow( String[] arg_message )
        {
            message = arg_message;
            timeMessage = new TimeMessage(message[messageCount], "DEBUG_PFONT"); 
        }
        
        public void Update()
        { 
          timeMessage.NextCount();
          if (timeMessage.isFinished())  //Trueだった場合
          {
              if (GameSystem.GameInput.GetCount(GameInput.Code.INPUT_DECIDE) >= 1)
              {
                  timeMessage = new TimeMessage(message[messageCount++], "DEBUG_PFONT");
                  window.Break();
              }

          }
          else
          {
              if (GameSystem.GameInput.GetCount(GameInput.Code.INPUT_DECIDE) >= 1)
              {
                  timeMessage.Finish(); 
              }
          }

        }

        public void Draw()
        {
            window.Draw();
            string[] line = timeMessage.GetMessage();
            for (int i = 0; i < line.Length; i++)
            {
                
               // DxLibDLL.DX.DrawString(STRING_X, STRING_Y + LINE_HEIGHT * i, line[i], DX.GetColor(255, 255, 255));
                Drawer.DrawString(STRING_X, STRING_Y + LINE_HEIGHT*i, line[i], new GameColor(0,0,0), "DEBUG_PFONT");
            }
        }
        
    }
}
