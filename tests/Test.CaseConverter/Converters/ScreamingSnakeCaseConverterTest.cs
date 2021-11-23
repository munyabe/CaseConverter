using CaseConverter.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.CaseConverter.Converters
{
    /// <summary>
    /// <see cref="ScreamingSnakeCaseConverter"/>のテストクラスです。
    /// </summary>
    [TestClass]
    public class ScreamingSnakeCaseConverterTest : CaseConverterTestBase<ScreamingSnakeCaseConverter>
    {
        [TestMethod]
        public void ConvertTest()
        {
            ConvertTest("HOGE_FUGA_PIYO", "HOGE", "H");
        }
    }
}
