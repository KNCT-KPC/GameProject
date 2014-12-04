using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPGProject.GamePlay.Party
{
    class Player
    {
        string name;
        PlayerStatus status;
        int Lv;
        int HP;
        int TP;

        public void NameStatus(string Name, PlayerStatus Status, int hp, int tp) {
            this.name = Name;
            this.status = Status;
            this.HP = hp;
            this.TP = tp;
        }

        public int GetHP() {
            return HP;
        }

        public int GetTP() {
            return TP;
        }

        public void SetHP(int MAXHP) {
            if (HP < 0) {
                HP = 0;
            }

            if (HP >= MAXHP) {
                HP = MAXHP;
            }
        }

        public void SetTP(int MAXTP) {
            if (TP < 0) {
                TP = 0;
            }

            if (TP >= MAXTP){
                TP = MAXTP;
            }
        }

        public PlayerStatus GetStatus() {
            return status;
        }

        public int GetLv() {
            return Lv;
        }
    }
}
