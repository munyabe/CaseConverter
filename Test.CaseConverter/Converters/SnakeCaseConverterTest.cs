using CaseConverter.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.CaseConverter.Converters
{
    /// <summary>
    /// <see cref="SnakeCaseConverter"/>のテストクラスです。
    /// </summary>
    [TestClass]
    public class SnakeCaseConverterTest : CaseConverterTestBase<SnakeCaseConverter>
    {
        [TestMethod]
        public void ConvertTest()
        {
            ConvertTest("hoge_fuga_piyo", "hoge", "h");
        }

        [TestMethod]
        public void ConvertTestTR()
        {
            ConvertTestTR("saçım_şekil_önümden_çekil", "şekil", "i");
        }
    }
}
