using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace RPGProject.GamePlay.Battle {
	/// <summary>
	/// 戦闘中に発生する行動を表すクラス
	/// </summary>
	class BattleAction {
		/// <summary>
		/// 「このアクション」が対象とする範囲（発生する効果の対象とは限らない）
		/// </summary>
		// 例えば、以下の様なことが考えられる
		// エナジースマイト : この攻撃で敵を倒すと自身のTPが回復
		// アクションの対象は敵だが、発生する追加効果の対象は使用者自身
		public enum TargetSide{
			Friend,
			Rival,
			All,
			Ones
		}

		/// <summary>
		/// 対象とするターゲットから選ぶ範囲
		/// </summary>
		public enum TargetRange{
			Single,
			All,
		}

		/// <summary>
		/// 行動のカテゴリ（発生する効果を表すものではない）
		/// </summary>
		public enum Type{
			攻撃,
			補助,
		}

		//フィールド
		readonly public string name;	//アクション名
		readonly public Type type;		//アクションのカテゴリ
		readonly public int TP;			//消費するTP
		readonly public int priority;	//優先度
		readonly public bool veer;		//対象が戦闘不能の場合、残りからランダムに選ぶかどうか
		readonly public TargetSide trgSide;		//対象とする範囲
		readonly public TargetRange trgRange;	//全体か単体か
		readonly public ReadOnlyCollection<string[]> script;	//アクションスクリプト

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public BattleAction(string name, Type type, int TP, TargetSide trgSide, 
							TargetRange trgRange, bool veer, int priority, string[][] script){
			this.name = name;
			this.type = type;
			this.TP = TP;
			this.trgSide = trgSide;
			this.trgRange = trgRange;
			this.priority = priority;
			this.veer = veer;
			this.script = new ReadOnlyCollection<string[]>(script);
		}
	}
}
