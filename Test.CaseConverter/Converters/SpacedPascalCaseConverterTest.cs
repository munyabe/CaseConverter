using CaseConverter.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.CaseConverter.Converters
{
    /// <summary>
    /// <see cref="SpacedPascalCaseConverterTest"/>のテストクラスです。
    /// </summary>
    [TestClass]
    public class SpacedPascalCaseConverterTest : CaseConverterTestBase<SpacedPascalCaseConverter>
    {
        [TestMethod]
        public void ConvertTest()
        {
            ConvertTest("Hoge Fuga Piyo", "Hoge", "H");
        }
    }
}
