using Mafin.Web.UI.Selenium.Configuration;
using Mafin.Web.UI.Selenium.Driver.Strategy;
using Mafin.Web.UI.Selenium.Example.Core;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Mafin.Web.UI.Selenium.Example.Tests;

[TestFixture]
public abstract class AbstractTest
{
    protected IWebDriver driver;
    protected WdmExtended wdm;

    [SetUp]
    public void SetUpUi()
    {
        var webConfiguration = WebConfigurationProvider.GetWebConfiguration();
        var strategy = DriverMapping.GetDriverStrategy(webConfiguration);
        driver = strategy.GetDriver();

        wdm = new WdmExtended(driver, webConfiguration.Timeouts);
    }

    [TearDown]
    public void TearDownUi()
    {
        driver?.Quit();
    }
}

[Parallelizable(ParallelScope.Fixtures)]
[SetUpFixture]
public class GlobalSetup
{
    [OneTimeSetUp]
    public void OneTimeBaseSetup()
    {
        // something might be here
    }

    [OneTimeTearDown]
    public void AfterAll()
    {
        // something might be here
    }
}
