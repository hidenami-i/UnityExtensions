using System.Text;

namespace UnityExtensions
{
	public static class ExStringBuilder
 	{
 		private static readonly StringBuilder builder = new StringBuilder(1024);
 		
 		public static string GetGCSafeString(params string[] appends) {
 			builder.Length = 0;
 			int length = appends.Length;
 			for (int i = 0; i < length; i++) {
 				builder.Append(appends[i]);
 			}
 
 			return builder.ToString();
 		}
 	}
 }