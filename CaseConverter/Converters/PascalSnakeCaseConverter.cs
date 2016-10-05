using System.Collections.Generic;
using System.Linq;
using CaseConverter.Utils;

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
            return string.Join("_", words.Select(StringUtil.ToFirstUpper));
        }
    }
}
