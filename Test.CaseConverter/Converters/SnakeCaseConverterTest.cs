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
            ConvertTest("hoge_fuga_piyo", "hoge");
        }
    }
}
