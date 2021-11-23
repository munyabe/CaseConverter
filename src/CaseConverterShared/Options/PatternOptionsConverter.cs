﻿using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using CaseConverter.Converters;

namespace CaseConverter.Options
{
    /// <summary>
    /// <see cref="PatternOption"/>の配列を1つの文字列に変換するコンバーターです。
    /// </summary>
    public class PatternOptionsConverter : ArrayConverter
    {
        /// <summary>
        /// 複数の列挙体を分けるセパレーターです。
        /// </summary>
        private const string SEPARATOR = ", ";

        /// <inheritdoc />
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
        }

        /// <inheritdoc />
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof(string) || base.CanConvertTo(context, destinationType);
        }

        /// <inheritdoc />
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            var source = value as string;
            if (string.IsNullOrEmpty(source))
            {
                return base.ConvertFrom(context, culture, value);
            }

            return source.Split(new[] { SEPARATOR }, StringSplitOptions.None)
                .Select(x => ParseOrDefault<StringCasePattern>(x))
                .Select(x => new PatternOption { Pattern = x })
                .ToArray();
        }

        /// <inheritdoc />
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            var array = value as PatternOption[];
            if (array == null)
            {
                return base.ConvertTo(context, culture, value, destinationType);
            }

            return string.Join(SEPARATOR, array.Select(x => x.Pattern));
        }

        /// <summary>
        /// 指定の文字列を列挙体に変換できればその値を、できない場合はデフォルト値を返します。
        /// </summary>
        private static TEnum ParseOrDefault<TEnum>(string value) where TEnum : struct
        {
            TEnum result;
            return Enum.TryParse(value, out result) ? result : default(TEnum);
        }
    }
}
