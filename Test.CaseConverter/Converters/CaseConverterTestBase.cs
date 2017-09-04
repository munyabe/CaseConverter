using System;
using CaseConverter.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.CaseConverter.Converters
{
    /// <summary>
    /// <see cref="ICaseConverter"/>の実装クラスをテストする抽象クラスです。
    /// </summary>
    /// <typeparam name="TConverter">テスト対象のコンバーターの型</typeparam>
    public abstract class CaseConverterTestBase<TConverter> where TConverter : ICaseConverter, new()
    {
        /// <summary>
        /// <see cref="ICaseConverter.Convert"/>をテストします。
        /// </summary>
        protected void ConvertTest(string multiWords, string singleWord, string singleCharacter)
        {
            using (new CultureInfoContext("en-US"))
            {
                var converter = new TConverter();
                Action<string, string[]> assert = (expected, source) =>
                    Assert.AreEqual(expected, converter.Convert(source));

                assert(multiWords, new[] { "hoge", "fuga", "piyo" });
                assert(multiWords, new[] { "HOGE", "fuga", "piyo" });

                assert(singleWord, new[] { "hoge" });
                assert(singleWord, new[] { "HOGE" });

                assert(singleCharacter, new[] { "h" });
                assert(singleCharacter, new[] { "H" });

                assert(string.Empty, null);
                assert(string.Empty, new string[0]);
            }
        }

        /// <summary>
        /// <see cref="ICaseConverter.Convert"/>をテストします。
        /// </summary>
        protected void ConvertTestTR(string multiWords, string singleWord, string singleCharacter)
        {
            using (new CultureInfoContext("tr-TR"))
            {
                var converter = new TConverter();
                Action<string, string[]> assert = (expected, source) =>
                    Assert.AreEqual(expected, converter.Convert(source));

                assert(multiWords, new[] { "saçım", "şekil", "önümden", "çekil" });
                assert(multiWords, new[] { "SAÇIM", "ŞEKİL", "ÖNÜMDEN", "çekil" });

                assert(singleWord, new[] { "şekil" });
                assert(singleWord, new[] { "ŞEKİL" });

                assert(singleCharacter, new[] { "i" });
                assert(singleCharacter, new[] { "İ" });

                assert(string.Empty, null);
                assert(string.Empty, new string[0]);
            }
        }
    }
}
