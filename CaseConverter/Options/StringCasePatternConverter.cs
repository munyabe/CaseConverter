using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using CaseConverter.Converters;

namespace CaseConverter.Options
{
    /// <summary>
    /// <see cref="StringCasePattern"/>を文字列に変換するコンバーターです。
    /// </summary>
    public class StringCasePatternConverter : EnumConverter
    {
        /// <summary>
        /// <see cref="StringCasePattern"/>と文字列の対応表です。
        /// </summary>
        private static readonly Dictionary<StringCasePattern, string> _names = new Dictionary<StringCasePattern, string>
        {
            [StringCasePattern.CamelCase] = "camelCase",
            [StringCasePattern.PascalCase] = "PascalCase",
            [StringCasePattern.SnakeCase] = "snake_case",
            [StringCasePattern.PascalSnakeCase] = "Pascal_Snake_Case",
            [StringCasePattern.ScreamingSnakeCase] = "SCREAMING_SNAKE_CASE",
            [StringCasePattern.KebabCase] = "kebab-case",
            [StringCasePattern.SpacedPascalCase] = "Spaced Pascal Case"
        };

        /// <summary>
        /// シングルトンのインスタンスです。
        /// </summary>
        public static readonly StringCasePatternConverter Instance = new StringCasePatternConverter();

        /// <summary>
        /// インスタンスを初期化します。
        /// </summary>
        private StringCasePatternConverter() : base(typeof(StringCasePattern))
        {
        }

        /// <inheritdoc />
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            var key = value is StringCasePattern ? (StringCasePattern)value : StringCasePattern.CamelCase;
            return ConvertTo(key);
        }

        /// <summary>
        /// 指定の<see cref="StringCasePattern"/>を文字列に変換します。
        /// </summary>
        /// <param name="value">変換する値</param>
        /// <returns>変換した値</returns>
        public string ConvertTo(StringCasePattern value)
        {
            string result;
            if (_names.TryGetValue(value, out result))
            {
                return result;
            }
            else
            {
                throw new InvalidOperationException($"This item [{value}] is not supported");
            }
        }

        /// <inheritdoc />
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            return ConvertFrom(value as string);
        }

        /// <summary>
        /// 指定の文字列を<see cref="StringCasePattern"/>に変換します。
        /// </summary>
        /// <param name="value">変換する値</param>
        /// <returns>変換した値</returns>
        public StringCasePattern ConvertFrom(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return StringCasePattern.CamelCase;
            }

            return _names.FirstOrDefault(x => x.Value == value).Key;
        }
    }
}
