using System;
using CaseConverter.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.CaseConverter.Converters
{
    /// <summary>
    /// <see cref="PascalSnakeCaseConverter"/>のテストクラスです。
    /// </summary>
    [TestClass]
    public class PascalSnakeCaseConverterTest
    {
        [TestMethod]
        public void ConvertTest()
        {
            var converter = new PascalSnakeCaseConverter();
            Action<string, string[]> assert = (expected, source) =>
                Assert.AreEqual(expected, converter.Convert(source));

            assert("Hoge_Fuga_Piyo", new[] { "hoge", "fuga", "piyo" });
            assert("Hoge_Fuga_Piyo", new[] { "HOGE", "fuga", "piyo" });

            assert("Hoge", new[] { "hoge" });
            assert("Hoge", new[] { "HOGE" });
        }
    }
}
