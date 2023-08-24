using Mafin.Web.UI.Selenium.Configuration;
using Mafin.Web.UI.Selenium.Driver.Strategy;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Mafin.Web.UI.Selenium.YapomlExample;

[TestFixture]
public abstract class AbstractTest
{
    protected IWebDriver driver;

    protected Pages.PagesSpace Ya { get; private set; }

    [SetUp]
    public void SetUpUi()
    {
        var webConfiguration = WebConfigurationProvider.GetWebConfiguration();
        var strategy = DriverMapping.GetDriverStrategy(webConfiguration);
        driver = strategy.GetDriver();

        Ya = driver.Ya(opts => opts.WithBaseUrl("https://www.epam.com")).Pages;

        Ya.HomePage.Open().AcceptCookies();
    }

    [TearDown]
    public void TearDownUi()
    {
        driver.Quit();
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
