using System.ComponentModel;
using CaseConverter.Converters;

namespace CaseConverter.Options
{
    /// <summary>
    /// <see cref="StringCasePattern"/>の配列を1つの文字列に変換するコンバーターです。
    /// </summary>
    /// <remarks>
    /// <see cref="TypeConverter"/>がジェネリッククラスを受け付けないために、新しくクラスを定義しています。
    /// </remarks>
    public class StringCasePatternArrayConverter : EnumArrayConverter<StringCasePattern>
    {
    }
}
