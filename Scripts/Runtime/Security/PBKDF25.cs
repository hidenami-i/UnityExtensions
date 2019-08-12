using System;
using System.Security.Cryptography;
using System.Text;

namespace UnityExtensions
{
	public static class PBKDF25
	{
		/// <summary>
		/// Encrypt to String
		/// </summary>
		/// <param name="password">target string</param>
		/// <param name="saltSize">default 32</param>
		/// <param name="size">default 32</param>
		/// <param name="count">default 1000</param>
		/// <returns></returns>
		public static string Encrypt(string password, int saltSize = 32, int size = 32, int count = 1000) {
			return Convert.ToBase64String(GetHash(password, GetSalt(saltSize), size, count));
		}

		/// <summary>
		/// Encrypt to HexDecimal String.
		/// </summary>
		/// <param name="password"></param>
		/// <param name="saltSize"></param>
		/// <param name="size"></param>
		/// <param name="count"></param>
		/// <returns></returns>
		public static string EncryptToHexDecimal(string password, int saltSize = 32, int size = 32, int count = 1000) {
			byte[] bytes = GetHash(password, GetSalt(saltSize), size, count);
			StringBuilder builder = new StringBuilder();
			foreach (byte b in bytes) {
				builder.Append(b.ToString("x2"));
			}

			return builder.ToString();
		}

		/// <summary>
		/// Gets salt.
		/// </summary>
		/// <param name="saltSize"></param>
		/// <returns></returns>
		static string GetSalt(int saltSize) {
			var bytes = new byte[saltSize];
			using (var provider = new RNGCryptoServiceProvider()) {
				provider.GetBytes(bytes);
			}

			return Convert.ToBase64String(bytes);
		}

		/// <summary>
		/// Gets pbkdf25 byte hash.
		/// </summary>
		/// <param name="password"></param>
		/// <param name="salt"></param>
		/// <param name="size"></param>
		/// <param name="count"></param>
		/// <returns></returns>
		static byte[] GetHash(string password, string salt, int size, int count) {
			byte[] bytes = Encoding.ASCII.GetBytes(salt);
			byte[] byteSalt;

			using (var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, bytes, count)) {
				byteSalt = rfc2898DeriveBytes.GetBytes(size);
			}

			return byteSalt;
		}
	}
}