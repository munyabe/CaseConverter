using System;
using System.Globalization;
using System.Threading;

namespace Test.CaseConverter
{
    internal class CultureInfoContext : IDisposable
    {
        private readonly CultureInfo _previousCultureInfo;

        public CultureInfoContext(int cultureInfoId) : this(new CultureInfo(cultureInfoId)) { }

        public CultureInfoContext(string cultureInfoName) : this(new CultureInfo(cultureInfoName)) { }

        public CultureInfoContext(CultureInfo cultureInfo)
        {
            _previousCultureInfo = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = cultureInfo;
        }

        public void Dispose()
        {
            Thread.CurrentThread.CurrentCulture = _previousCultureInfo;
        }
    }
}
