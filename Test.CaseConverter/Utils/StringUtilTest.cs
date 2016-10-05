using System;
using CaseConverter.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.CaseConverter.Utils
{
    /// <summary>
    /// <see cref="StringUtil"/>のテストクラスです。
    /// </summary>
    [TestClass]
    public class StringUtilTest
    {
        [TestMethod]
        public void ToFirstUpperTest()
        {
            Action<string, string> assert = (expected, source) =>
                Assert.AreEqual(expected, StringUtil.ToFirstUpper(source));

            assert("Hoge", "hoge");
            assert("Hoge", "HOGE");
            assert("Hoge", "hOGE");
            assert("H", "h");

            assert("Hoge fuga", "hoge Fuga");
            assert(" hoge", " hoge");

            assert(null, null);
            assert(string.Empty, string.Empty);
        }
    }
}
