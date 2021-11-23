using CaseConverter.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.CaseConverter.Converters
{
    /// <summary>
    /// <see cref="PascalSnakeCaseConverter"/>のテストクラスです。
    /// </summary>
    [TestClass]
    public class PascalSnakeCaseConverterTest : CaseConverterTestBase<PascalSnakeCaseConverter>
    {
        [TestMethod]
        public void ConvertTest()
        {
            ConvertTest("Hoge_Fuga_Piyo", "Hoge", "H");
        }
    }
}
