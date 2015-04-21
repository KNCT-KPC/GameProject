using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPGProject.GamePlay.Party
{    
    class PlayerUnitStatus
    {
        public readonly int MaxHP;
        public readonly int MaxTP;
        public readonly int 筋力;
        public readonly int 耐久;
        public readonly int 精神;
        public readonly int 技能;
        public readonly int 俊敏;
        public readonly int 幸運;
        
        /// <summary>
        /// 8能力すべてを引数として受け取りフィールドを初期化
        /// </summary>
        /// <param name="maxhp">MaxHP</param>
        /// <param name="maxtp">MaxTP</param>
        /// <param name="筋力">筋力</param>
        /// <param name="耐久">耐久</param>
        /// <param name="精神">精神</param>
        /// <param name="技能">技能</param>
        /// <param name="俊敏">俊敏</param>
        /// <param name="幸運">幸運</param>
        public PlayerUnitStatus(int maxhp, int maxtp, int 筋力, int 耐久, int 精神, int 技能, int 俊敏, int 幸運) {
            this.MaxHP = maxhp;
            this.MaxTP = maxtp;
            this.筋力 = 筋力;
            this.耐久 = 耐久;
            this.精神 = 精神;
            this.技能 = 技能;
            this.俊敏 = 俊敏;
            this.幸運 = 幸運;
        }
    }
}
