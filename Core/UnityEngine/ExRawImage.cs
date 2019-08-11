using UnityEngine.UI;

namespace UnityExtensions
{
    public static class ExRawImage
    {
        public static float GetUvRectX(this RawImage self) {
            return self.uvRect.x;
        }

        public static float GetUvRectY(this RawImage self) {
            return self.uvRect.y;
        }

        public static float GetUvRectWidth(this RawImage self) {
            return self.uvRect.width;
        }

        public static float GetUvRectHeight(this RawImage self) {
            return self.uvRect.height;
        }

        public static void SetUvRectX(this RawImage self, float value) {
            var uvRect = self.uvRect;
            uvRect.x = value;
            self.uvRect = uvRect;
        }

        public static void SetUvRectY(this RawImage self, float value) {
            var uvRect = self.uvRect;
            uvRect.y = value;
            self.uvRect = uvRect;
        }

        public static void SetUvRectWidth(this RawImage self, float value) {
            var uvRect = self.uvRect;
            uvRect.width = value;
            self.uvRect = uvRect;
        }

        public static void SetUvRectHeight(this RawImage self, float value) {
            var uvRect = self.uvRect;
            uvRect.height = value;
            self.uvRect = uvRect;
        }
    }
}