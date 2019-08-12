using System.Linq;

namespace UnityExtensions
{
	public static class ExArray
	{
		/// <summary>
		/// Returns the array of the length.
		/// If the array is null it rturns 0;
		/// </summary>
		/// <param name="self"></param>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static int SafeLength<T>(this T[] self) {
			if (self.IsNullOrEmpty()) {
				return 0;
			}

			return self.Length;
		}

		/// <summary>
		/// 配列同士を比較し、同じであればtrueを返します。
		/// </summary>
		/// <param name="a1"></param>
		/// <param name="a2"></param>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static bool SequenceMatch<T>(this T[] a1, T[] a2) {
			if (a1.SafeLength() != a2.SafeLength()) {
				return false;
			}

			return a1.All(a2.Contains);
		}
	}
}