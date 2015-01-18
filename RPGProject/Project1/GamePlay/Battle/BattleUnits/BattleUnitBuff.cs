using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPGProject.GamePlay.Battle.BattleUnits {
	class BattleUnitBuff {
		public class UnitEffect{
			public enum Type{
				物理攻撃,
				物理防御,
				術式攻撃,
				術式防御,
				全攻撃,
				全防御,
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

		/// <summary>
		/// ゲーム中に表示されるカテゴリ（補助効果の性質を表すものとは限らない）
		/// </summary>
		public enum Category{
			攻撃力,
			防御力,
			命中力,
			回避力
		}

		public readonly string Name;
		public readonly Category ctg;
		public readonly UnitEffect[] effects;
		public readonly int maxTurn;
		//DEBUG
		public int nowTurn;
		//

		public BattleUnitBuff(UnitEffect[] effects, Category ctg, int maxTurn, string buffName){
			this.ctg = ctg;
			this.Name = buffName;
			this.effects = effects;
			this.maxTurn = maxTurn;
			nowTurn = maxTurn+1;
		}

		public void NextTurn(){
			nowTurn--;
		}

		public void AddTurn(int t){
			nowTurn += t;
			if(nowTurn > 9){
				nowTurn = 9;
			}
		}

		public bool isEnd(){
			return nowTurn <= 0;
		}
	}
}
