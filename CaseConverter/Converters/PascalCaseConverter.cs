using System.Collections.Generic;
using System.Text;
using CaseConverter.Utils;

namespace CaseConverter.Converters
{
    /// <summary>
    /// 文字列をパスカルケースに変換するクラスです。
    /// </summary>
    public class PascalCaseConverter : ICaseConverter
    {
        /// <inheritdoc />
        public string Convert(IEnumerable<string> words)
        {
            var result = new StringBuilder();
            foreach (var word in words)
            {
                result.Append(StringUtil.ToFirstUpper(word));
            }

            return result.ToString();
        }
    }
}
