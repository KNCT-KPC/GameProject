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

        public void SetHP(int arg_hp) {
            HP = arg_hp;
            if (HP < 0) {
                HP = 0;
            }

            if (HP > status.MaxHP) {
                HP = status.MaxHP;
            }
        }

        public void SetTP(int arg_tp) {
            TP = arg_tp;
            if (TP < 0) {
                TP = 0;
            }

            if (TP > status.MaxTP){
                TP = status.MaxTP;
            }
        }

        public PlayerStatus GetStatus() {
            return status;
        }

        public int GetLv() {
            return Lv;
        }

        public string GetName() {
            return name;
        }
    }
}
