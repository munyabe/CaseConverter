using System.ComponentModel;

namespace CaseConverter.Options
{
    /// <summary>
    /// 全般設定のオプションです。
    /// </summary>
    public class GeneralOption
    {
        /// <summary>
        /// 文字列の変換パターンを取得または設定します。
        /// </summary>
        [Category("Basic")]
        [DisplayName("Conversion Pattern")]
        [Description("This is an order to convert a string.")]
        [TypeConverter(typeof(StringCasePatternArrayConverter))]
        public StringCasePattern[] Patterns { get; set; } =
            new[]
            {
                StringCasePattern.SnakeCase,
                StringCasePattern.CamelCase,
                StringCasePattern.PascalCase
            };
    }
}
