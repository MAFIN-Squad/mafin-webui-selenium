using OpenQA.Selenium;

namespace Mafin.Web.UI.Selenium.Tests.Unit.Stubs
{
    public class FakeOptions : DriverOptions
    {
        public override ICapabilities ToCapabilities()
        {
            throw new NotImplementedException();
        }
    }
}
