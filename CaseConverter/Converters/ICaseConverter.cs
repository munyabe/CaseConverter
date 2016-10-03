using System.Collections.Generic;

namespace CaseConverter.Converters
{
    /// <summary>
    /// 文字列を変換する機能を提供します。
    /// </summary>
    public interface ICaseConverter
    {
        /// <summary>
        /// 単語を文字列に連結します。
        /// </summary>
        /// <remarks>連結した文字列</remarks>
        string Convert(IEnumerable<string> words);
    }
}
