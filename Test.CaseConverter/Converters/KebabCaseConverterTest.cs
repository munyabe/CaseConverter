using System;
using CaseConverter.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.CaseConverter.Converters
{
    /// <summary>
    /// <see cref="KebabCaseConverter"/>のテストクラスです。
    /// </summary>
    [TestClass]
    public class KebabCaseConverterTest
    {
        [TestMethod]
        public void ConvertTest()
        {
            var converter = new KebabCaseConverter();
            Action<string, string[]> assert = (expected, source) =>
                Assert.AreEqual(expected, converter.Convert(source));

            assert("hoge-fuga-piyo", new[] { "hoge", "fuga", "piyo" });
            assert("hoge-fuga-piyo", new[] { "HOGE", "fuga", "piyo" });

            assert("hoge", new[] { "hoge" });
            assert("hoge", new[] { "HOGE" });
        }
    }
}
