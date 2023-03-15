using Mafin.Web.UI.Selenium.Driver.Strategy;
using Mafin.Web.UI.Selenium.Example.Core;
using Mafin.Web.UI.Selenium.Meta;
using Mafin.Web.UI.Selenium.Models;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Safari;

namespace Mafin.Web.UI.Selenium.Example.Tests;

[TestFixture]
public abstract class AbstractTest
{
    protected IWebDriver driver;
    protected WdmExtended wdm;

    [SetUp]
    public void SetUpUi()
    {
        var webConfiguration = new WebConfiguration
        {
            DriverType = "chrome",
            RunType = RunType.Local,
            Arguments = new List<string>() { "--start-maximized", "--lang=en" },
            Preferences = new Dictionary<string, object>
            {
                ["credentials_enable_service"] = false,
                ["profile.password_manager_enabled"] = false
            },
            RemoteConfig = new RemoteConfig
            {
                BrowserVersion = "97.0",
                Url = new Uri("http://localhost:4444/wd/hub")
            },
            TimeoutsConfig = new TimeoutsConfig
            {
                ImplicitWait = TimeSpan.Zero,
                PageLoad = TimeSpan.FromSeconds(60),
                CommandTimeout = TimeSpan.FromSeconds(30),
                AsynchronousJavaScript = TimeSpan.FromSeconds(30),
                ExplicitWaitTimeout = TimeSpan.FromSeconds(90),
                ExplicitWaitPoolingTimeout = TimeSpan.FromMilliseconds(500)
            }
        };
        var strategy = DriverMapping.GetDriverStrategy(webConfiguration);
        driver = strategy.GetDriver();

        wdm = new WdmExtended(driver, webConfiguration.TimeoutsConfig);
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
