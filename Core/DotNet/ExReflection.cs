using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace UnityExtensions
{
	public static class ExReflection
	{
		private class FieldInfoComparer : IEqualityComparer<FieldInfo>
		{
			public bool Equals(FieldInfo x, FieldInfo y) {
				return x.DeclaringType == y.DeclaringType && x.Name == y.Name;
			}

			public int GetHashCode(FieldInfo obj) {
				return obj.Name.GetHashCode() ^ obj.DeclaringType.GetHashCode();
			}
		}

		private static readonly FieldInfoComparer fieldInfoComparer = new FieldInfoComparer();

		public static FieldInfo[] GetFieldInfoWithBaseClass(this Type type, BindingFlags flags) {
			FieldInfo[] fieldInfos = type.GetFields(flags);
			Type objectType = typeof(object);
			if (type.BaseType == objectType) {
				return fieldInfos;
			}

			Type currentType = type;
			HashSet<FieldInfo> fieldInfosHash = new HashSet<FieldInfo>(fieldInfos, fieldInfoComparer);
			while (currentType != objectType) {
				fieldInfos = currentType.GetFields(flags);
				fieldInfosHash.UnionWith(fieldInfos);
				currentType = currentType.BaseType;
			}

			return fieldInfosHash.ToArray();
		}

		public static object InvokeGeneric(this Type type, object obj, string methodName, Type[] parameterTypes, Type[] argumentTypes, params object[] parameters) {
			MethodInfo methodInfo = type.GetMethod(methodName, parameterTypes).MakeGenericMethod(argumentTypes);

			ExDebug.Log("method name", methodInfo.Name);
			ExDebug.Log("obj", obj.ToString());
			foreach (var parameterType in parameterTypes) {
				ExDebug.Log("parameterType", parameterType.Name);
			}

			return methodInfo.Invoke(obj, parameterTypes);
		}

		public static object Invoke(this Type type, object obj, string methodName, Type[] parameterTypes, params object[] parameters) {
			MethodInfo methodInfo = type.GetMethod(methodName, parameterTypes);
			return methodInfo.Invoke(obj, parameters);
		}
	}
}