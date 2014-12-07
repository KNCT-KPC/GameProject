using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPGProject.GamePlay.Party
{
    class Party
    {
        Player[] member;

        public void Member() {
            member = new Player[5];
        }

        void AddMember(Player p) {
            for (int i = 0; i < member.Length; i++) {
                if (member[i] == null){
                    member[i] = p;
                }
            }
        }

        public Player GetMember(int n) {
            return member[n];
        }

        public Player SearchPlayer(string name) {
            for (int i = 0; i < member.Length; i++) {
                if (member[i] != null) {
                    if (member[i] == Player.GetName()) {
                        return name;
                    }
                }
            }
            return null;
        }
    }
}
