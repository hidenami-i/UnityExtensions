using UnityEditor;
using UnityEngine;

namespace UnityExtensions.Editor
{
	public static class ExEditorGUI
	{
		public static void SetDaultGUIColor() {
			GUI.color = Color.white;
		}
		
		/// <summary>
		/// </summary>
		/// <param name="title"></param>
		/// <param name="isShow"></param>
		/// <returns></returns>
		/// <example>
		/// bool isShow = false;
		///
		/// public override void OnInspectorGUI()
		/// {
		///	isShow = ExGUI.FoldOut("Hoge", isShow);
		///	if (isShow) {
		///		EditorGUILayout.Toggle("", false);
		///		EditorGUILayout.IntField("", 12345);
		///		EditorGUILayout.TextField("", "");
		///	}
		/// }
		/// </example>
		public static bool Foldout(string title, bool isShow) {
			var style = new GUIStyle("ShurikenModuleTitle")
			{
				font = new GUIStyle(EditorStyles.label).font,
				border = new RectOffset(15, 7, 4, 4),
				fixedHeight = 22,
				contentOffset = new Vector2(20f, -2f)
			};

			Rect rect = GUILayoutUtility.GetRect(16f, 22f, style);
			GUI.Box(rect, title, style);

			Event e = Event.current;

			Rect toggleRect = new Rect(rect.x + 4f, rect.y + 2f, 13f, 13f);
			if (e.type == EventType.Repaint) {
				EditorStyles.foldout.Draw(toggleRect, false, false, isShow, false);
			}

			if (e.type == EventType.MouseDown && rect.Contains(e.mousePosition)) {
				isShow = !isShow;
				e.Use();
			}

			return isShow;
		}

		/// <summary>
		/// Separate horizontal line.
		/// </summary>
		/// <param name="width"> this.position.width </param>
		public static void SeperatorHorizontalLine(float width) {
			GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(1));
		}

		/// <summary>
		/// Separate vertical line.
		/// </summary>
		/// <param name="width"></param>
		public static void SeperatorVerticalLine(float height) {
			GUILayout.Box("", GUILayout.Width(1), GUILayout.ExpandHeight(true));
		}

		/// <summary>
		/// EditorGUILayout.Space();
		/// </summary>
		/// <param name="width"></param>
		/// <param name="height"></param>
		public static void Space(float width = 6f, float height = 6f) {
			GUILayoutUtility.GetRect(6f, 6f);
		}

		/// <summary>
		/// Get Component Texture.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static Texture GetComponentTexture<T>() where T : Component {
			return EditorGUIUtility.ObjectContent(null, typeof(T)).image;
		}

		/// <summary>
		/// Ping Object by assetpath.
		/// </summary>
		/// <param name="assetPath"></param>
		/// <typeparam name="T"></typeparam>
		public static void PingObject<T>(string assetPath) {
			var obj = AssetDatabase.LoadAssetAtPath(assetPath, typeof(T));
			EditorGUIUtility.PingObject(obj);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public static Rect BeginHorizontalAsToolBar() {
			return EditorGUILayout.BeginHorizontal(EditorStyles.toolbar);
		}

		/// <summary>
		/// 
		/// </summary>
		public static void BeginDisabledGroupUnityEditorBusy() {
			EditorGUI.BeginDisabledGroup(ExUnityEditor.IsPlayingUnityEditor());
		}

		#region label field

		public static void LabelField(string label, params GUILayoutOption[] options) {
			EditorGUILayout.LabelField(label, options);
		}

		public static void LabelField(string label, GUIStyle style, params GUILayoutOption[] options) {
			EditorGUILayout.LabelField(label, style, options);
		}

		public static void LabelFieldAsBold(string label, params GUILayoutOption[] options) {
			EditorGUILayout.LabelField(label, EditorStyles.boldLabel, options);
		}

		public static void LabelFieldAsHelpBox(string label, params GUILayoutOption[] options) {
			EditorGUILayout.LabelField(label, EditorStyles.helpBox, options);
		}

		#endregion

		#region Button

		public static bool Button(string text, params GUILayoutOption[] options) {
			return GUILayout.Button(text, options);
		} 

		public static bool ButtonAsLink(string label, params GUILayoutOption[] options) {
			return ButtonAsLink(new GUIContent(label), options);
		}

		public static bool ButtonAsLink(GUIContent label, params GUILayoutOption[] options) {
			Rect position = GUILayoutUtility.GetRect(label, EditorStyles.label, options);
			Handles.color = EditorStyles.label.normal.textColor;
			Handles.DrawLine(new Vector3(position.xMin, position.yMax), new Vector3(position.xMax, position.yMax));
			Handles.color = Color.white;
			EditorGUIUtility.AddCursorRect(position, MouseCursor.Link);
			return GUI.Button(position, label, EditorStyles.label);
		}

		public static bool ButtonAsToolBar(string text, params GUILayoutOption[] options) {
			return GUILayout.Button(text, EditorStyles.toolbarButton, options);
		}
		
		public static bool ButtonAsToolBar(GUIContent content, params GUILayoutOption[] options) {
			return GUILayout.Button(content, EditorStyles.toolbarButton, options);
		}

		#endregion

		#region builtin resource

		/// <summary>
		/// Load error small icon.
		/// </summary>
		/// <returns></returns>
		public static Texture LoadErrorSmallIcon() {
			return LoadBuiltInTexture("d_console.erroricon.sml");
		}

		/// <summary>
		/// Load error icon.
		/// </summary>
		/// <returns></returns>
		public static Texture LoadErrorIcon() {
			return LoadBuiltInTexture("d_console.erroricon");
		}

		/// <summary>
		/// Load error info small icon.
		/// </summary>
		/// <returns></returns>
		public static Texture LoadInfoSmallIcon() {
			return LoadBuiltInTexture("d_console.infoicon.sml");
		}

		/// <summary>
		/// Load error info icon.
		/// </summary>
		/// <returns></returns>
		public static Texture LoadInfoIcon() {
			return LoadBuiltInTexture("d_console.infoicon.sml");
		}

		/// <summary>
		/// Load error warn small icon.
		/// </summary>
		/// <returns></returns>
		public static Texture LoadWarnSmallIcon() {
			return LoadBuiltInTexture("d_console.warnicon.sml");
		}

		/// <summary>
		/// Load error warn icon.
		/// </summary>
		/// <returns></returns>
		public static Texture LoadWarnIcon() {
			return LoadBuiltInTexture("d_console.warnicon");
		}

		public static Texture LoadBuiltInTexture(string fileName) {
			return EditorGUIUtility.Load($"icons/{fileName}.png") as Texture2D;
		}

		#endregion

		#region HelpBox

		/// <summary>
		/// Show error helpbox.
		/// </summary>
		/// <param name="message"></param>
		public static void HelpErrorBox(string message) {
			EditorGUILayout.HelpBox(message, MessageType.Error);
		}

		/// <summary>
		/// Show info helpbox.
		/// </summary>
		/// <param name="message"></param>
		public static void HelpInfoBox(string message) {
			EditorGUILayout.HelpBox(message, MessageType.Info);
		}

		/// <summary>
		/// Show warning helpbox.
		/// </summary>
		/// <param name="message"></param>
		public static void HelpWarningBox(string message) {
			EditorGUILayout.HelpBox(message, MessageType.Warning);
		}

		/// <summary>
		/// Show none helpbox.
		/// </summary>
		/// <param name="message"></param>
		public static void HelpNoneBox(string message) {
			EditorGUILayout.HelpBox(message, MessageType.None);
		}

		#endregion
	}
}