using UnityEngine;

namespace UnityExtensions
{
	public static class ExVector
	{
		/// <summary>
		/// ベクトルを各要素1対1に掛け合わせます
		/// </summary>
		/// <returns>The one to one.</returns>
		/// <param name="origin">Origin.</param>
		/// <param name="factor">Factor.</param>
		public static Vector3 ProductOneToOne(this Vector3 origin, Vector3 factor) {
			return new Vector3(origin.x * factor.x, origin.y * factor.y, origin.z * factor.z);
		}

		/// <summary>
		/// ベクトルを各要素1対1に掛け合わせます
		/// </summary>
		/// <returns>The one to one.</returns>
		/// <param name="origin">Origin.</param>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		/// <param name="z">The z coordinate.</param>
		public static Vector3 ProductOneToOne(this Vector3 origin, float x, float y, float z) {
			return origin.ProductOneToOne(new Vector3(x, y, z));
		}

		/// <summary>
		/// ターゲットとの距離の絶対値を返します
		/// </summary>
		/// <returns>The distance.</returns>
		/// <param name="origin">Origin.</param>
		/// <param name="target">Target.</param>
		public static float GetDistance(this Vector3 origin, Vector3 target) {
			return Mathf.Abs((origin - target).sqrMagnitude);
		}

		/// <summary>
		/// ターゲットとの高さを無視した距離の絶対値を返します
		/// </summary>
		/// <returns>The distance2 d.</returns>
		/// <param name="origin">Origin.</param>
		/// <param name="target">Target.</param>
		public static float GetDistance2D(this Vector3 origin, Vector3 target) {
			return Mathf.Abs((origin.SetY(0) - target.SetY(0)).sqrMagnitude);
		}

		/// <summary>
		/// 対象を指す方向ベクトルを計算します
		/// </summary>
		/// <param name="origin"></param>
		/// <param name="factor"></param>
		/// <returns></returns>
		public static Vector3 GetTowardNormalizedVector(this Vector3 origin, Vector3 factor) {
			return (factor - origin).normalized;
		}

		/// <summary>
		/// xを上書きしたベクトルを返します
		/// </summary>
		/// <returns>The x.</returns>
		/// <param name="origin">Origin.</param>
		/// <param name="x">The x coordinate.</param>
		public static Vector3 SetX(this Vector3 origin, float x) {
			return new Vector3(x, origin.y, origin.z);
		}

		/// <summary>
		/// yを上書きしたベクトルを返します
		/// </summary>
		/// <returns>The y.</returns>
		/// <param name="origin">Origin.</param>
		/// <param name="y">The y coordinate.</param>
		public static Vector3 SetY(this Vector3 origin, float y) {
			return new Vector3(origin.x, y, origin.z);
		}

		/// <summary>
		/// zを上書きしたベクトルを返します
		/// </summary>
		/// <returns>The z.</returns>
		/// <param name="origin">Origin.</param>
		/// <param name="z">The z coordinate.</param>
		public static Vector3 SetZ(this Vector3 origin, float z) {
			return new Vector3(origin.x, origin.y, z);
		}

		/// <summary>
		/// xを0にして正規化したベクトルを返します
		/// </summary>
		/// <returns>The x.</returns>
		/// <param name="origin">Origin.</param>
		public static Vector3 SuppressX(this Vector3 origin) {
			var v = origin.SetX(0);
			return v.normalized;
		}

		/// <summary>
		/// yを0にして正規化したベクトルを返します
		/// </summary>
		/// <returns>The x.</returns>
		/// <param name="origin">Origin.</param>
		public static Vector3 SuppressY(this Vector3 origin) {
			var v = origin.SetY(0);
			return v.normalized;
		}

		/// <summary>
		/// zを0にして正規化したベクトルを返します
		/// </summary>
		/// <returns>The x.</returns>
		/// <param name="origin">Origin.</param>
		public static Vector3 SuppressZ(this Vector3 origin) {
			var v = origin.SetZ(0);
			return v.normalized;
		}

		/// <summary>
		/// 指定座標の周辺のベクトルをランダムで返します
		/// </summary>
		/// <returns>The around2 d.</returns>
		/// <param name="origin">Origin.</param>
		/// <param name="radius">Radius.</param>
		public static Vector3 GetAround(this Vector3 origin, float radius) {
			var rad = UnityEngine.Random.Range(-180, 180);
			var vec = Quaternion.AngleAxis(rad, Vector3.up) * Vector3.forward;
			return origin + vec * radius;
		}

		/// <summary>
		/// ベクトルの加算結果を返します
		/// </summary>
		/// <param name="origin">Origin.</param>
		/// <param name="target">Target.</param>
		public static Vector3 Add(this Vector3 origin, Vector3 target) {
			return origin + target;
		}

		/// <summary>
		/// ベクトルの減算結果を返します
		/// </summary>
		/// <param name="origin">Origin.</param>
		/// <param name="target">Target.</param>
		public static Vector3 Subtraction(this Vector3 origin, Vector3 target) {
			return origin - target;
		}

		/// <summary>
		/// ２つのベクトルの角度差を返します
		/// 返される値は0 ~ 180です
		/// </summary>
		/// <returns>The angle.</returns>
		/// <param name="origin">Origin.</param>
		/// <param name="target">Target.</param>
		public static float GetAngle(this Vector3 origin, Vector3 target) {
			return Vector3.Angle(origin, target);
		}

		/// <summary>
		/// ベクトルを指定角度回転します
		/// </summary>
		/// <param name="origin">Origin.</param>
		/// <param name="xAngle">X angle is 0f ~ 360f</param>
		/// <param name="yAngle">Y angle is 0f ~ 360f</param>
		/// <param name="zAngle">Z angle is 0f ~ 360f</param>
		public static Vector3 Rotation(this Vector3 origin, float xAngle, float yAngle, float zAngle) {
			return Quaternion.Euler(xAngle, yAngle, zAngle) * origin;
		}

		/// <summary>
		/// ベクトルを指定したベクトルに向かって指定角度だけ回転します
		/// </summary>
		/// <param name="origin">Origin.</param>
		/// <param name="target">Target.</param>
		/// <param name="angle">Angle.</param>
		public static Vector3 Rotation(this Vector3 origin, Vector3 target, float angle) {
			// 指定したベクトルに向かって指定角度回転
			var axis = Vector3.Cross(target, origin);
			var res = Quaternion.AngleAxis(angle, axis) * target;
			return res;
		}

		/// <summary>
		/// Vector3をVector2へ変換します
		/// </summary>
		/// <returns>The vector2.</returns>
		/// <param name="origin">Origin.</param>
		public static Vector2 XY(this Vector3 origin) {
			return new Vector2(origin.x, origin.y);
		}

		/// <summary>
		/// Vector3をVector2へ変換します
		/// </summary>
		/// <returns>The vector2.</returns>
		/// <param name="origin">Origin.</param>
		public static Vector2 XZ(this Vector3 origin) {
			return new Vector2(origin.x, origin.z);
		}

		/// <summary>
		/// Vector3をVector2へ変換します
		/// </summary>
		/// <returns>The vector2.</returns>
		/// <param name="origin">Origin.</param>
		public static Vector2 YZ(this Vector3 origin) {
			return new Vector2(origin.y, origin.z);
		}

		/// <summary>
		/// ベクトルをANGLEだけ回転させる(正数を与えると内側に傾く)
		/// ANGLEは度数法
		/// </summary>
		/// <returns>The vector.</returns>
		/// <param name="vector">Vector.</param>
		/// <param name="ANGLE">ANGLE.</param>
		public static Vector2 RotateVector(this Vector2 origin, float angle, bool turn) {
			float rad = angle.ToRadian();
			float cos = Mathf.Cos(rad);
			float sin = Mathf.Sin(rad);

			float x = origin.x;
			float y = origin.y;

			return new Vector2(x * cos + y * sin, -x * sin + y * cos);
		}

		/// <summary>
		/// Vector3の値がすべて同じか返します
		/// </summary>
		/// <returns><c>true</c> if is uniform the specified self; otherwise, <c>false</c>.</returns>
		/// <param name="self">Self.</param>
		public static bool IsUniform(this Vector3 self) {
			return Mathf.Approximately(self.x, self.y) && Mathf.Approximately(self.x, self.z);
		}

		public static Vector3 NearestPointOnAxis(this Vector3 axisDirection, Vector3 point, bool isNormalized = false) {
			if (!isNormalized) axisDirection.Normalize();

			// 内積を求めます
			var d = Vector3.Dot(point, axisDirection);
			return axisDirection * d;
		}

		public static Vector3 NearestPointOnLine(
			this Vector3 lineDirection, Vector3 point, Vector3 pointOnLine, bool isNormalized = false
		) {
			if (!isNormalized) lineDirection.Normalize();

			// 内積を求めます
			var d = Vector3.Dot(point - pointOnLine, lineDirection);
			return pointOnLine + lineDirection * d;
		}

		/// <summary>
		/// 線分の交差判定(2D版).
		/// </summary>
		/// <returns><c>true</c>, 交差している, <c>false</c> 交差していない.</returns>
		/// <param name="a">線分a-bのa.</param>
		/// <param name="b">線分a-bのb.</param>
		/// <param name="c">線分c-dのc.</param>
		/// <param name="d">線分c-dのd.</param>
		public static bool CrossLineCheck2D(Vector3 a, Vector3 b, Vector3 c, Vector3 d) {
			float ta = (a.x - b.x) * (c.y - a.y) + (a.y - b.y) * (a.x - c.x);
			float tb = (a.x - b.x) * (d.y - a.y) + (a.y - b.y) * (a.x - d.x);
			float tc = (c.x - d.x) * (a.y - c.y) + (c.y - d.y) * (c.x - a.x);
			float td = (c.x - d.x) * (b.y - c.y) + (c.y - d.y) * (c.x - b.x);

			if ((ta * tb < 0f) && (tc * td) < 0f) {
				return true;
			}

			return false;
		}
	}
}