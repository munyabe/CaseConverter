using System.Collections.Generic;
using System.Linq;

namespace CaseConverter.Converters
{
    /// <summary>
    /// 文字列を、単語の先頭のみ大文字にしたスネークケースに変換するクラスです。
    /// </summary>
    public class PascalSnakeCaseConverter : ICaseConverter
    {
        /// <inheritdoc />
        public string Convert(IEnumerable<string> words)
        {
            var camelWords = words.Select(word => char.ToUpper(word[0]) + word.Substring(1, word.Length - 1).ToLower());
            return string.Join("_", camelWords);
        }
    }
}
