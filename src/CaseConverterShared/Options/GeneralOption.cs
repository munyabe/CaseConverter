using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using CaseConverter.Converters;

namespace CaseConverter.Options
{
    /// <summary>
    /// 全般設定のオプションです。
    /// </summary>
    public class GeneralOption
    {
        /// <summary>
        /// 文字列の変換パターンのオプションを取得または設定します。
        /// </summary>
        [Category("Basic")]
        [DisplayName("Conversion Pattern")]
        [Description("This is an order to convert a string.")]
        [TypeConverter(typeof(PatternOptionsConverter))]
        public PatternOption[] PatternOptions { get; set; } =
            new[]
            {
                new PatternOption { Pattern = StringCasePattern.SnakeCase },
                new PatternOption { Pattern = StringCasePattern.CamelCase },
                new PatternOption { Pattern = StringCasePattern.PascalCase }
            };

        /// <summary>
        /// 文字列の変換パターンを取得または設定します。
        /// </summary>
        [Browsable(false)]
        public IEnumerable<StringCasePattern> Patterns => PatternOptions.Select(x => x.Pattern);
    }
}
