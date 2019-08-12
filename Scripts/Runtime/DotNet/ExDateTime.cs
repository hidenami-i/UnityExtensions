using System;

namespace UnityExtensions
{
	/// <summary>
	/// DateTime Extensions
	/// </summary>
	public static class ExDateTime
	{
		/// <summary> 協定時間を取得します。 </summary>
		public static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

		/// <summary>
		/// 対象のdatetimeがUnixEpockであるかどうかを判定します。
		/// </summary>
		/// <returns><c>true</c> if is unix epoch the specified dateTime; otherwise, <c>false</c>.</returns>
		/// <param name="dateTime">Date time.</param>
		public static bool IsUnixEpoch(this DateTime dateTime) {
			return dateTime == UnixEpoch;
		}

		/// <summary>
		/// 現在時刻からUnixTimeを計算する。
		/// </summary>
		public static double SecondsSinceEpoch => ToEpochSeconds(DateTime.UtcNow);

		/// <summary>
		/// UnixTimeからDateTimeに変換します。
		/// </summary>
		/// <returns>現在時刻のDateTime</returns>
		/// <param name="totalSeconds">現在のEpochからの経過秒数</param>
		public static DateTime FromEpochSeconds(double totalSeconds) {
			return UnixEpoch.AddSeconds(totalSeconds);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="totalSeconds"></param>
		/// <returns></returns>
		public static DateTime FromEpochSecondsToLocalTime(double totalSeconds) {
			return FromEpochSeconds(totalSeconds).ToLocalTime();
		}

		/// <summary>
		/// 指定時間を協定時間からの経過秒数として計算します。
		/// 小数点以下は切り捨てます。
		/// </summary>
		/// <returns>協定時間からの経過秒数</returns>
		/// <param name="timestamp">どのタイムゾーンであってもutcに変換されます</param>
		public static double ToEpochSeconds(this DateTime timestamp) {
			return Math.Floor((timestamp.ToUniversalTime() - UnixEpoch).TotalSeconds);
		}

		/// <summary>
		/// yyyy/MM/dd HH:mm:ss 形式の文字列に変換して返します。
		/// </summary>
		public static string ToString(this DateTime self) {
			return self.ToString("yyyy/MM/dd HH:mm:ss");
		}

		/// <summary>
		/// yyyy/MM/dd 形式の文字列に変換して返します。
		/// </summary>
		public static string ToShortDateString(this DateTime self) {
			return self.ToString("yyyy/MM/dd");
		}

		/// <summary>
		/// yyyy年M月d日 形式の文字列に変換して返します。
		/// </summary>
		public static string ToLongDateString(this DateTime self) {
			return self.ToString("yyyy年M月d日");
		}

		/// <summary>
		/// yyyy年M月d日 HH:mm:ss 形式の文字列に変換して返します。
		/// </summary>
		public static string ToFullDateTimeString(this DateTime self) {
			return self.ToString("yyyy年M月d日 HH:mm:ss");
		}

		/// <summary>
		/// MM/dd HH:mm 形式の文字列に変換して返します。
		/// </summary>
		public static string ToMiddleDateTimeString(this DateTime self) {
			return self.ToString("MM/dd HH:mm");
		}

		/// <summary>
		/// HH:mm 形式の文字列に変換して返します。
		/// </summary>
		public static string ToShortTimeString(this DateTime self) {
			return self.ToString("HH:mm");
		}

		/// <summary>
		/// HH:mm:ss 形式の文字列に変換して返します。
		/// </summary>
		public static string ToLongTimeString(this DateTime self) {
			return self.ToString("HH:mm:ss");
		}

		/// <summary>
		/// System.DateTime.Nowを基準として二つの時間の期限内であるかを判定します。
		/// </summary>
		/// <param name="dateTime1"></param>
		/// <param name="dateTime2"></param>
		/// <returns></returns>
		public static bool IsWithin(DateTime dateTime1, DateTime dateTime2) {
			DateTime now = DateTime.Now;
			return IsWithin(dateTime1, dateTime2, now);
		}

		/// <summary>
		/// 二つの時間の期限内であるかを判定します。
		/// </summary>
		/// <param name="dateTime1"></param>
		/// <param name="dateTime2"></param>
		/// <param name="standardDateTime">基準時間</param>
		/// <returns></returns>
		public static bool IsWithin(DateTime dateTime1, DateTime dateTime2, DateTime standardDateTime) {
			return standardDateTime >= dateTime1 && standardDateTime <= dateTime2;
		}
	}
}