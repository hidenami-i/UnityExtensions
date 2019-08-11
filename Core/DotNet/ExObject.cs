using System.Collections.Generic;

namespace UnityExtensions
{
	public static class ExObject
	{
		/// <summary>
		/// objectをint型へ変換します。
		/// </summary>
		/// <param name="obj"></param>
		/// <param name="defaultValue"></param>
		/// <returns></returns>
		public static int ToInt(object obj, int defaultValue = 0) {
			return obj.ToString().ToInt(defaultValue);
		}

		/// <summary>
		/// objectをfloat型へ変換します。
		/// </summary>
		/// <param name="obj"></param>
		/// <param name="defaultValue"></param>
		/// <returns></returns>
		public static float ToFloat(object obj, float defaultValue = 0.0f) {
			return obj.ToString().ToFloat(defaultValue);
		}

		/// <summary>
		/// objectをdouble型へ変換します。
		/// </summary>
		/// <param name="obj"></param>
		/// <param name="defaultValue"></param>
		/// <returns></returns>
		public static double ToDouble(object obj, double defaultValue = 0.0d) {
			return obj.ToString().ToDouble(defaultValue);
		}

		/// <summary>
		/// objectをlong型へ変換します。
		/// </summary>
		/// <param name="obj"></param>
		/// <param name="defaultValue"></param>
		/// <returns></returns>
		public static long ToLong(object obj, long defaultValue = 0L) {
			return obj.ToString().ToLong(defaultValue);
		}

		/// <summary>
		/// objectをlist型へ変換します。
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public static List<object> ToList(object obj) {
			return obj as List<object>;
		}

		/// <summary>
		/// objectをdictionary型へ変換します。
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public static Dictionary<string, object> ToDictionary(object obj) {
			return obj as Dictionary<string, object>;
		}

		/// <summary>
		/// objectをdictionary型へ変換します。
		/// </summary>
		/// <param name="obj"></param>
		/// <param name="key"></param>
		/// <returns></returns>
		public static Dictionary<string, object> ToDictionaryByKey(object obj, string key) {
			Dictionary<string, object> data = ToDictionary(obj);
			object o;
			if (!data.TryGetValue(key, out o)) {
				ExDebug.LogWarning($"key not found : {key}");
				return new Dictionary<string, object>();
			}

			var value = data[key] as Dictionary<string, object>;
			return value;
		}

		/// <summary>
		/// objectからdictionaryのvalue取得しlist<object>へ変換します。
		/// </summary>
		/// <param name="obj"></param>
		/// <param name="key"></param>
		/// <returns></returns>
		public static List<object> ToListFromDictinaryByKey(object obj, string key) {
			Dictionary<string, object> data = ToDictionary(obj);
			object o;
			if (!data.TryGetValue(key, out o)) {
				ExDebug.LogWarning($"key not found : {key}");
				return new List<object>();
			}

			var list = data[key] as List<object>;
			return list;
		}

		/// <summary>
		/// objectからdictionaryのvalue取得しlist<object>へ変換します。
		/// </summary>
		/// <param name="obj"></param>
		/// <param name="key"></param>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static List<T> ToListFromDictinaryByKey<T>(object obj, string key) {
			Dictionary<string, object> data = ToDictionary(obj);
			object o;
			if (!data.TryGetValue(key, out o)) {
				ExDebug.LogWarning($"key not found : {key}");
				return new List<T>();
			}

			var list = data[key] as List<T>;
			return list;
		}
	}
}