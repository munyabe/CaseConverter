using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace CaseConverter.Converters
{
    /// <summary>
    /// 文字列を、ハイフンで単語を区切った小文字のケースに変換するクラスです。
    /// </summary>
    public class KebabCaseConverter : ICaseConverter
    {
        /// <inheritdoc />
        public string Convert(IEnumerable<string> words)
        {
            return string.Join("-", words.Select(CultureInfo.CurrentCulture.TextInfo.ToLower));
        }
    }
}
