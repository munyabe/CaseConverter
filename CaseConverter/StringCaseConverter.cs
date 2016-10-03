using System;
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
        /// 文字列を指定したパターンで変換します。
        /// </summary>
        /// <param name="input">変換する文字列</param>
        /// <param name="patterns">変換のパターン</param>
        /// <returns>変換した文字列</returns>
        public static string Convert(string input, IList<StringCasePattern> patterns)
        {
            if (patterns == null || patterns.Any() == false)
            {
                return input;
            }

            var words = GetWords(input);
            if (words.Any() == false)
            {
                return input;
            }

            var currentPattern = GetCasePattern(input);
            var nextIndex = patterns.IndexOf(currentPattern) + 1;
            if (patterns.Count <= nextIndex)
            {
                nextIndex = 0;
            }

            return GetConverter(patterns[nextIndex])(words);
        }

        /// <summary>
        /// 指定した文字列のパターンを判定します。
        /// </summary>
        /// <param name="input">判定する文字列</param>
        /// <returns>文字列のパターン</returns>
        internal static StringCasePattern GetCasePattern(string input)
        {
            if (input.Contains('_'))
            {
                return StringCasePattern.SnakeCase;
            }
            else if (char.IsLower(input[0]))
            {
                return StringCasePattern.CamelCase;
            }
            else
            {
                return StringCasePattern.PascalCase;
            }
        }

        /// <summary>
        /// 文字列を単語に分割します。
        /// </summary>
        /// <remarks>
        /// 空白や記号、小文字と大文字の境を単語の境界とします。
        /// 行末でない場所に大文字が連続する場合は、最後の1文字を除いて単語と認識します。
        /// </remarks>
        internal static IEnumerable<string> GetWords(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return Enumerable.Empty<string>();
            }

            return Regex.Matches(input, @"[a-z\d]+|[A-Z\d]+(?![A-Za-z\d])|[A-Z\d]+(?=[A-Z])|[A-Z][a-z\d]*").GetValues();
        }

        /// <summary>
        /// 単語をキャメルケースの文字列に連結します。
        /// </summary>
        internal static string ToCamelCase(IEnumerable<string> words)
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
        internal static string ToPascalCase(IEnumerable<string> words)
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
        internal static string ToSnakeCase(IEnumerable<string> words)
        {
            return string.Join("_", words.Select(CultureInfo.CurrentCulture.TextInfo.ToLower));
        }

        /// <summary>
        /// 複合の単語を指定のパターンで連結する処理を取得します。
        /// </summary>
        private static Func<IEnumerable<string>, string> GetConverter(StringCasePattern pattern)
        {
            switch (pattern)
            {
                case StringCasePattern.CamelCase:
                    return ToCamelCase;
                case StringCasePattern.PascalCase:
                    return ToPascalCase;
                case StringCasePattern.SnakeCase:
                    return ToSnakeCase;
                default:
                    throw new NotSupportedException($"This pattern [{pattern}]is not supported.");
            }
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
