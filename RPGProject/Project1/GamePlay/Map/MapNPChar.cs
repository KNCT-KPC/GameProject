using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPGProject.GamePlay.Map {
	class MapNPChar : MapObject{
		private string name; 
		private string[] eventScript;

		public MapNPChar(Map maps, int x, int y, string name, string[] eventScript) : base(maps, x, y){
			this.name = name;
			this.eventScript = eventScript;
		}

		public string[] Checked(){
			//将来は「チェックでイベントを返すなら」的な判定をする
			return eventScript;
		}

		override protected bool Move(){
			//テキトーに動く
			return false;
		}
	}
}
