using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using UnityEngine;
using UnityEngine.Profiling;

namespace UnityExtensions
{
	/// <summary>
	/// Debug Extensions
	/// </summary>
	public static class ExDebug
	{
		private const string DefaultHexColor = "327AB7";
		private const string WarningHexColor = "F1C476";
		private const string ErrorHexColor = "E66762";
		private static int step;

		#if UNITY_EDITOR || !DEVELOPMENT_BUILD
		[Conditional("UNITY_EDITOR")]
		#endif
		public static void LogStep(string message = null, Color? color = null) {
			string hex = !color.HasValue ? DefaultHexColor : ColorUtility.ToHtmlStringRGB(color.Value);
			UnityEngine.Debug.LogFormat("<color=#{0}>{1}{2}</color>", hex, string.IsNullOrEmpty(message) ? string.Empty : message + " : ", step++);
		}

		/// <summary>
		/// Shows a log.
		/// </summary>
		/// <param name="value"></param>
		/// <param name="color"></param>
		/// <param name="message"></param>
		#if UNITY_EDITOR || !DEVELOPMENT_BUILD
		[Conditional("UNITY_EDITOR")]
		#endif
		public static void Log<T>(T value, string message = null, Color? color = null) {
			string hex = !color.HasValue ? DefaultHexColor : ColorUtility.ToHtmlStringRGB(color.Value);
			UnityEngine.Debug.LogFormat("<color=#{0}>{1}{2}</color>", hex, string.IsNullOrEmpty(message) ? string.Empty : message + " : ", value);
		}

		/// <summary>
		/// Shows a warning log.
		/// </summary>
		/// <param name="value"></param>
		/// <param name="message"></param>
		#if UNITY_EDITOR || !DEVELOPMENT_BUILD
		[Conditional("UNITY_EDITOR")]
		#endif
		public static void LogWarning<T>(T value, string message = null) {
			var str = string.IsNullOrEmpty(message) ? "" : $"{message} : ";
			UnityEngine.Debug.LogWarningFormat("<color=#{0}>{1}{2}</color>", WarningHexColor, str, value.ToString());
		}

		/// <summary>
		/// Shows a error log.
		/// </summary>
		/// <param name="value"></param>
		/// <param name="message"></param>
		#if UNITY_EDITOR || DEVELOPMENT_BUILD
		[Conditional("UNITY_EDITOR")]
		#endif
		public static void LogError<T>(T value, string message = null) {
			var str = string.IsNullOrEmpty(message) ? "" : $"{message} : ";
			UnityEngine.Debug.LogErrorFormat("<color=#{0}>{1}{2}</color>", ErrorHexColor, str, value.ToString());
		}

		/// <summary>
		/// Shows a array log.
		/// </summary>
		/// <param name="array"></param>
		/// <param name="title"></param>
		#if UNITY_EDITOR || !DEVELOPMENT_BUILD
		[Conditional("UNITY_EDITOR")]
		#endif
		public static void LogArray<T>(T[] array, string title = "") {
			var builder = new StringBuilder($"Array <{typeof(T).Name}> [{array.Length}]");
			if (!string.IsNullOrEmpty(title)) {
				builder.AppendLine($"<color=#{DefaultHexColor}>[{title}]</color>");
			}
			else {
				builder.AppendLine();
			}

			for (int i = 0; i < array.Length; ++i) {
				builder.AppendLine($"[{i}] {array[i]}");
			}

			UnityEngine.Debug.Log(builder.ToString());
		}

		/// <summary>
		/// Shows a list log.
		/// </summary>
		/// <param name="list"></param>
		/// <param name="title"></param>
		#if UNITY_EDITOR || !DEVELOPMENT_BUILD
		[Conditional("UNITY_EDITOR")]
		#endif
		public static void LogList<T>(IList<T> list, string title = "") {
			var builder = new StringBuilder($"List <{typeof(T).Name}> [{list.Count}]");
			if (!string.IsNullOrEmpty(title)) {
				builder.AppendLine($"<color=#{DefaultHexColor}>[{title}]</color>");
			}
			else {
				builder.AppendLine();
			}

			for (int i = 0; i < list.Count; ++i) {
				builder.AppendLine($"[{i}] {list[i]}");
			}

			UnityEngine.Debug.Log(builder.ToString());
		}

		/// <summary>
		/// Shows a dictionary log.
		/// </summary>
		/// <param name="values"></param>
		/// <param name="title"></param>
		#if UNITY_EDITOR || !DEVELOPMENT_BUILD
		[Conditional("UNITY_EDITOR")]
		#endif
		public static void LogDictionary<T, K>(IDictionary<T, K> values, string title = "") {
			var builder = new StringBuilder($"Dictionary <{typeof(T).Name}, {typeof(K).Name}> [{values.Count}]");
			if (!string.IsNullOrEmpty(title)) {
				builder.AppendLine($"<color=#{DefaultHexColor}>[{title}]</color>");
			}
			else {
				builder.AppendLine();
			}

			foreach (var pair in values) {
				builder.AppendLine($"key=[{pair.Key}], value={pair.Value}");
			}

			UnityEngine.Debug.Log(builder.ToString());
		}

		/// <summary>
		/// Shows a error log.
		/// </summary>
		/// <param name="e"></param>
		/// <param name="message"></param>
		#if UNITY_EDITOR || !DEVELOPMENT_BUILD
		[Conditional("UNITY_EDITOR")]
		#endif
		public static void LogException(System.Exception e, string message = "") {
			var builder = new StringBuilder();
			if (string.IsNullOrEmpty(message)) {
				builder.AppendLine($"<color=#{DefaultHexColor}>[Message]</color>");
				builder.AppendLine(message);
				builder.AppendLine();
			}

			builder.AppendLine($"<color=#{DefaultHexColor}>[Source]</color>");
			builder.AppendLine(e.Source);
			builder.AppendLine();
			builder.AppendLine($"<color=#{DefaultHexColor}>[StackTrace]</color>");
			builder.AppendLine(e.StackTrace);
			builder.AppendLine();
			builder.AppendLine($"<color=#{DefaultHexColor}>[ToString]</color>");
			builder.AppendLine(e.ToString());
			UnityEngine.Debug.LogError(builder.ToString());
		}

		/// <summary>
		/// Shows a Vector2 Log
		/// </summary>
		/// <param name="_vector">Vector.</param>
		public static void LogVector2(Vector2 vector) {
			Log("(" + vector.x.ToString("0.0#######") + ", " + vector.y.ToString("0.0#######") + ")");
		}

		/// <summary>
		/// Shows a Vector3 Log
		/// </summary>
		/// <param name="_vector">Vector.</param>
		public static void LogVector3(Vector3 vector) {
			Log("(" + vector.x.ToString("0.0#######") + ", " + vector.y.ToString("0.0#######") + ", " + vector.z.ToString("0.0#######") + ")");
		}

		/// <summary>
		/// Draw a lay.
		/// </summary>
		/// <param name="startPosition"></param>
		/// <param name="direction"></param>
		/// <param name="distance"></param>
		/// <param name="color"></param>
		#if UNITY_EDITOR || !DEVELOPMENT_BUILD
		[Conditional("UNITY_EDITOR")]
		#endif
		public static void DrawRay(Vector3 startPosition, Vector3 direction, float distance, Color color) {
			UnityEngine.Debug.DrawRay(startPosition, direction.normalized * distance, color, 0, false);
		}

		/// <summary>
		/// Draw a ray in front of the target.
		/// </summary>
		/// <param name="targetTransform"></param>
		/// <param name="distance"></param>
		/// <param name="color"></param>
		#if UNITY_EDITOR || !DEVELOPMENT_BUILD
		[Conditional("UNITY_EDITOR")]
		#endif
		public static void DrawForwardRay(Transform targetTransform, Color color, float distance = 10) {
			DrawRay(targetTransform.position, targetTransform.forward, distance, color);
		}

		/// <summary>
		/// Draw a ray to the right of the target.
		/// </summary>
		/// <param name="targetTransform"></param>
		/// <param name="distance"></param>
		/// <param name="color"></param>
		#if UNITY_EDITOR || !DEVELOPMENT_BUILD
		[Conditional("UNITY_EDITOR")]
		#endif
		public static void DrawRightRay(Transform targetTransform, Color color, float distance = 10) {
			DrawRay(targetTransform.position, targetTransform.right, distance, color);
		}

		/// <summary>
		/// Draw a ray to the backward of the target.
		/// </summary>
		/// <param name="targetTransform"></param>
		/// <param name="distance"></param>
		/// <param name="color"></param>
		#if UNITY_EDITOR || !DEVELOPMENT_BUILD
		[Conditional("UNITY_EDITOR")]
		#endif
		public static void DrawBackwardRay(Transform targetTransform, Color color, float distance = 10) {
			DrawRay(targetTransform.position, -targetTransform.forward, distance, color);
		}

		/// <summary>
		/// Draw a ray to the left of the target.
		/// </summary>
		/// <param name="targetTransform"></param>
		/// <param name="distance"></param>
		/// <param name="color"></param>
		#if UNITY_EDITOR || !DEVELOPMENT_BUILD
		[Conditional("UNITY_EDITOR")]
		#endif
		public static void DrawLeftRay(Transform targetTransform, Color color, float distance = 10) {
			DrawRay(targetTransform.position, -targetTransform.right, distance, color);
		}

		#if UNITY_EDITOR || !DEVELOPMENT_BUILD
		[Conditional("UNITY_EDITOR")]
		#endif
		public static void LogMonoUsedSize() {
			Log("mono used size", ExString.ToHumanReadableSize(Profiler.GetMonoUsedSizeLong()));
		}

		/// <summary>
		/// Shows a log about the system info.
		/// </summary>
		#if UNITY_EDITOR || !DEVELOPMENT_BUILD
		[Conditional("UNITY_EDITOR")]
		#endif
		public static void LogSystemInfo() {
			UnityEngine.Debug.Log(GetSystemInfoToString);
		}

		/// <summary>
		/// System info list to String.
		/// </summary>
		public static string GetSystemInfoToString {
			get {
				StringBuilder builder = new StringBuilder();
				builder.AppendLine("[deviceModel]" + SystemInfo.deviceModel);
				builder.AppendLine("[deviceName]" + SystemInfo.deviceName);
				builder.AppendLine("[deviceType]" + SystemInfo.deviceType);
				builder.AppendLine("[graphicsDeviceId]" + SystemInfo.graphicsDeviceID);
				builder.AppendLine("[graphicsDeviceName]" + SystemInfo.deviceModel);
				builder.AppendLine("[graphicsDeviceVendor]" + SystemInfo.graphicsDeviceVendor);
				builder.AppendLine("[graphicsDeviceVendorId]" + SystemInfo.graphicsDeviceVendorID);
				builder.AppendLine("[graphicsDeviceVersion]" + SystemInfo.graphicsDeviceVersion);
				builder.AppendLine("[graphicsMemorySize]" + SystemInfo.graphicsMemorySize);
				builder.AppendLine("[graphicsShaderLevel]" + SystemInfo.graphicsShaderLevel);
				builder.AppendLine("[operatingSystem]" + SystemInfo.operatingSystem);
				builder.AppendLine("[processorCount]" + SystemInfo.processorCount);
				builder.AppendLine("[processorType]" + SystemInfo.processorType);
				builder.AppendLine("[supportedRenderTargetCount]" + SystemInfo.supportedRenderTargetCount);
				builder.AppendLine("[supports3dTextures]" + SystemInfo.supports3DTextures);
				builder.AppendLine("[supportsAccelerometer]" + SystemInfo.supportsAccelerometer);
				builder.AppendLine("[supportsComputeShaders]" + SystemInfo.supportsComputeShaders);
				builder.AppendLine("[supportsGyroscope]" + SystemInfo.supportsGyroscope);
				builder.AppendLine("[supportsInstancing]" + SystemInfo.supportsInstancing);
				builder.AppendLine("[supportsLocationService]" + SystemInfo.supportsLocationService);
				builder.AppendLine("[supportsShadows]" + SystemInfo.supportsShadows);
				builder.AppendLine("[supportsVibration]" + SystemInfo.supportsVibration);
				builder.AppendLine("[systemMemorySize]" + SystemInfo.systemMemorySize);
				builder.AppendLine($"[Screen] {Screen.width}×{Screen.height}");
				builder.AppendLine($"[ScreenCurrentResolution] {Screen.currentResolution.width}×{Screen.currentResolution.height}");

				builder.AppendLine("[SupportsTextureFormat]");
				foreach (TextureFormat textureFormat in ExEnumeraion.ToList<TextureFormat>()) {
					builder.AppendLine($"[{ExEnumeraion.GetName(textureFormat)}]" + SystemInfo.SupportsTextureFormat(textureFormat));
				}

				return builder.ToString();
			}
		}
	}
}