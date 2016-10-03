using System.Collections.Generic;
using System.Text;

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
                result.Append(char.ToUpper(word[0]) + word.Substring(1, word.Length - 1).ToLower());
            }

            return result.ToString();
        }
    }
}
