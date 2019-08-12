using System.Diagnostics;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace UnityExtensions
{
    public static class ExUnityEditor
    {
        /// <summary>
        /// Refresh AssetDatabase.
        /// </summary>
        [Conditional("UNITY_EDITOR")]
        public static void AssetDataBaseRefresh() {
            #if UNITY_EDITOR
            AssetDatabase.Refresh();
            #endif
        }
    }
}