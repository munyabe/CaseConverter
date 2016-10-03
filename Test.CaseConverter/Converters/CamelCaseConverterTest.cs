using System;
using CaseConverter.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.CaseConverter.Converters
{
    /// <summary>
    /// <see cref="CamelCaseConverter"/>のテストクラスです。
    /// </summary>
    [TestClass]
    public class CamelCaseConverterTest
    {
        [TestMethod]
        public void ConvertTest()
        {
            var converter = new CamelCaseConverter();
            Action<string, string[]> assert = (expected, source) =>
                Assert.AreEqual(expected, converter.Convert(source));

            assert("hogeFugaPiyo", new[] { "hoge", "fuga", "piyo" });
            assert("hogeFugaPiyo", new[] { "HOGE", "fuga", "piyo" });

            assert("hoge", new[] { "hoge" });
            assert("hoge", new[] { "HOGE" });
        }
    }
}
