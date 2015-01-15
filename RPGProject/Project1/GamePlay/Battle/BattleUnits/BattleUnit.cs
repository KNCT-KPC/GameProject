using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPGProject.GamePlay.Battle.BattleUnits {
	class BattleUnit {
		public struct Status{
			public readonly int 物理攻撃;
			public readonly int 物理防御;
			public readonly int 術式攻撃;
			public readonly int 術式防御;
			public readonly int 行動速度;
			public readonly int 会心補正;
			public readonly int 物理命中;
			public readonly int 物理回避;
			public readonly int 変化命中;
			public readonly int 変化回避;

			public Status(int 物理攻撃, int 物理防御, int 術式攻撃, int 術式防御, int 行動速度, 
				int 会心補正, int 物理命中, int 物理回避, int 変化命中, int 変化回避){
			  this.物理攻撃 = 物理攻撃;
			  this.物理防御 = 物理防御;
			  this.術式攻撃 = 術式攻撃;
			  this.術式防御 = 術式防御;
			  this.行動速度 = 行動速度;
			  this.会心補正 = 会心補正;
			  this.物理命中 = 物理命中;
			  this.物理回避 = 物理回避;
			  this.変化命中 = 変化命中;
			  this.変化回避 = 変化回避;				
			}
		}

		public string Name{get; set;}
		public int HP{get; set;}
		public int TP{get; set;}
		public Status status{get; private set;}

		public BattleUnit(string name){
			Name = name;
			HP = 200;
			TP = 100;

			status = new Status(200, 200, 200, 200, 200, 200, 200, 200, 200, 200);
		}

		public bool isAbleToAction(){
			return true;
		}
	}
}
