using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPGProject.GamePlay.Party
{
    class PlayerStatus
    {
        public readonly int HP;
        public readonly int TP;
        public readonly int 筋力;
        public readonly int 耐久;
        public readonly int 精神;
        public readonly int 技能;
        public readonly int 俊敏;
        public readonly int 幸運;

        public PlayerStatus(int hp, int tp, int str, int end, int spi, int tec, int smq, int luc)
        {
            HP = hp;
            TP = tp;
            耐久 = str;
            精神 = end;
            精神 = spi;
            技能 = tec;
            俊敏 = smq;
            幸運 = luc;
        }
    }
}
