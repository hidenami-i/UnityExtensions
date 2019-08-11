using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Reflection;

namespace UnityExtensions
{
	public static class ExString
	{
		const int IndexNotFound = -1;
		const string Empty = "";
		const string NumericChars = "0123456789";
		const string PasswordChars = "0123456789abcdefghijklmnopqrstuvwxyz";
		const string UniqueChars = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

		/// <summary>
		/// 指定文字が最後に発見された文字より前の文字を切り出す
		/// </summary>
		/// <returns>The before last.</returns>
		/// <param name="str">String.</param>
		/// <param name="separator">Separator.</param>
		public static string SubstringBeforeLast(this string str, string separator) {
			if (string.IsNullOrEmpty(str) || string.IsNullOrEmpty(separator)) {
				return str;
			}

			int position = str.LastIndexOf(separator);
			if (position == IndexNotFound) {
				return str;
			}

			return str.Substring(0, position);
		}

		/// <summary>
		/// 右から指定した文字数切り出す
		/// </summary>
		/// <param name="str">String.</param>
		/// <param name="len">Length.</param>
		public static string Rigth(this string str, int len) {
			if (str == null) {
				return null;
			}

			if (len < 0) {
				return Empty;
			}

			if (str.Length <= len) {
				return str;
			}

			string result = str.Substring(str.Length - len);
			if (result.StartsWith("0")) {
				return RemoveStart(result, "0");
			}

			return result;
		}

		/// <summary>
		/// 文字列の先頭を指定文字で削除する
		/// </summary>
		/// <returns>The start.</returns>
		/// <param name="str">String.</param>
		/// <param name="remove">Remove.</param>
		public static string RemoveStart(this string str, string remove) {
			if (string.IsNullOrEmpty(str) || string.IsNullOrEmpty(remove)) {
				return str;
			}

			if (str.StartsWith(remove)) {
				return str.Substring(remove.Length);
			}

			return str;
		}

		/// <summary>
		/// 改行文字を削除する
		/// </summary>
		/// <returns>The new line.</returns>
		/// <param name="str">Self.</param>
		public static string RemoveNewLine(this string str) {
			return str.Replace("\r", "").Replace("\n", "");
		}
		
		/// <summary>
		/// Reverse the target string.
		/// <para>Hello → olleH</para>
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		public static string Reverse(this string str) {
			return string.Join("", str.Reverse());
		}

		/// <summary>
		/// 指定文字が最初に発見された文字より後の文字を切り出す
		/// </summary>
		/// <returns>The string after.</returns>
		/// <param name="str">String.</param>
		/// <param name="separator">Separator.</param>
		public static string SubStringAfter(this string str, string separator) {
			if (str.IsNullOrEmpty()) {
				return str;
			}

			if (separator == null) {
				return Empty;
			}

			int pos = str.IndexOf(separator);
			if (pos == IndexNotFound) {
				return Empty;
			}

			return str.Substring(pos + separator.Length);
		}

		/// <summary>
		/// 指定文字が最後に発見された文字より後の文字を切り出す
		/// </summary>
		/// <returns>The string after last.</returns>
		/// <param name="str">String.</param>
		/// <param name="separator">Separator.</param>
		public static string SubStringAfterLast(this string str, string separator) {
			if (str.IsNullOrEmpty()) {
				return str;
			}

			if (separator.IsNullOrEmpty()) {
				return Empty;
			}

			int pos = str.LastIndexOf(separator);
			if (pos == IndexNotFound || pos == str.Length - separator.Length) {
				return Empty;
			}

			return str.Substring(pos + separator.Length);
		}

		/// <summary>
		/// D2のToStringへ変換する
		/// </summary>
		/// <returns>The to string.</returns>
		/// <param name="str">String.</param>
		public static string D2ToString(this string str) {
			if (str.IsNullOrEmpty()) {
				return str;
			}

			return str.ToInt().D2ToString();
		}

		/// <summary>
		/// D2s to string.
		/// </summary>
		/// <returns>The to string.</returns>
		/// <param name="num">Number.</param>
		public static string D2ToString(this int num) {
			return num.ToString("D2");
		}

		/// <summary>
		/// 渡された数値を3桁カンマ区切りの文字列に変換して返します
		/// </summary>
		public static string WithComma(this int self) {
			return self.ToString("N0");
		}

		/// <summary>
		/// 渡された数値を3桁カンマ区切りの文字列に変換して返します
		/// </summary>
		/// <param name="self"></param>
		/// <returns></returns>
		public static string WithComma(this long self) {
			return self.ToString("N0");
		}

		/// <summary>
		/// 渡された数値を文字列に変換して返します
		/// </summary>
		public static string IntToString(this int self) {
			return self.ToString();
		}

		/// <summary>
		/// long型のbyteを適切な単位へと変換します。
		/// </summary>
		/// <returns>The byte to string.</returns>
		/// <param name="amount">bytes</param>
		/// <param name="rounding">表示する小数点の桁数</param>
		public static string ToHumanReadableSize(long size)
		{
			double bytes = size;
			
			if (bytes <= 1024) return bytes.ToString("f2") + " B";

			bytes = bytes / 1024;
			if (bytes <= 1024) return bytes.ToString("f2") + " KB";

			bytes = bytes / 1024;
			if (bytes <= 1024) return bytes.ToString("f2") + " MB";

			bytes = bytes / 1024;
			if (bytes <= 1024) return bytes.ToString("f2") + " GB";

			bytes = bytes / 1024;
			if (bytes <= 1024) return bytes.ToString("f2") + " TB";

			bytes = bytes / 1024;
			if (bytes <= 1024) return bytes.ToString("f2") + " PB";

			bytes = bytes / 1024;
			if (bytes <= 1024) return bytes.ToString("f2") + " EB";

			bytes = bytes / 1024;
			return bytes + " ZB";
		}

		/// <summary>
		/// ランダムな数字のみで構成された文字列を生成して返します。
		/// </summary>
		/// <returns>The numeric chars.</returns>
		/// <param name="length">Length.</param>
		public static string GenerateNumericChars(int length) {
			StringBuilder sb = new StringBuilder(length);
			System.Random random = new System.Random();

			for (int i = 0; i < length; i++) {
				int pos = random.Next(NumericChars.Length);
				char c = NumericChars[pos];
				sb.Append(c);
			}

			return sb.ToString();
		}
		
		/// <summary>
		/// ランダムな小文字の英数字のみで構成された文字列を生成して返します。
		/// </summary>
		public static string GeneratePassword(int length) {
			StringBuilder sb = new StringBuilder(length);
			System.Random random = new System.Random();

			for (int i = 0; i < length; i++) {
				int pos = random.Next(PasswordChars.Length);
				char c = PasswordChars[pos];
				sb.Append(c);
			}

			return sb.ToString();
		}

		/// <summary>
		/// ランダムな英数字で構成された文字列を生成して返します。
		/// </summary>
		/// <returns>The unique I.</returns>
		/// <param name="length">Length.</param>
		public static string GenerateUniqueID(int length) {
			StringBuilder sb = new StringBuilder(length);
			System.Random random = new System.Random();

			for (int i = 0; i < length; i++) {
				int pos = random.Next(UniqueChars.Length);
				char c = UniqueChars[pos];
				sb.Append(c);
			}

			return sb.ToString();
		}

		/// <summary>
		/// Systemで生成されるGuidを返します。
		/// </summary>
		/// <returns>Generates the unique ID</returns>
		public static string GenerateUUID() {
			Guid guid = Guid.NewGuid();
			return guid.ToString();
		}

		/// <summary>
		/// Splits the camel case.
		/// </summary>
		/// <returns>The camel case.</returns>
		/// <param name="str">String.</param>
		public static string SplitCamelCase(this string str) {
			if (string.IsNullOrEmpty(str)) {
				return str;
			}

			string camelCase =
				Regex.Replace(Regex.Replace(str, @"(\P{Ll})(\P{Ll}\p{Ll})", "$1 $2"), @"(\p{Ll})(\P{Ll})", "$1 $2");
			string firstLetter = camelCase.Substring(0, 1).ToUpper();

			if (str.Length > 1) {
				string rest = camelCase.Substring(1);

				return firstLetter + rest;
			}

			return firstLetter;
		}

		/// <summary>
		/// スネークケースをアッパーキャメル(パスカル)ケースに変換します
		/// 例) quoted_printable_encode → QuotedPrintableEncode
		/// </summary>
		public static string SnakeToUpperCamel(this string self) {
			if (string.IsNullOrEmpty(self)) {
				return self;
			}

			return self.Split(new[] {'_'}, StringSplitOptions.RemoveEmptyEntries)
						.Select(s => char.ToUpperInvariant(s[0]) + s.Substring(1, s.Length - 1))
						.Aggregate(string.Empty, (s1, s2) => s1 + s2);
		}

		/// <summary>
		/// スネークケースをローワーキャメル(キャメル)ケースに変換します
		/// 例) quoted_printable_encode → quotedPrintableEncode
		/// </summary>
		public static string SnakeToLowerCamel(this string self) {
			if (string.IsNullOrEmpty(self)) {
				return self;
			}

			return self.SnakeToUpperCamel().Insert(0, char.ToLowerInvariant(self[0]).ToString()).Remove(1, 1);
		}

		/// <summary>
		/// BOMがついていたら削除して文字列を返します
		/// </summary>
		/// <returns>The contains bom.</returns>
		/// <param name="str">String.</param>
		public static string TrimContainsBom(this string str) {
			if (str.StartsWith(Encoding.UTF8.GetString(Encoding.UTF8.GetPreamble()))) {
				return str.Remove(0, Encoding.UTF8.GetString(Encoding.UTF8.GetPreamble()).Length);
			}

			return str;
		}

		/// <summary>
		/// 指定された文字列からType型へ変換して返します
		/// </summary>
		/// <returns>The type.</returns>
		public static Type ToType(this string str) {
			foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies()) {
				foreach (Type type in assembly.GetTypes()) {
					if (type.Name == str) {
						return type;
					}
				}
			}

			return null;
		}

		/// <summary>
		/// 指定された文字列がメールアドレスかどうか判定します。
		/// </summary>
		/// <returns><c>true</c> if is mail address the specified str; otherwise, <c>false</c>.</returns>
		/// <param name="str">String.</param>
		public static bool IsMailAddress(string str) {
			if (string.IsNullOrEmpty(str)) {
				return false;
			}

			return Regex.IsMatch(str, @"^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$", RegexOptions.IgnoreCase);
		}

		/// <summary>
		/// 指定した文字列に含まれる文字がすべてひらがなかどうかを示します。
		/// </summary>
		/// <param name="str">評価する文字列。</param>
		/// <returns>c に含まれる文字がすべてひらがなである場合は true。
		/// それ以外の場合は false。</returns>
		public static bool IsHiragana(this string str) {
			if (string.IsNullOrEmpty(str)) {
				return false;
			}

			foreach (char c in str) {
				if (!IsHiragana(c)) {
					return false;
				}
			}

			return true;
		}

		/// <summary>
		/// 指定した文字列に含まれる文字がすべてカタカナかどうかを示します。
		/// </summary>
		/// <param name = "str"></param>
		/// <param name="str">評価する文字列。</param>
		/// <returns>c に含まれる文字がすべてカタカナである場合は true。
		/// それ以外の場合は false。</returns>
		public static bool IsKatakana(this string str) {
			if (string.IsNullOrEmpty(str)) {
				return false;
			}

			foreach (char c in str) {
				if (!IsKatakana(c)) {
					return false;
				}
			}

			return true;
		}

		/// <summary>
		/// 指定した文字列に含まれる文字がすべて全角カタカナかどうかを示します。
		/// </summary>
		/// <param name = "str"></param>
		/// <param name="str">評価する文字列。</param>
		/// <returns>c に含まれる文字がすべて全角カタカナである場合は true。
		/// それ以外の場合は false。</returns>
		public static bool IsZenkakuKatakana(this string str) {
			if (string.IsNullOrEmpty(str)) {
				return false;
			}

			foreach (char c in str) {
				if (!IsZenkakuKatakana(c)) {
					return false;
				}
			}

			return true;
		}

		/// <summary>
		/// 指定した文字列に含まれる文字が全てひらがなとカタカナかどうかを示します
		/// </summary>
		/// <returns><c>true</c> if is hiragana and katakana the specified str; otherwise, <c>false</c>.</returns>
		/// <param name="str">String.</param>
		public static bool IsHiraganaAndKatakana(this string str) {
			if (string.IsNullOrEmpty(str)) {
				return false;
			}

			foreach (char c in str) {
				if (!IsKatakana(c)) {
					if (!IsHiragana(c)) {
						return false;
					}
				}
			}

			return true;
		}


		/// <summary>
		/// 文字列に半角が含まれているかどうか
		/// </summary>
		/// <returns><c>true</c> if is not contain one byte char the specified str; otherwise, <c>false</c>.</returns>
		/// <param name="str">String.</param>
		public static bool ContainsOneByteChar(this string str) {
			foreach (var c in str) {
				if (!IsChar2Byte(c)) {
					return true;
				}
			}

			return false;
		}

		/// <summary>
		/// 文字列内に含まれている空白文字を全て削除します。
		/// </summary>
		/// <returns>The whitespace.</returns>
		/// <param name="str">String.</param>
		public static string DeleteWhitespace(this String str) {
			if (string.IsNullOrEmpty(str)) {
				return str;
			}

			int sz = str.Length;
			char[] chs = new char[sz];
			int count = 0;
			for (int i = 0; i < sz; i++) {

				if (!char.IsWhiteSpace(str.ElementAt(i))) {
					chs[count++] = str.ElementAt(i);
				}
			}

			return count == sz ? str : new string(chs, 0, count);

		}

		/// <summary>
		/// char c が2バイト文字（全角文字）であるかどうか
		/// </summary>
		/// <returns><c>true</c> if is char2 byte the specified c; otherwise, <c>false</c>.</returns>
		/// <param name="c">C.</param>
		static bool IsChar2Byte(char c) {
			return !(c >= 0x0 && c < 0x81 || c == 0xf8f0 || c >= 0xff61 && c < 0xffa0 || c >= 0xf8f1 && c < 0xf8f4);
		}

		/// <summary>
		/// 文字列の長さが指定範囲ないであるか
		/// </summary>
		/// <returns><c>true</c> if is range by length the specified str min max; otherwise, <c>false</c>.</returns>
		/// <param name="str">String.</param>
		/// <param name="min">Minimum.</param>
		/// <param name="max">Max.</param>
		public static bool IsRangeByLength(this string str, int min, int max) {
			if (str.IsNullOrEmpty()) {
				return false;
			}

			return str.Length >= min && str.Length <= max;

		}

		/// <summary>
		/// 文字列の長さが指定範囲ないであるか
		/// </summary>
		/// <returns><c>true</c> if is range by length the specified str min max; otherwise, <c>false</c>.</returns>
		/// <param name="str">String.</param>
		/// <param name="min">Minimum.</param>
		/// <param name="max">Max.</param>
		public static bool IsRangeByLengthWithTrim(this string str, int min, int max) {
			return str.Trim().IsRangeByLength(min, max);
		}

		/// <summary>
		/// 文字列中にある特定の文字をカウントする
		/// </summary>
		/// <returns>文字数</returns>
		/// <param name="s">文字列</param>
		/// <param name="c">探す文字</param>
		public static int GetCountChar(this string s, char c) {
			return s.Length - s.Replace(c.ToString(), "").Length;
		}

		/// <summary>
		/// 対象の文字列が禁止文字列を含んでいるかどうか判定します。
		/// </summary>
		/// <returns><c>true</c>, 含んでいれば, <c>false</c> 含んでいなければ</returns>
		/// <param name="targetString">対象文字列</param>
		/// <param name="prohibitedCharacters">禁止文字列</param>
		public static bool ContainsProhibitedCharacter(string targetString, string[] prohibitedCharacters) {
			return prohibitedCharacters.Any(targetString.Contains);
		}

		/// <summary>
		/// 指定した Unicode 文字が、ひらがなかどうかを判定します。
		/// </summary>
		/// <param name="c">評価する Unicode 文字。</param>
		/// <returns>c がひらがなである場合は true。それ以外の場合は false。</returns>
		public static bool IsHiragana(this char c) {
			return ('\u3041' <= c && c <= '\u309F') || c == '\u30FC' || c == '\u30A0';
		}

		/// <summary>
		/// 指定した Unicode 文字が、カタカナかどうかを判定します。
		/// </summary>
		/// <param name="c">評価する Unicode 文字。</param>
		/// <returns>c がカタカナである場合は true。それ以外の場合は false。</returns>
		public static bool IsKatakana(this char c) {
			return '\u30A0' <= c && c <= '\u30FF' || '\u31F0' <= c && c <= '\u31FF' || '\u3099' <= c && c <= '\u309C' ||
					'\uFF65' <= c && c <= '\uFF9F';
		}

		/// <summary>
		/// 指定した Unicode 文字が、全角カタカナかどうかを判定します。
		/// </summary>
		/// <param name="c">評価する Unicode 文字。</param>
		/// <returns>c が全角カタカナである場合は true。それ以外の場合は false。</returns>
		public static bool IsZenkakuKatakana(this char c) {
			return '\u30A0' <= c && c <= '\u30FF' || '\u31F0' <= c && c <= '\u31FF' || '\u3099' <= c && c <= '\u309C';
		}

		/// <summary>
		/// 文字列をbool型へ変換します。
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		public static bool ToBoolean(this string str) {
			try {
				return Convert.ToBoolean(str);
			}
			catch (System.Exception) {
				return false;
			}
		}

		/// <summary>
		/// 文字列からByteへ変換します
		/// </summary>
		public static byte ToByte(this string str, byte defaultValue = 0) {
			if (str == null) {
				return defaultValue;
			}

			try {
				return byte.Parse(str);
			}
			catch (System.Exception) {
				return defaultValue;
			}
		}

		/// <summary>
		/// 文字列からIntへ変換します
		/// </summary>
		public static int ToInt(this string str, int defaultValue = 0) {
			if (str == null) {
				return defaultValue;
			}

			try {
				return int.Parse(str);
			}
			catch (System.Exception) {
				return defaultValue;
			}
		}

		/// <summary>
		/// 文字列からFloatへ変換します
		/// </summary>
		public static float ToFloat(this string str, float defaultValue = 0.0f) {
			if (str == null) {
				return defaultValue;
			}

			try {
				return float.Parse(str);
			}
			catch (System.Exception) {
				return defaultValue;
			}
		}

		/// <summary>
		/// 文字列からDoubleへ変換します
		/// </summary>
		public static double ToDouble(this string str, double defaultValue = 0.0d) {
			if (str == null) {
				return defaultValue;
			}

			try {
				return double.Parse(str);
			}
			catch (System.Exception) {
				return defaultValue;
			}
		}

		/// <summary>
		/// 文字列からDoubleへLong変換します
		/// </summary>
		public static long ToLong(this string str, long defaultValue = 0L) {
			if (str == null) {
				return defaultValue;
			}

			try {
				return long.Parse(str);
			}
			catch (System.Exception) {
				return defaultValue;
			}
		}

		/// <summary>
		/// 文字列配列のすべての要素を連結します。各要素の間には、指定した区切り記号が挿入されます
		/// </summary>
		public static string Join(string separator, IEnumerable<string> value) {
			return string.Join(separator, value.ToArray());
		}

		/// <summary>
		/// 文字列配列の指定した要素を連結します。各要素の間には、指定した区切り記号が挿入されます
		/// </summary>
		public static string Join(string separator, IEnumerable<string> value, int startIndex, int count) {
			return string.Join(separator, value.ToArray(), startIndex, count);
		}
	}
}