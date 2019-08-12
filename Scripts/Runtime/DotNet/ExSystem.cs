using UnityEngine;
using System.Text;

namespace UnityExtensions
{
	public static class ExSystem
 	{
 		/// <summary>
 		/// 端末名を取得します。
 		/// 端末名が取得できない場合は端末のモデルを取得します。
 		/// </summary>
 		public static string GetDeviceNameOrDeviceModel {
 			get {
 				string deviceName = SystemInfo.deviceName;
 				if (!string.IsNullOrEmpty(deviceName)) deviceName = SystemInfo.deviceModel;
 				return deviceName;
 			}
 		}
 	}
 }