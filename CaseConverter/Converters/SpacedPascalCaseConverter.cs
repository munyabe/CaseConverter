using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using CaseConverter.Utils;

namespace CaseConverter.Converters
{
    /// <summary>
    /// 文字列をスネークケースに変換するクラスです。
    /// </summary>
    public class SpacedPascalCaseConverter : ICaseConverter
    {
        /// <inheritdoc />
        public string Convert(IEnumerable<string> words)
        {
            if (words == null)
            {
                return string.Empty;
            }

            return string.Join(" ", words.Select(StringUtil.ToFirstUpper));
        }
    }
}
