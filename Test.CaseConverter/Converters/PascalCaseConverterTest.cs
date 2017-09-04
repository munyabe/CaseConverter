using CaseConverter.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.CaseConverter.Converters
{
    /// <summary>
    /// <see cref="PascalCaseConverter"/>のテストクラスです。
    /// </summary>
    [TestClass]
    public class PascalCaseConverterTest : CaseConverterTestBase<PascalCaseConverter>
    {
        [TestMethod]
        public void ConvertTest()
        {
            ConvertTest("HogeFugaPiyo", "Hoge", "H");
        }

        [TestMethod]
        public void ConvertTestTR()
        {
            ConvertTestTR("SaçımŞekilÖnümdenÇekil", "Şekil", "İ");
        }
    }
}
