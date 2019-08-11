using System;
using UnityEngine;

namespace UnityExtensions
{
	public static class ExApplication
	{
		/// <summary>
		/// Open blowser.
		/// url is urlencoded.
		/// </summary>
		public static void OpenURLWithEscapeUri(string url) {
			if (string.IsNullOrEmpty(url)) {
				ExDebug.LogError("url is null or empty");
				return;
			}

			Application.OpenURL(Uri.EscapeUriString(url));
		}

		/// <summary>
		/// Set 60 target frame rate.
		/// </summary>
		public static void SetTargetFrameRate60() {
			SetTargetFrameRate(60);
		}

		/// <summary>
		/// Set 30 target frame rate.
		/// </summary>
		public static void SetTargetFrameRate30() {
			SetTargetFrameRate(30);
		}

		/// <summary>
		/// Set target frame rate.
		/// </summary>
		/// <param name="frameRate"></param>
		public static void SetTargetFrameRate(int frameRate) {
			QualitySettings.vSyncCount = 0;
			Application.targetFrameRate = frameRate;
		}

		/// <summary>
		/// データを非同期で読み込む時に1フレーム内で使える最大時間を変更します。
		/// ローディングや読み込み画面を出すときはHighに設定します。
		///
		/// Low 2ms
		/// BelowNormal 4ms
		/// Normal 10ms
		/// High 50ms
		///
		/// </summary>
		/// <param name="threadPriority"></param>
		public static void SetBackgroundLoadingPriority(ThreadPriority threadPriority) {
			Application.backgroundLoadingPriority = threadPriority;
		}
	}
}