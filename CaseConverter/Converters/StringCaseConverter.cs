﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace CaseConverter.Converters
{
    /// <summary>
    /// 文字列のケースを変換するクラスです。
    /// </summary>
    public static class StringCaseConverter
    {
        /// <summary>
        /// 文字列を変換するコンバーターのインスタンス一覧です。
        /// </summary>
        private static readonly Dictionary<StringCasePattern, ICaseConverter> _caseConverters =
            new Dictionary<StringCasePattern, ICaseConverter>
            {
                [StringCasePattern.CamelCase] = new CamelCaseConverter(),
                [StringCasePattern.PascalCase] = new PascalCaseConverter(),
                [StringCasePattern.SnakeCase] = new SnakeCaseConverter(),
                [StringCasePattern.PascalSnakeCase] = new PascalSnakeCaseConverter(),
                [StringCasePattern.ScreamingSnakeCase] = new ScreamingSnakeCaseConverter(),
                [StringCasePattern.KebabCase] = new KebabCaseConverter()
            };

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

            return GetConverter(patterns[nextIndex]).Convert(words);
        }

        /// <summary>
        /// 指定した文字列のパターンを判定します。
        /// </summary>
        /// <param name="input">判定する文字列</param>
        /// <returns>文字列のパターン</returns>
        internal static StringCasePattern GetCasePattern(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return StringCasePattern.CamelCase;
            }

            if (input.Contains('_'))
            {
                if (input.Length == 1)
                {
                    return StringCasePattern.SnakeCase;
                }
                else if (input.All(x => char.IsUpper(x) || x == '_'))
                {
                    return StringCasePattern.ScreamingSnakeCase;
                }
                else
                {
                    return char.IsUpper(input[0]) ? StringCasePattern.PascalSnakeCase : StringCasePattern.SnakeCase;
                }
            }
            else if (input.Contains('-'))
            {
                return StringCasePattern.KebabCase;
            }
            else if (char.IsUpper(input[0]))
            {
                return 1 < input.Length && input.Skip(1).All(x => char.IsUpper(x)) ? StringCasePattern.ScreamingSnakeCase : StringCasePattern.PascalCase;
            }
            else
            {
                return StringCasePattern.CamelCase;
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

            return Regex.Matches(input, @"[\p{Ll}\d]+|[\p{Lu}\d]+(?![\p{L}\d])|[\p{Lu}\d]+(?=\p{Lu})|\p{Lu}[\p{Ll}\d]*").GetValues();
        }

        /// <summary>
        /// 複合の単語を指定のパターンで連結するコンバーターを取得します。
        /// </summary>
        private static ICaseConverter GetConverter(StringCasePattern pattern)
        {
            ICaseConverter converter;
            if (_caseConverters.TryGetValue(pattern, out converter) && converter != null)
            {
                return converter;
            }
            else
            {
                throw new InvalidOperationException($"This pattern [{pattern}]is not supported.");
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
