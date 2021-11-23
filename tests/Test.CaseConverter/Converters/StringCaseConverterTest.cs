﻿using System;
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

        [TestMethod]
        public void GetCasePatternTest()
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

        [TestMethod]
        public void GetWordsTest()
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
}
