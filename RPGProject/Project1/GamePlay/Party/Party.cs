using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPGProject.GamePlay.Party
{
    class Party
    {
        public const int ITEM_MAXNUM = 50;
        public const int MEMBER_MAXNUM = 4;
        private string[] item;
        private int memNum;
        private PlayerUnit[] member;
        

        /// <summary>
        /// Player配列の生成
        /// </summary>
        public Party() {
            member = new PlayerUnit[MEMBER_MAXNUM];
            item = new string[MEMBER_MAXNUM];
        }

        bool AddMember(PlayerUnit p) {
            if (memNum == MEMBER_MAXNUM) {
                return false;
            }

            member[memNum] = p;
            memNum++;
            return true;
        }

        /// <summary>
        /// 番号を入力し、配列から指定された番号のキャラクターを返す。
        /// </summary>
        /// <param name="n">配列番号</param>
        /// <returns>配列番号の要素</returns>
        public PlayerUnit GetMember(int n) {
            return member[n];
        }

        void RemoveMember(string name) {
            for (int i = 0; i < member.Length; i++) {
                if (member[i] != null) {
                    if (name == member[i].GetName()) {
                        member[i] = null;
                        memNum--;
                        member[MEMBER_MAXNUM] = null;
                    }
                }
            }
        }

        /// <summary>
        /// 名前を入力し、同じ名前のキャラクターを返す。
        /// </summary>
        /// <param name="name">プレイヤーの名前</param>
        /// <returns>一致するプレイヤー</returns>
        public PlayerUnit SearchPlayerUnit(string name) {
            for (int i = 0; i < member.Length; i++) {
                if (member[i] != null) {
                    if (name == member[i].GetName()) {
                        return member[i];
                    }
                }
            }
            return null;
        }
    }
}
