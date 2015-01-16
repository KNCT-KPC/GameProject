using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPGProject.GamePlay.Battle.BattleUnits;

namespace RPGProject.GamePlay.Battle.OrderExecute {
	class ActionCalculator {
		public class AttackData{
			public readonly BattleUnit.Category ctg;
			public readonly BattleUnit.Element elm;
			public readonly int power;
			public readonly int hit;

			public AttackData(string[] line){
				string ctgStr = line[1];
				string elmStr = line[2];
				power = int.Parse(line[3]);
				hit = int.Parse(line[4]);

				if(ctgStr == "物理"){
					ctg = BattleUnit.Category.物理;
				} else {
					ctg = BattleUnit.Category.術式;
				}

				elm = BattleUnit.Element.無;
				switch(elmStr){
				case "炎" : elm = BattleUnit.Element.炎; break;
				case "氷" : elm = BattleUnit.Element.氷; break;
				case "雷" : elm = BattleUnit.Element.雷; break;
				}
			}
		};

		public static bool Attack(BattleUnit actor, BattleUnit target, AttackData atkData){
			int baseValue = 0;

			if(atkData.ctg == BattleUnit.Category.物理){
				baseValue = actor.status.物理攻撃 - target.status.物理防御/2;
			} else {
				baseValue = actor.status.術式攻撃 - target.status.術式防御/4;
			}

			int normalDamage = (int)(baseValue * (atkData.power/100.0));
			target.Damage(normalDamage);

			//DEBUG
			Battle.DEBUG_MESSAGE = actor.Name + " >> " + target.Name + " | " + normalDamage + "ダメージ";
			//DEBUG

			return true;
		}
	}
}
