namespace UnityExtensions
{
    public static class ExRandom
    {
        /// <summary>
        /// Randomly returns a floating point number from 0.0 to 1.0.
        /// </summary>
        public static float Value => UnityEngine.Random.value;

        /// <summary>
        /// Randomly return true or false boolean values.
        /// </summary>
        public static bool BoolValue => UnityEngine.Random.Range(0, 2) == 0;

        /// <summary>
        /// Returns the value randomly from the specified array.
        /// </summary>
        public static T Random<T>(params T[] values) {
            return values[UnityEngine.Random.Range(0, values.Length)];
        }

        /// <summary>
        /// Randomly returns an integer between 0 and max - 1.
        /// </summary>
        public static int Range(int max) {
            return UnityEngine.Random.Range(0, max);
        }

        /// <summary>
        /// Randomly returns an integer between 0 and max - 1.
        /// </summary>
        public static byte RangeByte(byte max) {
            return (byte)UnityEngine.Random.Range(0, max);
        }

        /// <summary>
        /// Randomly returns an integer between 0 and max - 1.
        /// </summary>
        public static ushort RangeUshort(ushort max) {
            return (ushort)UnityEngine.Random.Range(0, max);
        }

        /// <summary>
        /// Randomly returns an integer between 0 and max - 1.
        /// </summary>
        public static int RangeUint(uint max) {
            return (int)UnityEngine.Random.Range(0, max);
        }

        /// <summary>
        /// Randomly returns an integer between 0 and max - 1.
        /// </summary>
        public static ulong RangeUlong(ulong max) {
            return (ulong)UnityEngine.Random.Range(0, (int)max);
        }

        /// <summary>
        /// Randomly returns an integer between min and max - 1.
        /// </summary>
        public static int Range(int min, int max) {
            return UnityEngine.Random.Range(min, max);
        }

        /// <summary>
        /// Randomly returns a floating point number between min and max - 1.
        /// </summary>
        public static float Range(float min, float max) {
            return UnityEngine.Random.Range(min, max);
        }
    }
}