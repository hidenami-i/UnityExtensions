using System;
using System.Text;
using System.Security.Cryptography;

namespace UnityExtensions
{
	public static class Encrypt
	{
		public static string DeTripleDesc(string key, string value) {
			// retrieve encryped
			byte[] secret = MD5(key);
			byte[] bytes = Convert.FromBase64String(value);

			// descrypt value 3DES
			TripleDES des = new TripleDESCryptoServiceProvider();
			des.Key = secret;
			des.Mode = CipherMode.ECB;
			ICryptoTransform xform = des.CreateDecryptor();
			byte[] decryped = xform.TransformFinalBlock(bytes, 0, bytes.Length);

			return Encoding.UTF8.GetString(decryped);
		}

		public static string TripleDesc(string key, string value) {
			// encrypt value
			byte[] secret = MD5(key);
			byte[] bytes = Encoding.UTF8.GetBytes(value);

			TripleDES des = new TripleDESCryptoServiceProvider();
			des.Key = secret;
			des.Mode = CipherMode.ECB;
			ICryptoTransform xform = des.CreateEncryptor();
			byte[] encrypted = xform.TransformFinalBlock(bytes, 0, bytes.Length);

			// convert encrypt
			return Convert.ToBase64String(encrypted);
		}

		public static byte[] MD5(byte[] bytes) {
			MD5CryptoServiceProvider md5Hash = new MD5CryptoServiceProvider();
			return md5Hash.ComputeHash(bytes);
		}

		public static byte[] MD5(string str) {
			return MD5(Encoding.UTF8.GetBytes(str));
		}

		public static string MD5ToString(byte[] bytes) {
			byte[] bs = MD5(bytes);
			StringBuilder builder = new StringBuilder();
			
			// to hex decimal
			foreach (byte b in bs) {
				builder.Append(b.ToString("x2"));
			}

			return builder.ToString();
		}

		public static string MD5ToString(string str) {
			byte[] bytes = MD5(str);
			StringBuilder builder = new StringBuilder();
			
			// to hex decimal
			foreach (byte b in bytes) {
				builder.Append(b.ToString("x2"));
			}

			return builder.ToString();
		}

		public static string SHA1Key(string strToEncrypt) {
			UTF8Encoding utf8encoding = new UTF8Encoding();
			byte[] bytes = utf8encoding.GetBytes(strToEncrypt);

			SHA1 sha = new SHA1CryptoServiceProvider();
			byte[] hashBytes = sha.ComputeHash(bytes);

			StringBuilder builder = new StringBuilder();
			for (int i = 0; i < hashBytes.Length; i++) {
				builder.Append(Convert.ToString(hashBytes[i], 16).PadLeft(2, '0'));
			}

			return builder.ToString().PadLeft(32, '0');
		}
	}
}