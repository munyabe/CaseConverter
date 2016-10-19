using System.ComponentModel;
using CaseConverter.Converters;

namespace CaseConverter.Options
{
    /// <summary>
    /// 文字列の変換パターンのオプションです。
    /// </summary>
    /// <remarks>
    /// オプション画面での表記をカスタマイズするために定義したクラスです。
    /// </remarks>
    public class PatternOption
    {
        /// <summary>
        /// 文字列の変換パターンを取得または設定します。
        /// </summary>
        [Category("Basic")]
        [DisplayName("Pattern")]
        [TypeConverter(typeof(StringCasePatternConverter))]
        public StringCasePattern Pattern { get; set; }

        /// <inheritdoc />
        /// <remarks>
        /// オプション画面の表記を変更するためにオーバーライドします。
        /// </remarks>
        public override string ToString()
        {
            return StringCasePatternConverter.Instance.ConvertTo(Pattern);
        }
    }
}
