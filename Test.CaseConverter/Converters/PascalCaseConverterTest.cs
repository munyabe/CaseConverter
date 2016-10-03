using System;
using CaseConverter.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.CaseConverter.Converters
{
    /// <summary>
    /// <see cref="PascalCaseConverter"/>のテストクラスです。
    /// </summary>
    [TestClass]
    public class PascalCaseConverterTest
    {
        [TestMethod]
        public void ConvertTest()
        {
            var converter = new PascalCaseConverter();
            Action<string, string[]> assert = (expected, source) =>
                Assert.AreEqual(expected, converter.Convert(source));

            assert("HogeFugaPiyo", new[] { "hoge", "fuga", "piyo" });
            assert("HogeFugaPiyo", new[] { "HOGE", "fuga", "piyo" });

            assert("Hoge", new[] { "hoge" });
            assert("Hoge", new[] { "HOGE" });
        }
    }
}
