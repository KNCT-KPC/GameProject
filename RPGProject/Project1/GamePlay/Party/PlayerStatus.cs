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

        public PlayerStatus(int hp, int tp, int 筋力, int 耐久, int 精神, int 技能, int 俊敏, int 幸運)
        {
            this.HP = hp;
            this.TP = tp;
            this.筋力 = 筋力;
            this.耐久 = 耐久;
            this.精神 = 精神;
            this.技能 = 技能;
            this.俊敏 = 俊敏;
            this.幸運 = 幸運;
        }
    }
}
