using System;
using System.Security.Cryptography;

namespace UnityExtensions
{
	public static class Aes128
	{
		/// <summary>
		/// Rijnedael instance.
		/// </summary>
		static readonly RijndaelManaged Rijndael = new RijndaelManaged
		{
			Padding = PaddingMode.Zeros, Mode = CipherMode.CBC, KeySize = 128, BlockSize = 128
		};

		public static byte[] Encrypt(byte[] contents, string password, string salt) {
			ICryptoTransform encryptor = CreateEncryptor(password, salt);
			byte[] encrypted = encryptor.TransformFinalBlock(contents, 0, contents.Length);
			encryptor.Dispose();
			return encrypted;
		}

		public static byte[] Encrypt(string str, string password, string salt) {
			byte[] contents = System.Text.Encoding.UTF8.GetBytes(str);
			return Encrypt(contents, password, salt);
		}

		public static string EncryptToString(byte[] contents, string password, string salt) {
			return Convert.ToBase64String(Encrypt(contents, password, salt));
		}

		public static string EncryptToString(string str, string password, string salt) {
			return Convert.ToBase64String(Encrypt(str, password, salt));
		}

		public static byte[] Decrypt(byte[] contents, string password, string salt) {
			ICryptoTransform decryptor = CreateDecryptor(password, salt);
			byte[] plain = decryptor.TransformFinalBlock(contents, 0, contents.Length);
			decryptor.Dispose();
			return plain;
		}

		public static byte[] Decrypt(string str, string password, string salt) {
			byte[] contents = Convert.FromBase64String(str);
			return Decrypt(contents, password, salt);
		}

		public static string DecryptToString(byte[] contents, string password, string salt) {
			return System.Text.Encoding.UTF8.GetString(Decrypt(contents, password, salt));
		}

		public static string DecryptToString(string str, string password, string salt) {
			return System.Text.Encoding.UTF8.GetString(Decrypt(str, password, salt));
		}

		/// <summary>
		/// Create encryption key and initialization vector from password and salt.
		/// </summary>
		/// <returns>The derive bytes.</returns>
		internal static Rfc2898DeriveBytes GenerateDeriveBytes(string password, string salt) {
			byte[] bytes = System.Text.Encoding.UTF8.GetBytes(salt);
			Rfc2898DeriveBytes deriveBytes = new Rfc2898DeriveBytes(password, bytes) {IterationCount = 1000};
			return deriveBytes;
		}

		internal static ICryptoTransform CreateEncryptor(string password, string salt) {
			Rfc2898DeriveBytes deriveBytes = GenerateDeriveBytes(password, salt);
			Rijndael.Key = deriveBytes.GetBytes(Rijndael.KeySize / 8);
			Rijndael.IV = deriveBytes.GetBytes(Rijndael.BlockSize / 8);
			return Rijndael.CreateEncryptor();
		}

		internal static ICryptoTransform CreateDecryptor(string password, string salt) {
			Rfc2898DeriveBytes deriveBytes = GenerateDeriveBytes(password, salt);
			Rijndael.Key = deriveBytes.GetBytes(Rijndael.KeySize / 8);
			Rijndael.IV = deriveBytes.GetBytes(Rijndael.BlockSize / 8);
			return Rijndael.CreateDecryptor();
		}
	}
}