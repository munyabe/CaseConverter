using System.Collections.Generic;
using System.Text;

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
            var result = new StringBuilder();
            var isFirst = true;
            foreach (var word in words)
            {
                var top = isFirst ? char.ToLower(word[0]) : char.ToUpper(word[0]);
                isFirst = false;

                result.Append(top + word.Substring(1, word.Length - 1).ToLower());
            }

            return result.ToString();
        }
    }
}
