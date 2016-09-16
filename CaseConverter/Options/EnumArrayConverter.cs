using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace CaseConverter.Options
{
    /// <summary>
    /// 列挙体の配列を1つの文字列に変換するコンバーターです。
    /// </summary>
    /// <typeparam name="TEnum">対象の列挙体の型</typeparam>
    public class EnumArrayConverter<TEnum> : ArrayConverter where TEnum : struct
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
            else
            {
                return source.Split(new[] { SEPARATOR }, StringSplitOptions.None)
                    .Select(ParseOrDefault)
                    .ToArray();
            }
        }

        /// <inheritdoc />
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            var array = value as TEnum[];
            if (array != null)
            {
                return string.Join(SEPARATOR, array);
            }
            else
            {
                return base.ConvertTo(context, culture, value, destinationType);
            }
        }

        /// <summary>
        /// 指定の文字列を列挙体に変換できればその値を、できない場合はデフォルト値を返します。
        /// </summary>
        private static TEnum ParseOrDefault(string value)
        {
            TEnum result;
            return Enum.TryParse(value, out result) ? result : default(TEnum);
        }
    }
}
