using CaseConverter.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.CaseConverter.Converters
{
    /// <summary>
    /// <see cref="CamelCaseConverter"/>のテストクラスです。
    /// </summary>
    [TestClass]
    public class CamelCaseConverterTest : CaseConverterTestBase<CamelCaseConverter>
    {
        [TestMethod]
        public void ConvertTest()
        {
            ConvertTest("hogeFugaPiyo", "hoge", "h");
        }

        [TestMethod]
        public void ConvertTestTR()
        {
            ConvertTestTR("saçımŞekilÖnümdenÇekil", "şekil", "i");
        }
    }
}
