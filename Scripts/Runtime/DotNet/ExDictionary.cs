using System.Collections.Generic;

namespace UnityExtensions
{
	public static class ExDictionary
	{
		/// <summary>
		/// Safely clear the dictionary.
		/// </summary>
		/// <param name="list"></param>
		/// <typeparam name="T"></typeparam>
		/// <typeparam name="K"></typeparam>
		public static void SafeClear<T, K>(this IDictionary<T, K> dictionary) {
			if (dictionary.IsNullOrEmpty()) {
				return;
			}

			dictionary.Clear();
		}

		/// <summary>
		/// Search securely whether key is included.
		/// </summary>
		/// <returns><c>true</c>, if contains key was safed, <c>false</c> otherwise.</returns>
		/// <param name="data">Data.</param>
		/// <param name="key">Key.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		/// <typeparam name="K">The 2nd type parameter.</typeparam>
		public static bool SafeContainsKey<T, K>(this IDictionary<T, K> dictionary, T key) {
			if (dictionary.IsNullOrEmpty()) {
				return false;
			}

			return dictionary.ContainsKey(key);
		}
	}
}