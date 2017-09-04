using CaseConverter.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.CaseConverter.Converters
{
    /// <summary>
    /// <see cref="KebabCaseConverter"/>のテストクラスです。
    /// </summary>
    [TestClass]
    public class KebabCaseConverterTest : CaseConverterTestBase<KebabCaseConverter>
    {
        [TestMethod]
        public void ConvertTest()
        {
            ConvertTest("hoge-fuga-piyo", "hoge", "h");
        }

        [TestMethod]
        public void ConvertTestTR()
        {
            ConvertTestTR("saçım-şekil-önümden-çekil", "şekil", "i");
        }
    }
}
