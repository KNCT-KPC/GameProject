using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPGProject.GamePlay.Party
{
    class Player
    {
        private string name;
        private PlayerStatus status;
        private int Lv;
        private int HP;
        private int TP;


        /// <summary>
        /// nameとPlayerStatus、HP/TPを引数で受け取ってフィールドを初期化。
        /// </summary>
        /// <param name="Name">名前</param>
        /// <param name="Status">ステータス</param>
        /// <param name="hp">HP</param>
        /// <param name="tp">TP</param>
        public Player(string Name, PlayerStatus Status, int hp, int tp) {
            this.name = Name;
            this.status = Status;
            this.HP = hp;
            this.TP = tp;
        }

        /// <summary>
        /// HPのゲッタ
        /// </summary>
        /// <returns>HP</returns>
        public int GetHP() {
            return HP;
        }

        /// <summary>
        /// TPのゲッタ
        /// </summary>
        /// <returns>TP</returns>
        public int GetTP() {
            return TP;
        }

        /// <summary>
        /// HPのセッタ
        /// </summary>
        /// <param name="arg_hp">与えられるHP</param>
        public void SetHP(int arg_hp) {
            HP = arg_hp;
            if (HP < 0) {
                HP = 0;
            }

            if (HP > status.MaxHP) {
                HP = status.MaxHP;
            }
        }

        /// <summary>
        /// TPのセッタ
        /// </summary>
        /// <param name="arg_tp">与えられるTP</param>
        public void SetTP(int arg_tp) {
            TP = arg_tp;
            if (TP < 0) {
                TP = 0;
            }

            if (TP > status.MaxTP){
                TP = status.MaxTP;
            }
        }

        /// <summary>
        /// statusのゲッタ
        /// </summary>
        /// <returns>ステータス</returns>
        public PlayerStatus GetStatus() {
            return status;
        }

        /// <summary>
        /// Lvのゲッタ
        /// </summary>
        /// <returns>Lv</returns>
        public int GetLv() {
            return Lv;
        }

        /// <summary>
        /// 名前のゲッタ
        /// </summary>
        /// <returns>名前</returns>
        public string GetName() {
            return name;
        }
    }
}
