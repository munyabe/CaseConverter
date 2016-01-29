using System;
using System.Linq;
using CaseConverter;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.CaseConverter
{
    /// <summary>
    /// <see cref="StringCaseConverter"/>のテストクラスです。
    /// </summary>
    [TestClass]
    public class StringCaseConverterTest
    {
        [TestMethod]
        public void ConvertTest()
        {
            Action<string, string> assert = (expected, source) =>
                Assert.AreEqual(expected, StringCaseConverter.Convert(source));

            assert("hogeFugaPiyo", "hoge_fuga_piyo");
            assert("HogeFugaPiyo", "hogeFugaPiyo");
            assert("hoge_fuga_piyo", "HogeFugaPiyo");

            assert(null, null);
            assert(string.Empty, string.Empty);
            assert(" ", " ");
            assert("123", "123");
        }

        [TestMethod]
        public void GetWordsFromCamelCaseTest()
        {
            Action<string[], string> assert = (expected, source) =>
                CollectionAssert.AreEquivalent(expected, StringCaseConverter.GetWordsFromCamelCase(source).ToArray());

            assert(new[] { "Hoge", "Fuga", "Piyo" }, "HogeFugaPiyo");
            assert(new[] { "hoge", "Fuga", "Piyo" }, "hogeFugaPiyo");
            assert(new[] { "HOGE", "Fuga", "Piyo" }, "HOGEFugaPiyo");
            assert(new[] { "Hoge", "Fuga", "PIYO" }, "HogeFugaPIYO");
            assert(new[] { "Hoge", "Fuga", "P" }, "HogeFugaP");

            // MEMO : 数字を含んだテスト
            assert(new[] { "Hoge", "Fu1ga", "Piyo" }, "HogeFu1gaPiyo");
            assert(new[] { "ho1ge2", "Fuga", "Piyo" }, "ho1ge2FugaPiyo");
            assert(new[] { "Hoge", "Fuga1", "PI2YO3" }, "HogeFuga1PI2YO3");

            // MEMO : 1単語のテスト
            assert(new[] { "hoge" }, "hoge");
            assert(new[] { "HOGE" }, "HOGE");
            assert(new[] { "ho1ge2" }, "ho1ge2");
            assert(new[] { "1hoge" }, "1hoge");
            assert(new[] { "1", "HOGE" }, "1HOGE");
        }

        [TestMethod]
        public void GetWordsFromSnakeCaseTest()
        {
            Action<string[], string> assert = (expected, source) =>
                CollectionAssert.AreEquivalent(expected, StringCaseConverter.GetWordsFromSnakeCase(source).ToArray());

            assert(new[] { "hoge", "fuga", "piyo" }, "hoge_fuga_piyo");
            assert(new[] { "HOGE", "FUGA", "PIYO" }, "HOGE_FUGA_PIYO");

            assert(new[] { "hoge", "f", "piyo" }, "hoge_f_piyo");
            assert(new[] { "hoge", "F", "piyo" }, "hoge_F_piyo");

            assert(new[] { "hoge" }, "hoge");
            assert(new[] { "HOGE" }, "HOGE");
        }

        [TestMethod]
        public void ToCamelCaseTest()
        {
            Action<string, string[]> assert = (expected, source) =>
                Assert.AreEqual(expected, source.ToCamelCase());

            assert("hogeFugaPiyo", new[] { "hoge", "fuga", "piyo" });
            assert("hogeFugaPiyo", new[] { "HOGE", "fuga", "piyo" });

            assert("hoge", new[] { "hoge" });
            assert("hoge", new[] { "HOGE" });
        }

        [TestMethod]
        public void ToPascalCaseTest()
        {
            Action<string, string[]> assert = (expected, source) =>
                Assert.AreEqual(expected, source.ToPascalCase());

            assert("HogeFugaPiyo", new[] { "hoge", "fuga", "piyo" });
            assert("HogeFugaPiyo", new[] { "HOGE", "fuga", "piyo" });

            assert("Hoge", new[] { "hoge" });
            assert("Hoge", new[] { "HOGE" });
        }

        [TestMethod]
        public void ToSnakeCaseTest()
        {
            Action<string, string[]> assert = (expected, source) =>
                Assert.AreEqual(expected, source.ToSnakeCase());

            assert("hoge_fuga_piyo", new[] { "hoge", "fuga", "piyo" });
            assert("hoge_fuga_piyo", new[] { "HOGE", "fuga", "piyo" });

            assert("hoge", new[] { "hoge" });
            assert("hoge", new[] { "HOGE" });
        }
    }
}
