using UnityEngine;

namespace UnityExtensions
{
	public static class ExRuntimePlatform
	{
		public static bool IsDesktopPlatform(this RuntimePlatform platform)
		{
			switch (platform)
			{
				case RuntimePlatform.LinuxEditor:
				case RuntimePlatform.LinuxPlayer:
				case RuntimePlatform.OSXEditor:
				case RuntimePlatform.OSXPlayer:
				case RuntimePlatform.WindowsEditor:
				case RuntimePlatform.WindowsPlayer:
				case RuntimePlatform.WebGLPlayer:
					return true;
			}

			return false;
		}

		public static bool IsConsolePlatform(this RuntimePlatform platform)
		{
			switch (platform)
			{
				case RuntimePlatform.Switch:
				case RuntimePlatform.PS4:
				case RuntimePlatform.XboxOne:
					return true;
			}

			return false;
		}
		
		public static bool IsMobilePlatform(this RuntimePlatform platform)
		{
			switch (platform)
			{
				case RuntimePlatform.IPhonePlayer:
				case RuntimePlatform.Android:
					return true;
			}

			return false;
		}
		
		public static bool IsOSXEditor(this RuntimePlatform runtimePlatform) {
			return runtimePlatform == RuntimePlatform.OSXEditor;
		}

		public static bool IsOSXPlayer(this RuntimePlatform runtimePlatform) {
			return runtimePlatform == RuntimePlatform.OSXPlayer;
		}

		public static bool IsWindowsPlayer(this RuntimePlatform runtimePlatform) {
			return runtimePlatform == RuntimePlatform.WindowsPlayer;
		}

		public static bool IsWindowsEditor(this RuntimePlatform runtimePlatform) {
			return runtimePlatform == RuntimePlatform.WindowsEditor;
		}

		public static bool IsIPhonePlayer(this RuntimePlatform runtimePlatform) {
			return runtimePlatform == RuntimePlatform.IPhonePlayer;
		}

		public static bool IsAndroid(this RuntimePlatform runtimePlatform) {
			return runtimePlatform == RuntimePlatform.Android;
		}

		public static bool IsLinuxPlayer(this RuntimePlatform runtimePlatform) {
			return runtimePlatform == RuntimePlatform.LinuxPlayer;
		}

		public static bool IsLinuxEditor(this RuntimePlatform runtimePlatform) {
			return runtimePlatform == RuntimePlatform.LinuxEditor;
		}

		public static bool IsWebGLPlayer(this RuntimePlatform runtimePlatform) {
			return runtimePlatform == RuntimePlatform.WebGLPlayer;
		}

		public static bool IsWSAPlayerX86(this RuntimePlatform runtimePlatform) {
			return runtimePlatform == RuntimePlatform.WSAPlayerX86;
		}

		public static bool IsWSAPlayerX64(this RuntimePlatform runtimePlatform) {
			return runtimePlatform == RuntimePlatform.WSAPlayerX64;
		}

		public static bool IsWSAPlayerARM(this RuntimePlatform runtimePlatform) {
			return runtimePlatform == RuntimePlatform.WSAPlayerARM;
		}

		public static bool IsPS4(this RuntimePlatform runtimePlatform) {
			return runtimePlatform == RuntimePlatform.PS4;
		}

		public static bool IsXboxOne(this RuntimePlatform runtimePlatform) {
			return runtimePlatform == RuntimePlatform.XboxOne;
		}

		public static bool IstvOS(this RuntimePlatform runtimePlatform) {
			return runtimePlatform == RuntimePlatform.tvOS;
		}

		public static bool IsSwitch(this RuntimePlatform runtimePlatform) {
			return runtimePlatform == RuntimePlatform.Switch;
		}
	}
}