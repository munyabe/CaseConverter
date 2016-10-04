namespace CaseConverter.Converters
{
    /// <summary>
    /// 複合の単語をひと綴りで表す場合のパターンを表します。
    /// </summary>
    public enum StringCasePattern
    {
        /// <summary>
        /// キャメルケースです。
        /// </summary>
        CamelCase,

        /// <summary>
        /// パスカルケースです。
        /// </summary>
        PascalCase,

        /// <summary>
        /// スネークケースです。
        /// </summary>
        SnakeCase,

        /// <summary>
        /// スクリーミングスネークケースです。
        /// </summary>
        ScreamingSnakeCase
    }
}
