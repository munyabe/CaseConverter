using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CaseConverter
{
    /// <summary>
    /// 文字列のケースを変換するクラスです。
    /// </summary>
    public static class StringCaseConverter
    {
        /// <summary>
        /// 文字列をスネークケース⇒キャメルケース⇒パスカルケースの順に変換します。
        /// </summary>
        /// <param name="input">変換する文字列</param>
        /// <returns>変換した文字列</returns>
        public static string Convert(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return input;
            }
            else if (input.Contains('_'))
            {
                return GetWordsFromSnakeCase(input).ToCamelCase();
            }
            else if (char.IsLower(input[0]))
            {
                return GetWordsFromCamelCase(input).ToPascalCase();
            }
            else
            {
                return GetWordsFromCamelCase(input).ToSnakeCase();
            }
        }

        /// <summary>
        /// キャメルケースの文字列を単語に分割します。
        /// </summary>
        /// <remarks>
        /// 空白や記号、小文字と大文字の境を単語の境界とします。
        /// 行末でない場所に大文字が連続する場合は、最後の1文字を除いて単語と認識します。
        /// </remarks>
        internal static IEnumerable<string> GetWordsFromCamelCase(string input)
        {
            return Regex.Matches(input, @"[a-z\d]+|[A-Z\d]+(?![A-Za-z\d])|[A-Z\d]+(?=[A-Z])|[A-Z][a-z\d]*").GetValues();
        }

        /// <summary>
        /// スネークケースの文字列を単語に分割します。
        /// </summary>
        internal static IEnumerable<string> GetWordsFromSnakeCase(string input)
        {
            return input.Split('_');
        }

        /// <summary>
        /// 単語をキャメルケースの文字列に連結します。
        /// </summary>
        internal static string ToCamelCase(this IEnumerable<string> words)
        {
            var result = new StringBuilder();
            var isFirst = true;
            foreach (var word in words)
            {
                var top = isFirst ? char.ToLower(word[0]) : char.ToUpper(word[0]);
                isFirst = false;

                result.Append(top + word.Substring(1, word.Length - 1).ToLower());
            }

            return result.ToString();
        }

        /// <summary>
        /// 単語をパスカルケースの文字列に連結します。
        /// </summary>
        internal static string ToPascalCase(this IEnumerable<string> words)
        {
            var result = new StringBuilder();
            foreach (var word in words)
            {
                result.Append(char.ToUpper(word[0]) + word.Substring(1, word.Length - 1).ToLower());
            }

            return result.ToString();
        }

        /// <summary>
        /// 単語をスネークケースの文字列に連結します。
        /// </summary>
        internal static string ToSnakeCase(this IEnumerable<string> words)
        {
            return string.Join("_", words.Select(CultureInfo.CurrentCulture.TextInfo.ToLower));
        }

        /// <summary>
        /// パターンに一致した文字列を取得します。
        /// </summary>
        private static IEnumerable<string> GetValues(this MatchCollection source)
        {
            for (int i = 0; i < source.Count; i++)
            {
                yield return source[i].Value;
            }
        }
    }
}
