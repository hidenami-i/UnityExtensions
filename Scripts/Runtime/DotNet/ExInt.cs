namespace UnityExtensions
{
    public static class ExInt
    {
        /// <summary>
        /// <para>10 進数の数値を 16 進数の文字列に変換します</para>
        /// <para>1234.DecimalToHex() // 0004D2</para>
        /// </summary>
        public static string DecimalToHex(this int self) {
            self &= 0xFFFFFF;
            return self.ToString("X6");
        }

        /// <summary>
        /// 偶数かどうかを返します
        /// </summary>
        public static bool IsEven(this int self) {
            return self % 2 == 0;
        }

        /// <summary>
        /// 奇数かどうかを返します
        /// </summary>
        public static bool IsOdd(this int self) {
            return self % 2 == 1;
        }

        /// <summary>
        /// 数値をalphabetへ変換します。
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static string ToAlphabet(this int self) {
            if (self <= 0) {
                return "";
            }

            int n = self % 26;
            n = n == 0 ? 26 : n;
            string s = ((char)(n + 64)).ToString();
            if (self == n)
                return s;
            return ((self - n) / 26).ToAlphabet() + s;
        }

        /// <summary>
        /// 数値の桁数を取得します。
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static int GetDigitsSize(this int self) {
            if (self == 0) {
                return 1;
            }

            return (int)UnityEngine.Mathf.Log10(self) + 1;
        }

        public static int ToCharsNonAlloc(this int self, char[] output, int start = 0) {
            const int Zero = '0';

            bool negative = System.Math.Sign(self) == -1;

            if (negative) {
                output[start] = '-';
                start++;
                self *= -1;
            }

            int digitsNum = self.GetDigitsSize();

            for (int i = digitsNum - 1; i >= 0; --i) {
                int digit = self % 10;
                output[start + i] = (char)(digit + Zero);
                self /= 10;
            }

            if (negative) {
                digitsNum++;
            }

            return digitsNum;
        }
    }
}