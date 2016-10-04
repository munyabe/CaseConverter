using System;
using CaseConverter.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.CaseConverter.Converters
{
    /// <summary>
    /// <see cref="ScreamingSnakeCaseConverter"/>のテストクラスです。
    /// </summary>
    [TestClass]
    public class ScreamingSnakeCaseConverterTest
    {
        [TestMethod]
        public void ConvertTest()
        {
            var converter = new ScreamingSnakeCaseConverter();
            Action<string, string[]> assert = (expected, source) =>
                Assert.AreEqual(expected, converter.Convert(source));

            assert("HOGE_FUGA_PIYO", new[] { "hoge", "fuga", "piyo" });
            assert("HOGE_FUGA_PIYO", new[] { "HOGE", "fuga", "piyo" });

            assert("HOGE", new[] { "hoge" });
            assert("HOGE", new[] { "HOGE" });
        }
    }
}
