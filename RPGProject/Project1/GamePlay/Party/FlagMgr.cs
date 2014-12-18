using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPGProject.GamePlay.Control
{
    class FlagMgr
    {
        Dictionary<string, bool> flags;

        /// <summary>
        /// flagsの初期化
        /// </summary>
        /// <param name="names">文字列配列</param>
        public FlagMgr(string[] names) {
            for (int i = 0; i < names.Length; i++) {
                flags[names[i]] = false;
            }
        }

        /// <summary>
        /// フラグをセットする
        /// </summary>
        /// <param name="names">フラグ名</param>
        /// <param name="flag">bool値</param>
        public void SetFlag(string names, bool flag) {
            flags[names] = flag;
        }

        /// <summary>
        /// フラグを反転する
        /// </summary>
        /// <param name="names">フラグ名</param>
        public void ReverseFlag(string names) {
            flags[names] = true;

            if (flags[names]) {
                flags[names] = false;
            } else {
                flags[names] = true;
            }
        }

        /// <summary>
        /// フラグを返す
        /// </summary>
        /// <param name="names">フラグ名</param>
        /// <returns></returns>
        public bool GetFlag(string names) {
            return (flags[names]);
        }
    }
}
