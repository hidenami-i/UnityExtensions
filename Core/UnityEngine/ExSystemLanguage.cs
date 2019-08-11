using UnityEngine;

namespace Kundalini.Core
{
	public static class ExSystemLaunguage
	{
		// public static bool IsAfrikaans(this SystemLanguage systemLanguage) {
		// 	return systemLanguage == SystemLanguage.Afrikaans;
		// }
		//
		// public static bool IsArabic(this SystemLanguage systemLanguage) {
		// 	return systemLanguage == SystemLanguage.Arabic;
		// }
		//
		// public static bool IsBasque(this SystemLanguage systemLanguage) {
		// 	return systemLanguage == SystemLanguage.Basque;
		// }
		//
		// public static bool IsBelarusian(this SystemLanguage systemLanguage) {
		// 	return systemLanguage == SystemLanguage.Belarusian;
		// }
		//
		// public static bool IsBulgarian(this SystemLanguage systemLanguage) {
		// 	return systemLanguage == SystemLanguage.Bulgarian;
		// }
		//
		// public static bool IsCatalan(this SystemLanguage systemLanguage) {
		// 	return systemLanguage == SystemLanguage.Catalan;
		// }

		/// <summary>
		/// Chinese (中国語)
		/// </summary>
		/// <param name="systemLanguage"></param>
		/// <returns></returns>
		public static bool IsChinese(this SystemLanguage systemLanguage) {
			return systemLanguage == SystemLanguage.Chinese;
		}

		/// <summary>
		/// ChineseSimplified (簡体字)
		/// </summary>
		/// <param name="systemLanguage"></param>
		/// <returns></returns>
		public static bool IsChineseSimplified(this SystemLanguage systemLanguage) {
			return systemLanguage == SystemLanguage.ChineseSimplified;
		}

		/// <summary>
		/// ChineseTraditional (繁体字)
		/// </summary>
		/// <param name="systemLanguage"></param>
		/// <returns></returns>
		public static bool IsChineseTraditional(this SystemLanguage systemLanguage) {
			return systemLanguage == SystemLanguage.ChineseTraditional;
		}

		// public static bool IsCzech(this SystemLanguage systemLanguage) {
		// 	return systemLanguage == SystemLanguage.Czech;
		// }
		//
		// public static bool IsDanish(this SystemLanguage systemLanguage) {
		// 	return systemLanguage == SystemLanguage.Danish;
		// }

		// public static bool IsDutch(this SystemLanguage systemLanguage) {
		// 	return systemLanguage == SystemLanguage.Dutch;
		// }

		public static bool IsEnglish(this SystemLanguage systemLanguage) {
			return systemLanguage == SystemLanguage.English;
		}

		// public static bool IsEstonian(this SystemLanguage systemLanguage) {
		// 	return systemLanguage == SystemLanguage.Estonian;
		// }
		//
		// public static bool IsFaroese(this SystemLanguage systemLanguage) {
		// 	return systemLanguage == SystemLanguage.Faroese;
		// }
		//
		// public static bool IsFinnish(this SystemLanguage systemLanguage) {
		// 	return systemLanguage == SystemLanguage.Finnish;
		// }

		public static bool IsFrench(this SystemLanguage systemLanguage) {
			return systemLanguage == SystemLanguage.French;
		}

		public static bool IsGerman(this SystemLanguage systemLanguage) {
			return systemLanguage == SystemLanguage.German;
		}

		// public static bool IsHebrew(this SystemLanguage systemLanguage) {
		// 	return systemLanguage == SystemLanguage.Hebrew;
		// }
		//
		// public static bool IsHungarian(this SystemLanguage systemLanguage) {
		// 	return systemLanguage == SystemLanguage.Hungarian;
		// }

		public static bool IsJapanese(this SystemLanguage systemLanguage) {
			return systemLanguage == SystemLanguage.Japanese;
		}

		public static bool IsKorean(this SystemLanguage systemLanguage) {
			return systemLanguage == SystemLanguage.Korean;
		}

		public static string LanguageCode(this SystemLanguage systemLanguage) {
			switch (systemLanguage) {
				case SystemLanguage.Chinese:
				case SystemLanguage.ChineseSimplified:
				case SystemLanguage.ChineseTraditional:
					return "zh";
				case SystemLanguage.English:
					return "en";
				case SystemLanguage.French:
					return "fr";
				case SystemLanguage.German:
					return "de";
				case SystemLanguage.Japanese:
					return "ja";
				case SystemLanguage.Korean:
					return "ko";
				default:
					Debug.LogWarning($"[{systemLanguage.ToString()}] is incompatible language.");
					return "";
			}
		}
	}
}