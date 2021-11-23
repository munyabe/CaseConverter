namespace CaseConverter.Utils
{
    /// <summary>
    /// 文字列に関する機能を提供するクラスです。
    /// </summary>
    public static class StringUtil
    {
        /// <summary>
        /// 指定した文字列を、単語の先頭のみ大文字で、他は小文字の文字列に変換します。
        /// </summary>
        /// <param name="input">変換する文字列</param>
        /// <returns>変換した文字列</returns>
        public static string ToFirstUpper(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            return char.ToUpper(input[0]) + input.Substring(1, input.Length - 1).ToLower();
        }
    }
}
