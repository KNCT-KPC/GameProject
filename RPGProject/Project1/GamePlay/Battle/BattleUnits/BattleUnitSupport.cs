using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPGProject.GamePlay.Battle.BattleUnits {
	/// <summary>
	/// 戦闘のサポート効果を発生させる
	/// </summary>
	class BattleUnitSupport {
		/// <summary>
		/// サポート効果が発生するタイミング
		/// </summary>
		public enum Timing{
			攻撃を受けた,
			攻撃を行った,
		}

		/// <summary>
		/// サポート効果が反応する範囲
		/// </summary>
		public enum Range{
			自分自身,
			味方全員,
			敵全員,
			全体,
		}

		//フィールド
		public readonly string Name;	//このサポート効果を発生させたアクション名
		private readonly Timing timing;	//発生タイミング
		private readonly Range range;	//反応範囲
		private readonly string[][] script;	//スクリプト
		private int nowTurn;	//残りターン

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public BattleUnitSupport(Timing timing, Range range, string[][] script, int maxTurn, string supportName){
			this.range = range;
			this.script = script;
			this.timing = timing;
			this.Name = supportName;
			nowTurn = maxTurn+1;
		}

		/// <summary>
		/// サポートのトリガーが発生したことを知らされるメソッド
		/// </summary>
		/// <param name="tim">発生タイミング</param>
		/// <param name="parent">この効果を所持しているユニット</param>
		/// <param name="actor">トリガーを発生させたユニット</param>
		/// <param name="status">ステータス</param>
		public string[][] Notice(Timing tim, BattleUnit parent, BattleUnit actor, Dictionary<string,string> status){
			//発生タイミングが一致していないなら無視
			if(timing != tim) return null;

			//範囲内で発生したものでないなら無視
			bool enable = true;
			switch(range){
			case Range.自分自身:
				enable = (actor == parent);
				break;
			case Range.味方全員:
				enable = (actor.GetType() == parent.GetType());
				break;
			case Range.敵全員:
				enable = (actor.GetType() != parent.GetType());
				break;
			}
			if(!enable) return null;

			//有効なら、スクリプト解釈	
			List<string[]> effects = new List<string[]>(0);
			foreach(var m in script){
				List<string> e = new List<string>(0);
				e.AddRange(m);

				bool cond = false;
				string val;

				switch(e[0]){
				case "If":
				case "Unless":
					if(status.TryGetValue(e[1], out val)){
						switch(e[1]){
						case "攻撃カテゴリ":
							cond = e[2] == val;
							break;
						}
					}

					if((e[0] == "If" && !cond) || (e[0] == "Unless" && cond)){
						return null;
					}
					break;
				case "Effect":
					e.RemoveAt(0);
					e.Add(actor.Name);
					effects.Add(e.ToArray());
					break;
				}
			}
			return effects.ToArray();
		}


		public void NextTurn(){
			nowTurn--;
		}
		public bool isEnd(){
			return nowTurn <= 0;
		}
	}
}
