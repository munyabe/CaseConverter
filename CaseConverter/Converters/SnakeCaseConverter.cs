using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace CaseConverter.Converters
{
    /// <summary>
    /// 文字列をスネークケースに変換するクラスです。
    /// </summary>
    public class SnakeCaseConverter : ICaseConverter
    {
        /// <inheritdoc />
        public string Convert(IEnumerable<string> words)
        {
            return string.Join("_", words.Select(CultureInfo.CurrentCulture.TextInfo.ToLower));
        }
    }
}
