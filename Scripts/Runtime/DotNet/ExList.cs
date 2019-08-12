using System;
using System.Collections.Generic;
using System.Linq;

namespace UnityExtensions
{
	public static class ExList
	{
		/// <summary>
		/// Safely clear the list.
		/// </summary>
		/// <param name="list"></param>
		/// <typeparam name="T"></typeparam>
		public static void SafeClear<T>(this IList<T> list) {
			if (list.IsNullOrEmpty()) {
				return;
			}

			list.Clear();
		}

		/// <summary>
		/// Safely contains the list.
		/// </summary>
		/// <param name="list"></param>
		/// <param name="value"></param>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static bool SafeContains<T>(this IList<T> list, T value) {
			if (list.IsNullOrEmpty()) {
				return false;
			}

			return list.Contains(value);
		}

		/// <summary>
		/// リスト内に指定された要素があるか調べて
		/// 存在する場合はその要素をリストから削除します。
		/// </summary>
		public static void SafeRemoveBy<T>(this List<T> list, Predicate<T> match) {
			if (list.IsNullOrEmpty()) {
				return;
			}

			var index = list.FindIndex(match);
			if (index == -1) {
				return;
			}

			list.RemoveAt(index);
		}

		/// <summary>
		/// リストから指定された要素を削除します。
		/// 要素がない場合は追加します。
		/// </summary>
		/// <param name="list">List.</param>
		/// <param name="item">Item.</param>
		/// <param name="match">Match.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public static void SafeRemoveOrAdd<T>(this List<T> list, T item) {
			if (list.IsNullOrEmpty()) {
				list.Add(item);
				return;
			}

			if (list.Contains(item)) {
				list.Remove(item);
				return;
			}

			list.Add(item);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="list"></param>
		/// <param name="index"></param>
		/// <param name="defaultValue"></param>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static T SafeFindByIndex<T>(this List<T> list, int index, T defaultValue) {

			if (index > list.Count) {
				return defaultValue;
			}
			
			return list[index];
		}

		/// <summary>
		/// リスト内の値を一つランダムで返します。
		/// リストが空またはnullである場合はdefalutValueを返します。
		/// </summary>
		/// <param name="self"></param>
		/// <param name="defaultValue"></param>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static T GetRandom<T>(this List<T> self, T defaultValue) {
			if (self.IsNullOrEmpty()) {
				return defaultValue;
			}

			return self[UnityEngine.Random.Range(0, self.Count)];
		}

		/// <summary>
		/// リスト内をシャッフルします。
		/// </summary>
		/// <param name="list">List.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public static void Shuffle<T>(List<T> list) {
			if (list.IsNullOrEmpty()) return;

			for (int i = 0; i < list.Count; i++) {
				var temp = list[i];
				var randomIndex = UnityEngine.Random.Range(0, list.Count);
				list[i] = list[randomIndex];
				list[randomIndex] = temp;
			}
		}

		/// <summary>
		/// list同士を比較し、同じであればtrueを返します。
		/// </summary>
		/// <param name="a1"></param>
		/// <param name="a2"></param>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static bool SequenceMatch<T>(this List<T> a1, List<T> a2) {
			if (a1.SafeCount() != a2.SafeCount()) {
				return false;
			}

			return a1.All(a2.Contains);
		}

		/// <summary>
		/// Find an element in a collection by binary searching. 
		/// This requires the collection to be sorted on the values returned by getSubElement
		/// This will compare some derived property of the elements in the collection, rather than the elements
		/// themselves.
		/// </summary>
		/// <typeparam name="TCollection"></typeparam>
		/// <typeparam name="TElement"></typeparam>
		/// <param name="source"></param>
		/// <param name="value"></param>
		/// <param name="getSubElement"></param>
		/// <returns></returns>
		public static int BinarySearch<TCollection, TElement>(
			this ICollection<TCollection> source, TElement value, Func<TCollection, TElement> getSubElement
		) {
			return BinarySearch(source, value, getSubElement, 0, source.Count, null);
		}

		/// <summary>
		/// Find an element in a collection by binary searching. 
		/// This requires the collection to be sorted on the values returned by getSubElement
		/// This will compare some derived property of the elements in the collection, rather than the elements
		/// themselves.
		/// </summary>
		/// <typeparam name="TCollection"></typeparam>
		/// <typeparam name="TElement"></typeparam>
		/// <param name="source"></param>
		/// <param name="value"></param>
		/// <param name="getSubElement"></param>
		/// <param name="comparer"></param>
		/// <returns></returns>
		public static int BinarySearch<TCollection, TElement>(
			this ICollection<TCollection> source, TElement value, Func<TCollection, TElement> getSubElement,
			IComparer<TElement> comparer
		) {
			return BinarySearch(source, value, getSubElement, 0, source.Count, comparer);
		}

		/// <summary>
		/// Find an element in a collection by binary searching. 
		/// This requires the collection to be sorted on the values returned by getSubElement
		/// This will compare some derived property of the elements in the collection, rather than the elements
		/// themselves.
		/// </summary>
		/// <typeparam name="TCollection"></typeparam>
		/// <typeparam name="TElement"></typeparam>
		/// <param name="source"></param>
		/// <param name="value"></param>
		/// <param name="getSubElement"></param>
		/// <param name="index"></param>
		/// <param name="length"></param>
		/// <returns></returns>
		public static int BinarySearch<TCollection, TElement>(
			this ICollection<TCollection> source, TElement value, Func<TCollection, TElement> getSubElement, int index,
			int length
		) {
			return BinarySearch(source, value, getSubElement, index, length, null);
		}

		/// <summary>
		/// Find an element in a collection by binary searching. 
		/// This requires the collection to be sorted on the values returned by getSubElement
		/// This will compare some derived property of the elements in the collection, rather than the elements
		/// themselves.
		/// </summary>
		/// <typeparam name="TCollection"></typeparam>
		/// <typeparam name="TElement"></typeparam>
		/// <param name="source"></param>
		/// <param name="value"></param>
		/// <param name="getSubElement"></param>
		/// <param name="index"></param>
		/// <param name="length"></param>
		/// <param name="comparer"></param>
		/// <returns></returns>
		public static int BinarySearch<TCollection, TElement>(
			this ICollection<TCollection> source, TElement value, Func<TCollection, TElement> getSubElement, int index,
			int length, IComparer<TElement> comparer
		) {
			if (index < 0) {
				throw new ArgumentOutOfRangeException(nameof(index), "index is less than the lower bound of array.");
			}

			if (length < 0) {
				throw new ArgumentOutOfRangeException(nameof(length), "Value has to be >= 0.");
			}

			// re-ordered to avoid possible integer overflow
			if (index > source.Count - length) {
				throw new ArgumentException("index and length do not specify a valid range in array.");
			}

			if (comparer == null) {
				comparer = Comparer<TElement>.Default;
			}

			int min = index;
			int max = index + length - 1;

			while (min <= max) {
				var mid = (min + ((max - min) >> 1));

				var cmp = comparer.Compare(getSubElement(source.ElementAt(mid)), value);

				if (cmp == 0) return mid;

				if (cmp > 0) {
					max = mid - 1;
				}
				else {
					min = mid + 1;
				}
			}

			return ~min;
		}
	}
}