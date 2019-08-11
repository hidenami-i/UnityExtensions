using UnityEngine;

namespace UnityExtensions
{
    public static class ExComponent
    {
        /// <summary>
        /// Hases the component.
        /// </summary>
        /// <returns><c>true</c>, if component was hased, <c>false</c> otherwise.</returns>
        /// <param name="self">Self.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static bool HasComponent<T>(this Component self) where T : Component {
            return self.gameObject.GetComponent<T>() != null;
        }

        /// <summary>
        /// GetComponentの拡張関数
        /// </summary>
        public static T GetParentOrAddComponent<T>(this Component self) where T : Component {
            return self.gameObject.SafeFindParent().GetComponent<T>() ?? self.gameObject.AddComponent<T>();
        }
    }
}