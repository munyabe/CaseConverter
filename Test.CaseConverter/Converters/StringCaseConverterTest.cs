using System;
using System.Collections.Generic;
using System.Linq;
using CaseConverter.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.CaseConverter.Converters
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
            using (new CultureInfoContext("en-US"))
            {
                var convertPatterns = new List<StringCasePattern>
                {
                    StringCasePattern.CamelCase,
                    StringCasePattern.PascalCase,
                    StringCasePattern.SnakeCase
                };

                Action<string, string> assert = (expected, source) =>
                    Assert.AreEqual(expected, StringCaseConverter.Convert(source, convertPatterns));

                assert("hogeFugaPiyo", "hoge_fuga_piyo");
                assert("HogeFugaPiyo", "hogeFugaPiyo");
                assert("hoge_fuga_piyo", "HogeFugaPiyo");

                assert(null, null);
                assert(string.Empty, string.Empty);
                assert(" ", " ");
                assert("123", "123");
                assert("+-*", "+-*");

                Assert.AreEqual("hoge_fuga_piyo", StringCaseConverter.Convert("hoge_fuga_piyo", null));
                Assert.AreEqual("hoge_fuga_piyo", StringCaseConverter.Convert("hoge_fuga_piyo", new List<StringCasePattern>()));
            }
        }

        [TestMethod]
        public void ConvertTestTR()
        {
            using (new CultureInfoContext("tr-TR"))
            {
                var convertPatterns = new List<StringCasePattern>
                {
                    StringCasePattern.CamelCase,
                    StringCasePattern.PascalCase,
                    StringCasePattern.SnakeCase
                };

                Action<string, string> assert = (expected, source) =>
                    Assert.AreEqual(expected, StringCaseConverter.Convert(source, convertPatterns));

                assert("saçımŞekilÖnümdenÇekil", "saçım_şekil_önümden_çekil");
                assert("SaçımŞekilÖnümdenÇekil", "saçımŞekilÖnümdenÇekil");
                assert("saçım_şekil_önümden_çekil", "SaçımŞekilÖnümdenÇekil");

                assert(null, null);
                assert(string.Empty, string.Empty);
                assert(" ", " ");
                assert("123", "123");
                assert("+-*", "+-*");

                Assert.AreEqual("saçım_şekil_önümden_çekil", StringCaseConverter.Convert("saçım_şekil_önümden_çekil", null));
                Assert.AreEqual("saçım_şekil_önümden_çekil", StringCaseConverter.Convert("saçım_şekil_önümden_çekil", new List<StringCasePattern>()));
            }
        }

        [TestMethod]
        public void GetCasePatternTest()
        {
            using (new CultureInfoContext("en-US"))
            {
                Action<StringCasePattern, string> assert = (expected, source) =>
                    Assert.AreEqual(expected, StringCaseConverter.GetCasePattern(source));

                assert(StringCasePattern.CamelCase, "hogeFugaPiyo");
                assert(StringCasePattern.PascalCase, "HogeFugaPiyo");
                assert(StringCasePattern.SnakeCase, "hoge_fuga_piyo");
                assert(StringCasePattern.PascalSnakeCase, "Hoge_Fuga_Piyo");
                assert(StringCasePattern.ScreamingSnakeCase, "HOGE_FUGA_PIYO");
                assert(StringCasePattern.KebabCase, "hoge-fuga-piyo");

                assert(StringCasePattern.CamelCase, "hoge");
                assert(StringCasePattern.PascalCase, "Hoge");
                assert(StringCasePattern.ScreamingSnakeCase, "HOGE");

                assert(StringCasePattern.CamelCase, "h");
                assert(StringCasePattern.PascalCase, "H");

                assert(StringCasePattern.SnakeCase, "_");
                assert(StringCasePattern.KebabCase, "-");
                assert(StringCasePattern.CamelCase, "=");

                assert(StringCasePattern.SnakeCase, "h_");
                assert(StringCasePattern.SnakeCase, "h_H");
                assert(StringCasePattern.PascalSnakeCase, "Ho_");
                assert(StringCasePattern.ScreamingSnakeCase, "H_");
                assert(StringCasePattern.KebabCase, "H-");

                assert(StringCasePattern.SnakeCase, "_h_");
                assert(StringCasePattern.SnakeCase, "_Ho_");
                assert(StringCasePattern.ScreamingSnakeCase, "_H_");
                assert(StringCasePattern.SnakeCase, "_h-");

                assert(StringCasePattern.CamelCase, string.Empty);
            }
        }

        [TestMethod]
        public void GetCasePatternTestTR()
        {
            using (new CultureInfoContext("tr-TR"))
            {
                Action<StringCasePattern, string> assert = (expected, source) =>
                    Assert.AreEqual(expected, StringCaseConverter.GetCasePattern(source));

                assert(StringCasePattern.CamelCase, "saçımŞekilÖnümdenÇekil");
                assert(StringCasePattern.PascalCase, "SaçımŞekilÖnümdenÇekil");
                assert(StringCasePattern.SnakeCase, "saçım_şekil_önümden_çekil");
                assert(StringCasePattern.PascalSnakeCase, "Saçım_Şekil_Önümden_Çekil");
                assert(StringCasePattern.ScreamingSnakeCase, "SAÇIM_ŞEKİL_ÖNÜMDEN_ÇEKİL");
                assert(StringCasePattern.KebabCase, "saçım-şekil-önümden-çekil");

                assert(StringCasePattern.CamelCase, "şekil");
                assert(StringCasePattern.PascalCase, "Şekil");
                assert(StringCasePattern.ScreamingSnakeCase, "ŞEKİL");

                assert(StringCasePattern.CamelCase, "i");
                assert(StringCasePattern.PascalCase, "İ");

                assert(StringCasePattern.SnakeCase, "_");
                assert(StringCasePattern.KebabCase, "-");
                assert(StringCasePattern.CamelCase, "=");

                assert(StringCasePattern.SnakeCase, "h_");
                assert(StringCasePattern.SnakeCase, "i_Ş");
                assert(StringCasePattern.PascalSnakeCase, "Şi_");
                assert(StringCasePattern.ScreamingSnakeCase, "Ş_");
                assert(StringCasePattern.KebabCase, "Ş-");

                assert(StringCasePattern.SnakeCase, "_i_");
                assert(StringCasePattern.SnakeCase, "_Şi_");
                assert(StringCasePattern.ScreamingSnakeCase, "_İ_");
                assert(StringCasePattern.SnakeCase, "_i-");

                assert(StringCasePattern.CamelCase, string.Empty);
            }
        }

        [TestMethod]
        public void GetWordsTest()
        {
            using (new CultureInfoContext("en-US"))
            {
                Action<string[], string> assert = (expected, source) =>
                    CollectionAssert.AreEquivalent(expected, StringCaseConverter.GetWords(source).ToArray());

                assert(new[] { "Hoge", "Fuga", "Piyo" }, "HogeFugaPiyo");
                assert(new[] { "hoge", "Fuga", "Piyo" }, "hogeFugaPiyo");
                assert(new[] { "HOGE", "Fuga", "Piyo" }, "HOGEFugaPiyo");
                assert(new[] { "Hoge", "Fuga", "PIYO" }, "HogeFugaPIYO");
                assert(new[] { "Hoge", "Fuga", "P" }, "HogeFugaP");

                // MEMO : 数字を含んだテスト
                assert(new[] { "Hoge", "Fu1ga", "Piyo" }, "HogeFu1gaPiyo");
                assert(new[] { "ho1ge2", "Fuga", "Piyo" }, "ho1ge2FugaPiyo");
                assert(new[] { "Hoge", "Fuga1", "PI2YO3" }, "HogeFuga1PI2YO3");
                assert(new[] { "Hoge", "FU1GA", "Piyo" }, "HogeFU1GAPiyo");
                assert(new[] { "123" }, "123");

                // MEMO : 1単語のテスト
                assert(new[] { "hoge" }, "hoge");
                assert(new[] { "HOGE" }, "HOGE");
                assert(new[] { "ho1ge2" }, "ho1ge2");
                assert(new[] { "1hoge" }, "1hoge");
                assert(new[] { "1", "HOGE" }, "1HOGE");

                // MEMO : 空白を含んだテスト
                assert(new[] { "Hoge", "Fuga", "Piyo" }, "Hoge Fuga Piyo");
                assert(new[] { "Hoge", "Fuga", "Piyo" }, "Hoge FugaPiyo");
                assert(new[] { "hoge", "fuga", "Piyo" }, "hoge fuga Piyo");
                assert(new[] { "hoge", "fuga", "Piyo" }, "hoge fugaPiyo");

                // MEMO : スネークケースのテスト
                assert(new[] { "hoge", "fuga", "piyo" }, "hoge_fuga_piyo");
                assert(new[] { "HOGE", "FUGA", "PIYO" }, "HOGE_FUGA_PIYO");
                assert(new[] { "hoge", "f", "piyo" }, "hoge_f_piyo");
                assert(new[] { "hoge", "F", "piyo" }, "hoge_F_piyo");
                assert(new[] { "HOGE", "1", "FUGA", "PIYO" }, "HOGE_1FUGA_PIYO");
                assert(new[] { "HOGE", "FU1GA", "PIYO" }, "HOGE_FU1GA_PIYO");
                assert(new[] { "HOGE", "FUGA1", "PIYO" }, "HOGE_FUGA1_PIYO");

                // MEMO : 記号を含んだテスト
                assert(new[] { "Hoge", "Fuga", "Piyo" }, "Hoge+Fuga-Piyo");
                assert(new[] { "Hoge", "Fuga", "Piyo" }, "Hoge+Fuga-Piyo*");
                assert(new[] { "Hoge", "Fuga", "Piyo" }, "+Hoge-Fuga*Piyo");
                assert(new[] { "Hoge", "Fuga", "Piyo" }, "Hoge+-Fuga**Piyo");

                // MEMO : 単語がない場合のテスト
                assert(new string[0], null);
                assert(new string[0], string.Empty);
                assert(new string[0], " ");
                assert(new string[0], "+-*");
            }
        }

        [TestMethod]
        public void GetWordsTestTR()
        {
            using (new CultureInfoContext("tr-TR"))
            {
                Action<string[], string> assert = (expected, source) =>
                    CollectionAssert.AreEquivalent(expected, StringCaseConverter.GetWords(source).ToArray());

                assert(new[] { "Saçım", "Şekil", "Önümden", "Çekil" }, "SaçımŞekilÖnümdenÇekil");
                assert(new[] { "saçım", "Şekil", "Önümden", "Çekil" }, "saçımŞekilÖnümdenÇekil");
                assert(new[] { "SAÇIM", "Şekil", "Önümden", "Çekil" }, "SAÇIMŞekilÖnümdenÇekil");
                assert(new[] { "Saçım", "Şekil", "Önümden", "ÇEKİL" }, "SaçımŞekilÖnümdenÇEKİL");
                assert(new[] { "Saçım", "Şekil", "Önümden", "Ç" }, "SaçımŞekilÖnümdenÇ");

                // MEMO : 数字を含んだテスト
                assert(new[] { "Saçım", "Şe1kil", "Önümden" }, "SaçımŞe1kilÖnümden");
                assert(new[] { "sa1çım2", "Şekil", "Önümden" }, "sa1çım2ŞekilÖnümden");
                assert(new[] { "Saçım", "Şekil1", "ÖN2ÜMDEN3" }, "SaçımŞekil1ÖN2ÜMDEN3");
                assert(new[] { "Saçım", "ŞE1KİL", "Önümden" }, "SaçımŞE1KİLÖnümden");
                assert(new[] { "123" }, "123");

                // MEMO : 1単語のテスト
                assert(new[] { "şekil" }, "şekil");
                assert(new[] { "ŞEKİL" }, "ŞEKİL");
                assert(new[] { "şe1kil2" }, "şe1kil2");
                assert(new[] { "1şekil" }, "1şekil");
                assert(new[] { "1", "ŞEKİL" }, "1ŞEKİL");

                // MEMO : 空白を含んだテスト
                assert(new[] { "Saçım", "Şekil", "Önümden" }, "Saçım Şekil Önümden");
                assert(new[] { "Saçım", "Şekil", "Önümden" }, "Saçım ŞekilÖnümden");
                assert(new[] { "saçım", "şekil", "Önümden" }, "saçım şekil Önümden");
                assert(new[] { "saçım", "şekil", "Önümden" }, "saçım şekilÖnümden");

                // MEMO : スネークケースのテスト
                assert(new[] { "saçım", "şekil", "önümden" }, "saçım_şekil_önümden");
                assert(new[] { "SAÇIM", "ŞEKİL", "ÖNÜMDEN" }, "SAÇIM_ŞEKİL_ÖNÜMDEN");
                assert(new[] { "saçım", "ş", "önümden" }, "saçım_ş_önümden");
                assert(new[] { "saçım", "Ş", "önümden" }, "saçım_Ş_önümden");
                assert(new[] { "SAÇIM", "1", "ŞEKİL", "ÖNÜMDEN" }, "SAÇIM_1ŞEKİL_ÖNÜMDEN");
                assert(new[] { "SAÇIM", "ŞE1KİL", "ÖNÜMDEN" }, "SAÇIM_ŞE1KİL_ÖNÜMDEN");
                assert(new[] { "SAÇIM", "ŞEKİL1", "ÖNÜMDEN" }, "SAÇIM_ŞEKİL1_ÖNÜMDEN");

                // MEMO : 記号を含んだテスト
                assert(new[] { "Saçım", "Şekil", "Önümden" }, "Saçım+Şekil-Önümden");
                assert(new[] { "Saçım", "Şekil", "Önümden" }, "Saçım+Şekil-Önümden*");
                assert(new[] { "Saçım", "Şekil", "Önümden" }, "+Saçım-Şekil*Önümden");
                assert(new[] { "Saçım", "Şekil", "Önümden" }, "Saçım+-Şekil**Önümden");

                // MEMO : 単語がない場合のテスト
                assert(new string[0], null);
                assert(new string[0], string.Empty);
                assert(new string[0], " ");
                assert(new string[0], "+-*");
            }
        }
    }
}
