using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UnityExtensions
{
	public static class ExGameObject
	{
		/// <summary> GameObjectがnullかどうかを返します </summary>
		public static bool IsNull(this GameObject self) {
			return self == null;
		}

		/// <summary> GameObjectがnullでないかどうかを返します </summary>
		public static bool IsNotNull(this GameObject self) {
			return !self.IsNull();
		}

		#region Find
		
		/// <summary> Scene内に存在するGameObjectを走査します </summary>
		public static GameObject SafeFind(string name) {
			GameObject go = GameObject.Find(name);
			if (!go.IsNull()) return go;
			ExDebug.LogWarning("not exist gameobject : " + name);
			return null;
		}

		/// <summary> Scene内に存在するGameObjectをタグを条件に走査します </summary>
		public static GameObject SafeFindGameObjectWithTag(string tag) {
			GameObject go = GameObject.FindGameObjectWithTag(tag);
			if (!go.IsNull()) return go;
			ExDebug.LogWarning("not exist gameobject : " + tag);
			return null;

		}

		/// <summary>
		/// 親GameObjectを走査し返します
		/// </summary>
		public static GameObject SafeFindParent(this GameObject self) {

			if (self == null) {
				return null;
			}

			var transform = self.transform;
			if (transform.parent == null) {
				return null;
			}

			return transform.parent.gameObject;
		}

		/// <summary>
		/// ルートゲームオブジェクトを走査し返します
		/// </summary>
		/// <returns>The root game object.</returns>
		/// <param name="self">Self.</param>
		public static GameObject SafeFindRootGameObject(this GameObject self) {
			if (self == null) {
				return null;
			}

			if (self.transform.parent == null) {
				return self;
			}

			var rootTransform = self.transform.parent;
			while (rootTransform.parent != null) {
				rootTransform = rootTransform.parent;
			}

			return rootTransform.gameObject;
		}

		/// <summary>
		/// 特定の名前が付いた子の最初のGameObjectを取得します。
		/// </summary>
		/// <param name="self"></param>
		/// <param name="name"></param>
		/// <returns></returns>
		public static GameObject Child(this GameObject self, string name) {
			if (self == null) {
				return null;
			}

			Transform child = self.transform.Find(name);
			if (child == null) {
				return null;
			}

			return child.gameObject;
		}

		/// <summary>
		/// 対象のGameObjectの子要素を返します。
		/// </summary>
		/// <param name="parent"></param>
		/// <returns></returns>
		public static List<GameObject> Children(this GameObject parent) {
			List<GameObject> list = new List<GameObject>();
			if (!parent.HasChild()) {
				return list;
			}

			var transform = parent.transform;
			foreach (Transform child in transform.Cast<Transform>()) {
				list.Add(child.gameObject);
			}

			return list;
		}
		
		/// <summary>
		/// Gets all descendatants
		/// </summary>
		/// <param name="parent"></param>
		/// <returns></returns>
		public static List<GameObject> Descendants(this GameObject parent) {
			
			List<GameObject> list = new List<GameObject>();
			if (!parent.HasChild()) {
				return list;
			}

			var transforms = parent.GetComponentsInChildren<Transform>();
			foreach (Transform transform in transforms) {
				list.Add(transform.gameObject);
			}

			return list;
		}

		/// <summary>
		/// 指定されたインターフェイスを実装したコンポーネントを持つオブジェクトを検索します
		/// </summary>
		public static T FindObjectOfInterface<T>() where T : class {
			var ns = UnityEngine.Object.FindObjectsOfType<Component>();
			for (var index = 0; index < ns.Length; index++) {
				var n = ns[index];
				var component = n as T;
				if (component != null) {
					return component;
				}
			}

			return null;
		}

		/// <summary>
		/// 指定されたインターフェイスを実装したコンポーネントを返します。
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static Component FindObjectOfInterfaceAsComponent<T>() where T : class {
			var ns = UnityEngine.Object.FindObjectsOfType<Component>();
			for (var index = 0; index < ns.Length; index++) {
				var n = ns[index];
				if (n is T) {
					return n;
				}
			}

			return null;
		}

		public static List<T> FindObjectsOfInterface<T>() where T : class {
			List<T> list = new List<T>();

			var ns = UnityEngine.Object.FindObjectsOfType<Component>();
			for (var index = 0; index < ns.Length; index++) {
				var n = ns[index];
				if (n is T component) {
					list.Add(component);
				}
			}

			return list;
		}

		public static List<Component> FindObjectsOfInterfaceAsComponent<T>() where T : class {
			List<Component> list = new List<Component>();

			var ns = UnityEngine.Object.FindObjectsOfType<Component>();
			for (var index = 0; index < ns.Length; index++) {
				var n = ns[index];
				if (n is T component) {
					list.Add(n);
				}
			}

			return list;
		}

		#endregion

		#region Destroy

		/// <summary> GameObjectを削除します </summary>
		public static void SafeDestroy(this GameObject self, bool isUnloadUnusedAssets = true) {
			if (self.IsNull()) return;
			UnityEngine.Object.Destroy(self);
			if (isUnloadUnusedAssets) {
				Resources.UnloadUnusedAssets();
			}
		}

		/// <summary>
		/// ルートゲームオブジェクトを削除します
		/// </summary>
		/// <param name="self">Self.</param>
		/// <param name = "isUnloadUnusedAssets"></param>
		public static void SafeDestroyRootGameObject(this GameObject self, bool isUnloadUnusedAssets = true) {
			if (self.IsNull()) return;
			self.SafeFindRootGameObject().SafeDestroy();
			if (isUnloadUnusedAssets) {
				Resources.UnloadUnusedAssets();
			}
		}

		#endregion

		/// <summary>
		/// GameObjectをActiveにします
		/// </summary>
		public static void SafeSetActive(this GameObject self, bool active = true) {
			if (self.IsNull()) {
				ExDebug.LogWarning("Not Found Gameobject");
				return;
			}

			self.SetActive(active);
		}

		#region Add

		/// <summary>
		/// GameObjectにComponentを設定します
		/// </summary>
		/// <typeparam name="T">Component</typeparam>
		public static T SafeAddComponent<T>(this GameObject self) where T : Component {
			if (self.IsNull()) {
				ExDebug.LogWarning("Not Found Gameobject");
				return null;
			}

			return self.AddComponent<T>();
		}

		/// <summary>
		/// GameObjectに複数のComponentを設定します
		/// </summary>
		/// <param name="self"></param>
		/// <param name="components"></param>
		public static void SafeAddComponents(this GameObject self, params Type[] components) {
			if (self.IsNull()) {
				ExDebug.LogWarning("Not Found Gameobject");
				return;
			}

			for (int i = 0, componentsLength = components.Length; i < componentsLength; i++) {
				var component = components[i];
				if (component == null) continue;
				self.AddComponent(component);
			}
		}

		/// <summary>
		/// GameObjectに対してコンポーネントを設定する。
		/// ラムダでコンポーネントを設定できるので、スコープが変数で汚れづらい。
		/// なお、GameObject上にコンポーネントが存在しない場合、コンポーネントを追加する。
		/// </summary>
		/// <returns>コンポーネント追加先のGameObject。</returns>
		/// <param name="gameObject">コンポーネントを追加するGameObject。</param>
		/// <param name="action">コンポーネント追加時に実施する処理。</param>
		public static GameObject SetComponent<T>(this GameObject gameObject, Action<T> action = null)
			where T : Component {
			T component = gameObject.GetComponent<T>();
			if (component == null) {
				component = gameObject.AddComponent<T>();
			}

			action?.Invoke(component);

			return gameObject;
		}

		#endregion

		#region Get
		
		public static bool TryGetComponent<T>(this GameObject self, out T component) where T : Component {
			component = self.GetComponent<T>();
			return component != null;
		}

		
		/// <summary>
		/// GetComponentの拡張関数
		/// </summary>
		public static T SafeGetOrAddComponent<T>(this GameObject self) where T : Component {
			return self.GetComponent<T>() ?? self.AddComponent<T>();
		}

		/// <summary>
		/// GetComponentの拡張関数
		/// </summary>
		public static T SafeGetOrAddComponent<T>(this Component self) where T : Component {
			return self.GetComponent<T>() ?? self.gameObject.AddComponent<T>();
		}

		/// <summary>
		/// GetComponentの拡張関数
		/// </summary>
		public static T SafeGetParentOrAddComponent<T>(this GameObject self) where T : Component {
			return self.SafeFindParent().GetComponent<T>() ?? self.gameObject.SafeFindParent().AddComponent<T>();
		}

		#endregion

		/// <summary>
		/// 該当コンポーネントを持っているか判定します。
		/// </summary>
		/// <returns><c>true</c>, if component was hased, <c>false</c> otherwise.</returns>
		/// <param name="self">Self.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public static bool HasComponent<T>(this GameObject self) where T : Component {
			return self.GetComponent<T>() != null;
		}

		/// <summary>
		/// 子オブジェクトが存在するか判定します。
		/// </summary>
		/// <param name="gameObject"></param>
		/// <returns></returns>
		public static bool HasChild(this GameObject gameObject) {
			return 0 < gameObject.transform.childCount;
		}

		/// <summary>
		/// 位置を(0, 0, 0)にリセットします
		/// </summary>
		public static void ResetPosition(this GameObject self) {
			self.transform.position = Vector3.zero;
		}

		/// <summary>
		/// 位置を返します
		/// </summary>
		public static Vector3 GetPosition(this GameObject self) {
			return self.transform.position;
		}

		/// <summary>
		/// X 座標を返します
		/// </summary>
		public static float GetPositionX(this GameObject self) {
			return self.transform.position.x;
		}

		/// <summary>
		/// Y 座標を返します
		/// </summary>
		public static float GetPositionY(this GameObject self) {
			return self.transform.position.y;
		}

		/// <summary>
		/// Z 座標を返します
		/// </summary>
		public static float GetPositionZ(this GameObject self) {
			return self.transform.position.z;
		}

		/// <summary>
		/// X 座標を設定します
		/// </summary>
		public static void SetPositionX(this GameObject self, float x) {
			self.transform.position = new Vector3(x, self.transform.position.y, self.transform.position.z);
		}

		/// <summary>
		/// Y 座標を設定します
		/// </summary>
		public static void SetPositionY(this GameObject self, float y) {
			self.transform.position = new Vector3(self.transform.position.x, y, self.transform.position.z);
		}

		/// <summary>
		/// Z 座標を設定します
		/// </summary>
		public static void SetPositionZ(this GameObject self, float z) {
			self.transform.position = new Vector3(self.transform.position.x, self.transform.position.y, z);
		}

		/// <summary>
		/// Vector3 型で位置を設定します
		/// </summary>
		public static void SetPosition(this GameObject self, Vector3 v) {
			self.transform.position = v;
		}

		/// <summary>
		/// Vector2 型で位置を設定します
		/// </summary>
		public static void SetPosition(this GameObject self, Vector2 v) {
			self.transform.position = new Vector3(v.x, v.y, self.transform.position.z);
		}

		/// <summary>
		/// 位置を設定します
		/// </summary>
		public static void SetPosition(this GameObject self, float x, float y) {
			self.transform.position = new Vector3(x, y, self.transform.position.z);
		}

		/// <summary>
		/// 位置を設定します
		/// </summary>
		public static void SetPosition(this GameObject self, float x, float y, float z) {
			self.transform.position = new Vector3(x, y, z);
		}

		/// <summary>
		/// X 座標に加算します
		/// </summary>
		public static void AddPositionX(this GameObject self, float x) {
			self.transform.position = new Vector3(self.transform.position.x + x, self.transform.position.y,
												self.transform.position.z);
		}

		/// <summary>
		/// Y 座標に加算します
		/// </summary>
		public static void AddPositionY(this GameObject self, float y) {
			self.transform.position = new Vector3(self.transform.position.x, self.transform.position.y + y,
												self.transform.position.z);
		}

		/// <summary>
		/// Z 座標に加算します
		/// </summary>
		public static void AddPositionZ(this GameObject self, float z) {
			self.transform.position = new Vector3(self.transform.position.x, self.transform.position.y,
												self.transform.position.z + z);
		}

		/// <summary>
		/// Vector3 型で位置を加算します
		/// </summary>
		public static void AddPosition(this GameObject self, Vector3 v) {
			self.transform.position += v;
		}

		/// <summary>
		/// Vector2 型で位置を加算します
		/// </summary>
		public static void AddPosition(this GameObject self, Vector2 v) {
			self.transform.position = new Vector3(self.transform.position.x + v.x, self.transform.position.y + v.y,
												self.transform.position.z);
		}

		/// <summary>
		/// 位置を加算します
		/// </summary>
		public static void AddPosition(this GameObject self, float x, float y) {
			self.transform.position = new Vector3(self.transform.position.x + x, self.transform.position.y + y,
												self.transform.position.z);
		}

		/// <summary>
		/// 位置を加算します
		/// </summary>
		public static void AddPosition(this GameObject self, float x, float y, float z) {
			self.transform.position = new Vector3(self.transform.position.x + x, self.transform.position.y + y,
												self.transform.position.z + z);
		}

		/// <summary>
		/// ローカル座標を(0, 0, 0)にリセットします
		/// </summary>
		public static void ResetLocalPosition(this GameObject self) {
			self.transform.localPosition = Vector3.zero;
		}

		/// <summary>
		/// ローカル座標を返します
		/// </summary>
		public static Vector3 GetLocalPosition(this GameObject self) {
			return self.transform.localPosition;
		}

		/// <summary>
		/// ローカル座標系の X 座標を返します
		/// </summary>
		public static float GetLocalPositionX(this GameObject self) {
			return self.transform.localPosition.x;
		}

		/// <summary>
		/// ローカル座標系の Y 座標を返します
		/// </summary>
		public static float GetLocalPositionY(this GameObject self) {
			return self.transform.localPosition.y;
		}

		/// <summary>
		/// ローカル座標系の Z 座標を返します
		/// </summary>
		public static float GetLocalPositionZ(this GameObject self) {
			return self.transform.localPosition.z;
		}

		/// <summary>
		/// ローカル座標系のX座標を設定します
		/// </summary>
		public static void SetLocalPositionX(this GameObject self, float x) {
			self.transform.localPosition =
				new Vector3(x, self.transform.localPosition.y, self.transform.localPosition.z);
		}

		/// <summary>
		/// ローカル座標系のY座標を設定します
		/// </summary>
		public static void SetLocalPositionY(this GameObject self, float y) {
			self.transform.localPosition =
				new Vector3(self.transform.localPosition.x, y, self.transform.localPosition.z);
		}

		/// <summary>
		/// ローカル座標系のZ座標を設定します
		/// </summary>
		public static void SetLocalPositionZ(this GameObject self, float z) {
			self.transform.localPosition =
				new Vector3(self.transform.localPosition.x, self.transform.localPosition.y, z);
		}

		/// <summary>
		/// Vector3 型でローカル座標を設定します
		/// </summary>
		public static void SetLocalPosition(this GameObject self, Vector3 v) {
			self.transform.localPosition = v;
		}

		/// <summary>
		/// Vector2 型でローカル座標を設定します
		/// </summary>
		public static void SetLocalPosition(this GameObject self, Vector2 v) {
			self.transform.localPosition = new Vector3(v.x, v.y, self.transform.localPosition.z);
		}

		/// <summary>
		/// ローカル座標を設定します
		/// </summary>
		public static void SetLocalPosition(this GameObject self, float x, float y) {
			self.transform.localPosition = new Vector3(x, y, self.transform.localPosition.z);
		}

		/// <summary>
		/// ローカル座標を設定します
		/// </summary>
		public static void SetLocalPosition(this GameObject self, float x, float y, float z) {
			self.transform.localPosition = new Vector3(x, y, z);
		}

		/// <summary>
		/// ローカルのX座標に加算します
		/// </summary>
		public static void AddLocalPositionX(this GameObject self, float x) {
			self.transform.localPosition = new Vector3(self.transform.localPosition.x + x,
														self.transform.localPosition.y, self.transform.localPosition.z);
		}

		/// <summary>
		/// ローカルのY座標に加算します
		/// </summary>
		public static void AddLocalPositionY(this GameObject self, float y) {
			self.transform.localPosition = new Vector3(self.transform.localPosition.x,
														self.transform.localPosition.y + y,
														self.transform.localPosition.z);
		}

		/// <summary>
		/// ローカルのZ座標に加算します
		/// </summary>
		public static void AddLocalPositionZ(this GameObject self, float z) {
			self.transform.localPosition = new Vector3(self.transform.localPosition.x, self.transform.localPosition.y,
														self.transform.localPosition.z + z);
		}

		/// <summary>
		/// Vector3 型でローカル座標を加算します
		/// </summary>
		public static void AddLocalPosition(this GameObject self, Vector3 v) {
			self.transform.localPosition += v;
		}

		/// <summary>
		/// Vector2 型でローカル座標を加算します
		/// </summary>
		public static void AddLocalPosition(this GameObject self, Vector2 v) {
			self.transform.localPosition = new Vector3(self.transform.localPosition.x + v.x,
														self.transform.localPosition.y + v.y,
														self.transform.localPosition.z);
		}

		/// <summary>
		/// ローカル座標を加算します
		/// </summary>
		public static void AddLocalPosition(this GameObject self, float x, float y) {
			self.transform.localPosition = new Vector3(self.transform.localPosition.x + x,
														self.transform.localPosition.y + y,
														self.transform.localPosition.z);
		}

		/// <summary>
		/// ローカル座標を加算します
		/// </summary>
		public static void AddLocalPosition(this GameObject self, float x, float y, float z) {
			self.transform.localPosition = new Vector3(self.transform.localPosition.x + x,
														self.transform.localPosition.y + y,
														self.transform.localPosition.z + z);
		}

		/// <summary>
		/// ローカル座標系のスケーリング値を(1, 1, 1)にリセットします
		/// </summary>
		public static void ResetLocalScale(this GameObject self) {
			self.transform.localScale = Vector3.one;
		}

		/// <summary>
		/// ローカル座標系のスケーリング値を返します
		/// </summary>
		public static Vector3 GetLocalScale(this GameObject self) {
			return self.transform.localScale;
		}

		/// <summary>
		/// X 軸方向のローカル座標系のスケーリング値を返します
		/// </summary>
		public static float GetLocalScaleX(this GameObject self) {
			return self.transform.localScale.x;
		}

		/// <summary>
		/// Y 軸方向のローカル座標系のスケーリング値を返します
		/// </summary>
		public static float GetLocalScaleY(this GameObject self) {
			return self.transform.localScale.y;
		}

		/// <summary>
		/// Z 軸方向のローカル座標系のスケーリング値を返します
		/// </summary>
		public static float GetLocalScaleZ(this GameObject self) {
			return self.transform.localScale.z;
		}

		/// <summary>
		/// X 軸方向のローカル座標系のスケーリング値を設定します
		/// </summary>
		public static void SetLocalScaleX(this GameObject self, float x) {
			self.transform.localScale = new Vector3(x, self.transform.localScale.y, self.transform.localScale.z);
		}

		/// <summary>
		/// Y 軸方向のローカル座標系のスケーリング値を設定します
		/// </summary>
		public static void SetLocalScaleY(this GameObject self, float y) {
			self.transform.localScale = new Vector3(self.transform.localScale.x, y, self.transform.localScale.z);
		}

		/// <summary>
		/// Z 軸方向のローカル座標系のスケーリング値を設定します
		/// </summary>
		public static void SetLocalScaleZ(this GameObject self, float z) {
			self.transform.localScale = new Vector3(self.transform.localScale.x, self.transform.localScale.y, z);
		}

		/// <summary>
		/// Vector3 型でローカル座標系のスケーリング値を設定します
		/// </summary>
		public static void SetLocalScale(this GameObject self, Vector3 v) {
			self.transform.localScale = v;
		}

		/// <summary>
		/// Vector2 型でローカル座標系のスケーリング値を設定します
		/// </summary>
		public static void SetLocalScale(this GameObject self, Vector2 v) {
			self.transform.localScale = new Vector3(v.x, v.y, self.transform.localScale.z);
		}

		/// <summary>
		/// ローカル座標系のスケーリング値を設定します
		/// </summary>
		public static void SetLocalScale(this GameObject self, float x, float y) {
			self.transform.localScale = new Vector3(x, y, self.transform.localScale.z);
		}

		/// <summary>
		/// ローカル座標系のスケーリング値を設定します
		/// </summary>
		public static void SetLocalScale(this GameObject self, float x, float y, float z) {
			self.transform.localScale = new Vector3(x, y, z);
		}

		/// <summary>
		/// X 軸方向のローカル座標系のスケーリング値を加算します
		/// </summary>
		public static void AddLocalScaleX(this GameObject self, float x) {
			self.transform.localScale = new Vector3(self.transform.localScale.x + x, self.transform.localScale.y,
													self.transform.localScale.z);
		}

		/// <summary>
		/// Y 軸方向のローカル座標系のスケーリング値を加算します
		/// </summary>
		public static void AddLocalScaleY(this GameObject self, float y) {
			self.transform.localScale = new Vector3(self.transform.localScale.x, self.transform.localScale.y + y,
													self.transform.localScale.z);
		}

		/// <summary>
		/// Z 軸方向のローカル座標系のスケーリング値を加算します
		/// </summary>
		public static void AddLocalScaleZ(this GameObject self, float z) {
			self.transform.localScale = new Vector3(self.transform.localScale.x, self.transform.localScale.y,
													self.transform.localScale.z + z);
		}

		/// <summary>
		/// Vector3 型でローカル座標系のスケーリング値を加算します
		/// </summary>
		public static void AddLocalScale(this GameObject self, Vector3 v) {
			self.transform.localScale += v;
		}

		/// <summary>
		/// Vector2 型でローカル座標系のスケーリング値を加算します
		/// </summary>
		public static void AddLocalScale(this GameObject self, Vector2 v) {
			self.transform.localScale = new Vector3(self.transform.localScale.x + v.x,
													self.transform.localScale.y + v.y, self.transform.localScale.z);
		}

		/// <summary>
		/// ローカル座標系のスケーリング値を加算します
		/// </summary>
		public static void AddLocalScale(this GameObject self, float x, float y) {
			self.transform.localScale = new Vector3(self.transform.localScale.x + x, self.transform.localScale.y + y,
													self.transform.localScale.z);
		}

		/// <summary>
		/// ローカル座標系のスケーリング値を加算します
		/// </summary>
		public static void AddLocalScale(this GameObject self, float x, float y, float z) {
			self.transform.localScale = new Vector3(self.transform.localScale.x + x, self.transform.localScale.y + y,
													self.transform.localScale.z + z);
		}

		/// <summary>
		/// 回転角を(0, 0, 0)にリセットします
		/// </summary>
		public static void ResetEulerAngles(this GameObject self) {
			self.transform.eulerAngles = Vector3.zero;
		}

		/// <summary>
		/// 回転角を返します
		/// </summary>
		public static Vector3 GetEulerAngles(this GameObject self) {
			return self.transform.eulerAngles;
		}

		/// <summary>
		/// X 軸方向の回転角を返します
		/// </summary>
		public static float GetEulerAngleX(this GameObject self) {
			return self.transform.eulerAngles.x;
		}

		/// <summary>
		/// Y 軸方向の回転角を返します
		/// </summary>
		public static float GetEulerAngleY(this GameObject self) {
			return self.transform.eulerAngles.y;
		}

		/// <summary>
		/// Z 軸方向の回転角を返します
		/// </summary>
		public static float GetEulerAngleZ(this GameObject self) {
			return self.transform.eulerAngles.z;
		}

		/// <summary>
		/// 回転角を設定します
		/// </summary>
		public static void SetEulerAngles(this GameObject self, Vector3 v) {
			self.transform.eulerAngles = v;
		}

		/// <summary>
		/// X 軸方向の回転角を設定します
		/// </summary>
		public static void SetEulerAngleX(this GameObject self, float x) {
			self.transform.eulerAngles = new Vector3(x, self.transform.eulerAngles.y, self.transform.eulerAngles.z);
		}

		/// <summary>
		/// Y 軸方向の回転角を設定します
		/// </summary>
		public static void SetEulerAngleY(this GameObject self, float y) {
			self.transform.eulerAngles = new Vector3(self.transform.eulerAngles.x, y, self.transform.eulerAngles.z);
		}

		/// <summary>
		/// Z 軸方向の回転角を設定します
		/// </summary>
		public static void SetEulerAngleZ(this GameObject self, float z) {
			self.transform.eulerAngles = new Vector3(self.transform.eulerAngles.x, self.transform.eulerAngles.y, z);
		}

		/// <summary>
		/// X 軸方向の回転角を加算します
		/// </summary>
		public static void AddEulerAngleX(this GameObject self, float x) {
			self.transform.Rotate(x, 0, 0, Space.World);
		}

		/// <summary>
		/// Y 軸方向の回転角を加算します
		/// </summary>
		public static void AddEulerAngleY(this GameObject self, float y) {
			self.transform.Rotate(0, y, 0, Space.World);
		}

		/// <summary>
		/// Z 軸方向の回転角を加算します
		/// </summary>
		public static void AddEulerAngleZ(this GameObject self, float z) {
			self.transform.Rotate(0, 0, z, Space.World);
		}

		/// <summary>
		/// ローカルの回転角を(0, 0, 0)にリセットします
		/// </summary>
		public static void ResetLocalEulerAngles(this GameObject self) {
			self.transform.localEulerAngles = Vector3.zero;
		}

		/// <summary>
		/// ローカルの回転角を返します
		/// </summary>
		public static Vector3 GetLocalEulerAngles(this GameObject self) {
			return self.transform.localEulerAngles;
		}

		/// <summary>
		/// ローカルの X 軸方向の回転角を返します
		/// </summary>
		public static float GetLocalEulerAngleX(this GameObject self) {
			return self.transform.localEulerAngles.x;
		}

		/// <summary>
		/// ローカルの Y 軸方向の回転角を返します
		/// </summary>
		public static float GetLocalEulerAngleY(this GameObject self) {
			return self.transform.localEulerAngles.y;
		}

		/// <summary>
		/// ローカルの Z 軸方向の回転角を返します
		/// </summary>
		public static float GetLocalEulerAngleZ(this GameObject self) {
			return self.transform.localEulerAngles.z;
		}

		/// <summary>
		/// ローカルの回転角を設定します
		/// </summary>
		public static void SetLocalEulerAngle(this GameObject self, Vector3 v) {
			self.transform.localEulerAngles = v;
		}

		/// <summary>
		/// ローカルの X 軸方向の回転角を設定します
		/// </summary>
		public static void SetLocalEulerAngleX(this GameObject self, float x) {
			self.transform.localEulerAngles =
				new Vector3(x, self.transform.localEulerAngles.y, self.transform.localEulerAngles.z);
		}

		/// <summary>
		/// ローカルの Y 軸方向の回転角を設定します
		/// </summary>
		public static void SetLocalEulerAngleY(this GameObject self, float y) {
			self.transform.localEulerAngles =
				new Vector3(self.transform.localEulerAngles.x, y, self.transform.localEulerAngles.z);
		}

		/// <summary>
		/// ローカルの Z 軸方向の回転角を設定します
		/// </summary>
		public static void SetLocalEulerAngleZ(this GameObject self, float z) {
			self.transform.localEulerAngles =
				new Vector3(self.transform.localEulerAngles.x, self.transform.localEulerAngles.y, z);
		}

		/// <summary>
		/// ローカルの X 軸方向の回転角を加算します
		/// </summary>
		public static void AddLocalEulerAngleX(this GameObject self, float x) {
			self.transform.Rotate(x, 0, 0, Space.Self);
		}

		/// <summary>
		/// ローカルの Y 軸方向の回転角を加算します
		/// </summary>
		public static void AddLocalEulerAngleY(this GameObject self, float y) {
			self.transform.Rotate(0, y, 0, Space.Self);
		}

		/// <summary>
		/// ローカルの X 軸方向の回転角を加算します
		/// </summary>
		public static void AddLocalEulerAngleZ(this GameObject self, float z) {
			self.transform.Rotate(0, 0, z, Space.Self);
		}

		/// <summary>
		/// 親オブジェクトが存在するかどうかを返します
		/// </summary>
		public static bool HasParent(this GameObject self) {
			return self.transform.parent != null;
		}

		/// <summary>
		/// 親オブジェクトを設定します
		/// </summary>
		public static void SetParent(this GameObject self, Component parent) {
			self.transform.SetParent(parent != null ? parent.transform : null);
		}

		/// <summary>
		/// 親オブジェクトを設定します
		/// </summary>
		public static void SetParent(this GameObject self, GameObject parent) {
			self.transform.SetParent(parent != null ? parent.transform : null);
		}

		/// <summary>
		/// 親オブジェクトを設定します
		/// </summary>
		public static void SetParent(this GameObject self, Component parent, bool worldPositionStays) {
			self.transform.SetParent(parent != null ? parent.transform : null, worldPositionStays);
		}

		/// <summary>
		/// 親オブジェクトを設定します
		/// </summary>
		public static void SetParent(this GameObject self, GameObject parent, bool worldPositionStays) {
			self.transform.SetParent(parent != null ? parent.transform : null, worldPositionStays);
		}

		/// <summary>
		/// ローカル座標を維持して親オブジェクトを設定します
		/// </summary>
		public static void SafeSetParent(this GameObject self, Component parent) {
			SafeSetParent(self, parent.gameObject);
		}

		/// <summary>
		/// ローカル座標を維持して親オブジェクトを設定します
		/// </summary>
		public static void SafeSetParent(this GameObject self, GameObject parent) {
			var t = self.transform;
			var localPosition = t.localPosition;
			var localRotation = t.localRotation;
			var localScale = t.localScale;
			t.parent = parent.transform;
			t.localPosition = localPosition;
			t.localRotation = localRotation;
			t.localScale = localScale;
			self.layer = parent.layer;
		}

		/// <summary>
		/// 向きを変更します
		/// </summary>
		public static void LookAt(this GameObject self, GameObject target) {
			self.transform.LookAt(target.transform);
		}

		/// <summary>
		/// 向きを変更します
		/// </summary>
		public static void LookAt(this GameObject self, Transform target) {
			self.transform.LookAt(target);
		}

		/// <summary>
		/// 向きを変更します
		/// </summary>
		public static void LookAt(this GameObject self, Vector3 worldPosition) {
			self.transform.LookAt(worldPosition);
		}

		/// <summary>
		/// 向きを変更します
		/// </summary>
		public static void LookAt(this GameObject self, GameObject target, Vector3 worldUp) {
			self.transform.LookAt(target.transform, worldUp);
		}

		/// <summary>
		/// 向きを変更します
		/// </summary>
		public static void LookAt(this GameObject self, Transform target, Vector3 worldUp) {
			self.transform.LookAt(target, worldUp);
		}

		/// <summary>
		/// 向きを変更します
		/// </summary>
		public static void LookAt(this GameObject self, Vector3 worldPosition, Vector3 worldUp) {
			self.transform.LookAt(worldPosition, worldUp);
		}

		/// <summary>
		/// 指定されたインデックスの子オブジェクトを返します
		/// </summary>
		public static Transform GetChild(this GameObject self, int index) {
			return self.transform.GetChild(index);
		}

		/// <summary>
		/// 親オブジェクトを返します
		/// </summary>
		public static Transform GetParent(this GameObject self) {
			return self.transform.parent;
		}

		/// <summary>
		/// ルートとなるオブジェクトを返します
		/// </summary>
		public static GameObject GetRoot(this GameObject self) {
			var root = self.transform.root;
			return root != null ? root.gameObject : null;
		}

		/// <summary>
		/// レイヤー名を使用してレイヤーを設定します
		/// </summary>
		public static void SetLayer(this GameObject self, string layerName) {
			self.layer = LayerMask.NameToLayer(layerName);
		}

		/// <summary>
		/// 自分自身を含めたすべての子オブジェクトのレイヤーを設定します
		/// </summary>
		public static void SetLayerRecursively(this GameObject self, int layer) {
			self.layer = layer;

			foreach (Transform n in self.transform) {
				SetLayerRecursively(n.gameObject, layer);
			}
		}

		/// <summary>
		/// 自分自身を含めたすべての子オブジェクトのレイヤーを設定します
		/// </summary>
		public static void SetLayerRecursively(this GameObject self, string layerName) {
			self.SetLayerRecursively(LayerMask.NameToLayer(layerName));
		}

		/// <summary>
		/// グローバル座標系における X 軸方向のスケーリング値を返します
		/// </summary>
		public static float GetGlobalScaleX(this GameObject self) {
			var t = self.transform;
			var x = 1f;
			while (t != null) {
				x *= t.localScale.x;
				t = t.parent;
			}

			return x;
		}

		/// <summary>
		/// グローバル座標系における Y 軸方向のスケーリング値を返します
		/// </summary>
		public static float GetGlobalScaleY(this GameObject self) {
			var t = self.transform;
			var y = 1f;
			while (t != null) {
				y *= t.localScale.y;
				t = t.parent;
			}

			return y;
		}

		/// <summary>
		/// グローバル座標系における Z 軸方向のスケーリング値を返します
		/// </summary>
		public static float GetGlobalScaleZ(this GameObject self) {
			var t = self.transform;
			var z = 1f;
			while (t != null) {
				z *= t.localScale.z;
				t = t.parent;
			}

			return z;
		}

		/// <summary>
		/// グローバル座標系におけるスケーリング値を返します
		/// </summary>
		public static Vector3 GetGlobalScale(this GameObject self) {
			var t = self.transform;
			var scale = Vector3.one;
			while (t != null) {
				scale.x *= t.localScale.x;
				scale.y *= t.localScale.y;
				scale.z *= t.localScale.z;
				t = t.parent;
			}

			return scale;
		}

		/// <summary>
		/// 指定されたゲームオブジェクトが null または非アクティブであるかどうかを示します
		/// </summary>
		public static bool IsNullOrInactive(this GameObject self) {
			return self == null || !self.activeInHierarchy || !self.activeSelf;
		}

		/// <summary>
		/// 指定されたゲームオブジェクトが null ではないかつ非アクティブではないかどうかを示します
		/// </summary>
		public static bool IsNotNullOrInactive(this GameObject self) {
			return !self.IsNullOrInactive();
		}

		/// <summary>
		/// 新しいシーンを読み込む時に自動で破棄されないようにします
		/// </summary>
		public static void DontDestroyOnLoad(this GameObject self) {
			GameObject.DontDestroyOnLoad(self);
		}

		/// <summary>
		/// すべての親オブジェクトを返します
		/// </summary>
		public static GameObject[] GetAllParent(this GameObject self) {
			var result = new List<GameObject>();
			for (var parent = self.transform.parent; parent != null; parent = parent.parent) {
				result.Add(parent.gameObject);
			}

			return result.ToArray();
		}

		/// <summary>
		/// ルートとなるオブジェクトからのパスを返します
		/// </summary>
		public static string GetRootPath(this GameObject self) {
			var parents = self.GetAllParent();
			parents.Reverse();
			var parentNames = parents.Select(c => c.name);

			return ExString.Join("/", parentNames) + "/" + self.name;
		}

		/// <summary>
		/// コンポーネントのインターフェースを取得して返します
		/// </summary>
		public static T GetComponentInterface<T>(this GameObject self) where T : class {
			foreach (var n in self.GetComponents<Component>()) {
				if (n is T component) {
					return component;
				}
			}

			return null;
		}

		/// <summary>
		/// コンポーネントのインターフェースを複数取得して返します
		/// </summary>
		public static T[] GetComponentInterfaces<T>(this GameObject self) where T : class {
			var result = new List<T>();
			foreach (var n in self.GetComponents<Component>()) {
				if (n is T component) {
					result.Add(component);
				}
			}

			return result.ToArray();
		}

		/// <summary>
		/// 子のコンポーネントのインターフェースを取得して返します
		/// </summary>
		public static T GetComponentInterfaceInChildren<T>(this GameObject self) where T : class {
			return self.GetComponentInterfaceInChildren<T>(false);
		}

		/// <summary>
		/// 子のコンポーネントのインターフェースを取得して返します
		/// </summary>
		public static T GetComponentInterfaceInChildren<T>(this GameObject self, bool includeInactive) where T : class {
			foreach (var n in self.GetComponentsInChildren<Component>(includeInactive)) {
				if (n is T component) {
					return component;
				}
			}

			return null;
		}

		/// <summary>
		/// 子のコンポーネントのインターフェースを複数取得して返します
		/// </summary>
		public static T[] GetComponentInterfacesInChildren<T>(this GameObject self) where T : class {
			return self.GetComponentInterfacesInChildren<T>(false);
		}

		/// <summary>
		/// 子のコンポーネントのインターフェースを複数取得して返します
		/// </summary>
		public static T[] GetComponentInterfacesInChildren<T>(this GameObject self, bool includeInactive)
			where T : class {
			var result = new List<T>();
			foreach (var n in self.GetComponentsInChildren<Component>(includeInactive)) {
				if (n is T component) {
					result.Add(component);
				}
			}

			return result.ToArray();
		}

		/// <summary>
		/// 無効なコンポーネントがアタッチされている場合 true を返します
		/// </summary>
		public static bool HasMissingScript(this GameObject self) {
			return self.GetComponents<Component>().Any(c => c == null);
		}

		/// <summary>
		/// 指定されたアクティブと逆の状態にしてから指定されたアクティブになります
		/// </summary>
		public static void ToggleActive(this GameObject self, bool isActive) {
			self.SetActive(!isActive);
			self.SetActive(isActive);
		}

		public static void SetLayer(this GameObject self, int layer) {
			self.layer = layer;
		}
	}
}