using System;
using CaseConverter.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.CaseConverter.Converters
{
    /// <summary>
    /// <see cref="SnakeCaseConverter"/>のテストクラスです。
    /// </summary>
    [TestClass]
    public class SnakeCaseConverterTest
    {
        [TestMethod]
        public void ConvertTest()
        {
            var converter = new SnakeCaseConverter();
            Action<string, string[]> assert = (expected, source) =>
                Assert.AreEqual(expected, converter.Convert(source));

            assert("hoge_fuga_piyo", new[] { "hoge", "fuga", "piyo" });
            assert("hoge_fuga_piyo", new[] { "HOGE", "fuga", "piyo" });

            assert("hoge", new[] { "hoge" });
            assert("hoge", new[] { "HOGE" });
        }
    }
}
