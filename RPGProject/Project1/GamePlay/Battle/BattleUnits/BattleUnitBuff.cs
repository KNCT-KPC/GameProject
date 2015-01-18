using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPGProject.GamePlay.Battle.BattleUnits {
	class BattleUnitBuff {
		public class UnitEffect{
			public enum Type{
				威力,
				ダメージ,
				物理命中,
				物理回避,
				変化命中,
				変化回避,
			}

			public readonly Type type;
			public readonly int value;

			public UnitEffect(Type type, int value){
				this.type = type;
				this.value = value;
			}
		}

		public readonly string buffName;
		public readonly UnitEffect[] effects;
		private int nowTurn;

		public BattleUnitBuff(UnitEffect[] effects, int maxTurn, string buffName){
			this.buffName = buffName;
			this.effects = effects;
			nowTurn = maxTurn+1;
		}

		public void NextTurn(){
			nowTurn--;
		}

		public bool isEnd(){
			return nowTurn <= 0;
		}
	}
}
