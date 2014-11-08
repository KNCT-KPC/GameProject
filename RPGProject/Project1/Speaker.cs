using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DxLibDLL;

namespace Project1 {
	static class Speaker {
		/// <summary>
		/// 音声を通常再生するメソッド
		/// </summary>
		/// <param name="soundName">登録音声名</param>
		public static void PlaySound(string soundName){
			DX.PlaySoundMem(GameSound.GetSound(soundName), DX.DX_PLAYTYPE_BACK);
		}


		/// <summary>
		/// 音声をループ再生するメソッド
		/// </summary>
		/// <param name="soundName">登録音声名</param>
		public static void PlaySoundLoop(string soundName){
			DX.PlaySoundMem(GameSound.GetSound(soundName), DX.DX_PLAYTYPE_LOOP);
		}

		/// <summary>
		/// 指定した音声が再生中かどうかを調べるメソッド
		/// </summary>
		/// <param name="soundName">登録音声名</param>
		/// <returns>再生中かどうか</returns>
		public static bool CheckPlaySound(string soundName){
			if(DX.CheckSoundMem(GameSound.GetSound(soundName)) == 1) return true;
			return false;
		}

		/// <summary>
		/// 指定した音声が再生中なら、停止するメソッド
		/// </summary>
		/// <param name="soundName">登録音声名</param>
		public static void StopSound(string soundName){
			DX.StopSoundMem(GameSound.GetSound(soundName));
		}

		/// <summary>
		/// 指定した音声のボリュームを変更する
		/// </summary>
		/// <param name="volume">ボリューム（最大255)</param>
		/// <param name="soundName">登録音声名</param>
		public static void ChangeVolumeSound(int volume, string soundName){
			DX.ChangeVolumeSoundMem(volume, GameSound.GetSound(soundName));
		}
	}
}
