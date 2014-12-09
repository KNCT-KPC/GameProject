using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPGProject.GamePlay.Party
{
    class Party
    {
        private Player[] member;

        /// <summary>
        /// Player配列の生成
        /// </summary>
        public Party() {
            member = new Player[5];
        }

        void AddMember(Player p) {
            for (int i = 0; i < member.Length; i++) {
                if (member[i] == null){
                    member[i] = p;
                }
            }
        }

        /// <summary>
        /// 番号を入力し、配列から指定された番号のキャラクターを返す。
        /// </summary>
        /// <param name="n">配列番号</param>
        /// <returns>配列番号の要素</returns>
        public Player GetMember(int n) {
            return member[n];
        }

        /// <summary>
        /// 名前を入力し、同じ名前のキャラクターを返す。
        /// </summary>
        /// <param name="name">プレイヤーの名前</param>
        /// <returns>一致するプレイヤー</returns>
        public Player SearchPlayer(string name) {
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
