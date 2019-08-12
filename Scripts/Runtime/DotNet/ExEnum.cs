using System;
using System.Linq;
using System.Collections.Generic;

namespace UnityExtensions
{
	public static class ExEnum
	{
		/// <summary>
		/// Cast the enumerated type to the IEnumerable type and return it.
		/// </summary>
		/// <param name="enumList">Enum list.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public static IEnumerable<T> ToIEnumerable<T>() where T : struct, IComparable, IFormattable, IConvertible {
			return Enum.GetValues(typeof(T)).Cast<T>();
		}

		/// <summary>
		/// Cast the enumerated type to the IEnumrable type and return it.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static List<T> ToList<T>() where T : struct, IComparable, IFormattable, IConvertible {
			List<T> list = Enum.GetValues(typeof(T)).Cast<T>().ToList();
			return list;
		}

		public static string GetName<T>(T value) where T : struct, IComparable, IFormattable, IConvertible {
			return Enum.GetName(typeof(T), value);
		}

		public static string Format<T>(T value, string format) where T : struct, IComparable, IFormattable, IConvertible {
			return Enum.Format(typeof(T), value, format);
		}

		/// <summary>
		/// Converts the specified integer value to an enumerated type and return it.
		/// </summary>
		/// <param name="value"></param>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static T ToObject<T>(int value) where T : struct, IComparable, IFormattable, IConvertible {
			return (T)Enum.ToObject(typeof(T), value);
		}

		/// <summary>
		/// Returns the name of the enumerated type as a list.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static List<string> ToNameList<T>() where T : struct, IComparable, IFormattable, IConvertible {
			List<T> list = Enum.GetValues(typeof(T)).Cast<T>().ToList();
			List<string> result = new List<string>();
			foreach (var item in list) {
				result.Add(item.ToString());
			}

			return result;
		}

		/// <summary>
		/// Gets the number of items defined by the enumeration type.
		/// </summary>
		public static int Count<T>() where T : struct, IComparable, IFormattable, IConvertible {
			return Enum.GetNames(typeof(T)).SafeCount();
		}

		/// <summary>
		/// Convert int type to enumerated type.
		/// </summary>
		/// <returns>The enum.</returns>
		/// <param name="num">Number.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public static T IntToEnum<T>(int num) where T : struct, IComparable, IFormattable, IConvertible {
			return ToObject<T>(num);
		}

		/// <summary>
		/// Convers string type to enumerated type.
		/// </summary>
		/// <returns>The enum.</returns>
		/// <param name="value">Value.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public static T StringToEnum<T>(string value) where T : struct, IComparable, IFormattable, IConvertible {
			return (T)Enum.Parse(typeof(T), value, false);
		}

		/// <summary>
		/// 指定された文字列が列挙型に変換できるかどうかを返します。
		/// </summary>
		public static bool IsEnum<T>(string value) where T : struct, IComparable, IFormattable, IConvertible {
			T result;
			return TryParse(value, out result);
		}

		/// <summary>
		/// 指定された文字列を列挙型に変換して成功したかどうかを返します。
		/// </summary>
		public static bool TryParse<T>(string value, out T result) where T : struct, IComparable, IFormattable, IConvertible {
			return TryParse(value, true, out result);
		}

		/// <summary>
		/// 指定された文字列を列挙型に変換して成功したかどうかを返します。
		/// </summary>
		public static bool TryParse<T>(string value, bool ignoreCase, out T result) where T : struct, IComparable, IFormattable, IConvertible {
			try {
				result = (T)Enum.Parse(typeof(T), value, ignoreCase);
				return true;
			}
			catch {
				result = default;
				return false;
			}
		}
	}
}