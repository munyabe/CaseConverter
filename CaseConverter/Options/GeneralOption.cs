using System.Collections.Generic;
using System.ComponentModel;

namespace CaseConverter.Options
{
    /// <summary>
    /// 全般設定のオプションです。
    /// </summary>
    public class GeneralOption
    {
        /// <summary>
        /// キャメルケースを有効にするかどうかを取得または設定します。
        /// </summary>
        [Category("Conversion Pattern")]
        [DisplayName("Enable Camel Case")]
        public bool EnableCamelCase { get; set; } = true;

        /// <summary>
        /// パスカルケースを有効にするかどうかを取得または設定します。
        /// </summary>
        [Category("Conversion Pattern")]
        [DisplayName("Enable Pascal Case")]
        public bool EnablePascalCase { get; set; } = true;

        /// <summary>
        /// スネークケースを有効にするかどうかを取得または設定します。
        /// </summary>
        [Category("Conversion Pattern")]
        [DisplayName("Enable Snake Case")]
        public bool EnableSnakeCase { get; set; } = true;

        /// <summary>
        /// 文字列の変換パターンを取得します。
        /// </summary>
        /// <returns>文字列の変換パターン</returns>
        public IList<StringCasePattern> GetPatterns()
        {
            var result = new List<StringCasePattern>();

            if (EnableCamelCase)
            {
                result.Add(StringCasePattern.CamelCase);
            }
            if (EnablePascalCase)
            {
                result.Add(StringCasePattern.PascalCase);
            }
            if (EnableSnakeCase)
            {
                result.Add(StringCasePattern.SnakeCase);
            }

            return result;
        }
    }
}
