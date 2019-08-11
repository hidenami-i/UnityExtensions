using System;
using System.Collections.Generic;
using System.Linq;

namespace UnityExtensions
{
	public static class ExIEnumerable
	{
		/// <summary>
		/// Returns true if the IEnumerable is null or empty.
		/// </summary>
		public static bool IsNullOrEmpty<T>(this IEnumerable<T> self) {
			return self == null || !self.Any();
		}

		/// <summary>
		/// Returns true if the IEnumerable is null or not empty.
		/// </summary>
		public static bool IsNotEmpty<T>(this IEnumerable<T> self) {
			return !IsNullOrEmpty(self);
		}

		/// <summary>
		/// Returns the length of the list.
		/// If the list is null it returns 0.
		/// </summary>
		/// <returns>The count.</returns>
		/// <param name="self">Self.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public static int SafeCount<T>(this IEnumerable<T> self) {
			if (IsNullOrEmpty(self)) {
				return 0;
			}

			return self.Count();
		}

		/// <summary>
		/// Run Any function securely.
		/// If the list is empty or null it returns false.
		/// </summary>
		/// <returns><c>true</c>, if any was safed, <c>false</c> otherwise.</returns>
		/// <param name="list">List.</param>
		/// <param name="predicate">Predicate.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public static bool SafeAny<T>(this IEnumerable<T> self, Func<T, bool> predicate) {
			if (self.IsNullOrEmpty()) {
				return false;
			}

			return self.Any(predicate);
		}

		/// <summary>
		/// Returns the IEnumerable excluding the first element of IEnumerable.
		/// </summary>
		/// <returns>The first.</returns>
		/// <param name="source">Source.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public static IEnumerable<T> ButFirst<T>(this IEnumerable<T> source) {
			return source.Skip(1);
		}

		/// <summary>
		/// Returns the IEnumerable exluding the last element of IEnumerable.
		/// </summary>
		/// <returns>The last.</returns>
		/// <param name="source">Source.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public static IEnumerable<T> ButLast<T>(this IEnumerable<T> source) {
			var last = default(T);
			var first = true;

			foreach (var x in source) {
				if (first) {
					first = false;
				}
				else {
					yield return last;
				}

				last = x;
			}
		}

		/// <summary>
		/// Returns the largest element that matches the condition.
		/// </summary>
		/// <param name="source"></param>
		/// <param name="score"></param>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static T MaxBy<T>(this IEnumerable<T> source, Func<T, IComparable> score) {
			return source.Aggregate((x, y) => score(x).CompareTo(score(y)) > 0 ? x : y);
		}

		/// <summary>
		/// Returns the largest element that matches the condition.
		/// </summary>
		/// <param name="source"></param>
		/// <param name="selector"></param>
		/// <typeparam name="TSource"></typeparam>
		/// <typeparam name="TKey"></typeparam>
		/// <returns></returns>
		public static TSource MaxBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> selector) {
			return source.MaxBy(selector, Comparer<TKey>.Default);
		}

		/// <summary>
		/// Returns the largest element that matches the condition.
		/// </summary>
		/// <param name="source"></param>
		/// <param name="selector"></param>
		/// <param name="comparer"></param>
		/// <typeparam name="TSource"></typeparam>
		/// <typeparam name="TKey"></typeparam>
		/// <returns></returns>
		/// <exception cref="InvalidOperationException"></exception>
		public static TSource MaxBy<TSource, TKey>(
			this IEnumerable<TSource> source, Func<TSource, TKey> selector, IComparer<TKey> comparer
		) {
			using (IEnumerator<TSource> sourceIterator = source.GetEnumerator()) {
				if (!sourceIterator.MoveNext()) {
					throw new InvalidOperationException("Sequence was empty");
				}

				TSource max = sourceIterator.Current;
				TKey maxKey = selector(max);

				while (sourceIterator.MoveNext()) {
					TSource candidate = sourceIterator.Current;
					TKey candidateProjected = selector(candidate);

					if (comparer.Compare(candidateProjected, maxKey) > 0) {
						max = candidate;
						maxKey = candidateProjected;
					}
				}

				return max;
			}
		}

		/// <summary>
		/// Returns the smallest element that matches the condition.
		/// </summary>
		/// <param name="source"></param>
		/// <param name="selector"></param>
		/// <typeparam name="TSource"></typeparam>
		/// <typeparam name="TKey"></typeparam>
		/// <returns></returns>
		public static TSource MinBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> selector) {
			return source.MinBy(selector, Comparer<TKey>.Default);
		}

		/// <summary>
		/// Returns the smallest element that matches the condition.
		/// </summary>
		/// <param name="source"></param>
		/// <param name="selector"></param>
		/// <param name="comparer"></param>
		/// <typeparam name="TSource"></typeparam>
		/// <typeparam name="TKey"></typeparam>
		/// <returns></returns>
		/// <exception cref="InvalidOperationException"></exception>
		public static TSource MinBy<TSource, TKey>(
			this IEnumerable<TSource> source, Func<TSource, TKey> selector, IComparer<TKey> comparer
		) {
			using (IEnumerator<TSource> sourceIterator = source.GetEnumerator()) {
				if (!sourceIterator.MoveNext()) {
					throw new InvalidOperationException("Sequence was empty");
				}

				TSource min = sourceIterator.Current;
				TKey minKey = selector(min);

				while (sourceIterator.MoveNext()) {
					TSource candidate = sourceIterator.Current;
					TKey candidateProjected = selector(candidate);
					if (comparer.Compare(candidateProjected, minKey) < 0) {
						min = candidate;
						minKey = candidateProjected;
					}
				}

				return min;
			}
		}

		/// <summary>
		/// Returns the IEnumerable which moved the first element of IEnumerable to the last element.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="source"></param>
		/// <returns></returns>
		public static IEnumerable<T> RotateLeft<T>(this IEnumerable<T> source) {
			var enumeratedList = source as IList<T> ?? source.ToList();
			return enumeratedList.ButFirst().Concat(enumeratedList.Take(1));
		}

		/// <summary>
		/// Returns the IEnumerable which moved the last element of IEnumerable to the first element.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="source"></param>
		/// <returns></returns>
		public static IEnumerable<T> RotateRight<T>(this IEnumerable<T> source) {
			var enumeratedList = source as IList<T> ?? source.ToList();
			yield return enumeratedList.Last();

			foreach (var item in enumeratedList.ButLast()) {
				yield return item;
			}
		}

		/// <summary>
		/// Returns the first half IEnumerable of IEnumerable.
		/// </summary>
		public static IEnumerable<T> TakeHalf<T>(this IEnumerable<T> source) {
			if (source.IsNullOrEmpty()) {
				return null;
			}

			int count = source.Count();
			return source.Take(count / 2);
		}

		/// <summary>
		/// NonAllocationの配列に変換します。
		/// 返されるintはサイズです。
		/// </summary>
		/// <param name="source"></param>
		/// <param name="array"></param>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static int ToArrayNonAllc<T>(this IEnumerable<T> source, ref T[] array) {
			int index = 0;
			foreach (var item in source) {
				if (array.Length == index) {
					int newSize = index == 0 ? 4 : index * 2;
					Array.Resize(ref array, newSize);
				}

				array[index++] = item;
			}

			return index;
		}
	}
}