using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPGProject {
	static class GameMath {
		static public bool JudgeProbab(int probab){
			if(DxLibDLL.DX.GetRand(99) < probab){
				return true;
			}

			return false;
		}

		static public int[] GetRandomArray(int length){
			int[] ary = new int[length];
			int r,a,temp;

			for(int i = 0; i < length; i++){
				ary[i] = i;
			}
			
			for(int i = 0; i < length; i++){
				r = (length-1) - i;
				a = DxLibDLL.DX.GetRand(length-1);

				temp = ary[r];
				ary[r] = ary[a];
				ary[a] = temp;
			}

			return ary;
		}
	}
}
