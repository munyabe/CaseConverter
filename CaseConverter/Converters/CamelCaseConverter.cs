using System.Collections.Generic;
using System.Globalization;
using System.Text;
using CaseConverter.Utils;

namespace CaseConverter.Converters
{
    /// <summary>
    /// 文字列をキャメルケースに変換するクラスです。
    /// </summary>
    public class CamelCaseConverter : ICaseConverter
    {
        /// <inheritdoc />
        public string Convert(IEnumerable<string> words)
        {
            if (words == null)
            {
                return string.Empty;
            }

            var result = new StringBuilder();
            var isFirst = true;
            foreach (var word in words)
            {
                result.Append(isFirst ? CultureInfo.CurrentCulture.TextInfo.ToLower(word) : StringUtil.ToFirstUpper(word));
                isFirst = false;
            }

            return result.ToString();
        }
    }
}
