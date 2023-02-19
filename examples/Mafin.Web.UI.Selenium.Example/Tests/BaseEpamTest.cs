using Mafin.Web.UI.Selenium.Example.Pages;
using Mafin.Web.UI.Selenium.Example.Steps;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Mafin.Web.UI.Selenium.Example.Tests;

public class BaseEpamTest : AbstractTest
{
    protected ActionsSteps _actionsSteps;
    protected BaseEpamPage _baseEpamPage;
    protected ContactUsPage _contactUsPage;
    protected NavigationBar _navigationBar;
    protected ServicesPage _servicesPage;
    protected SearchPage _searchPage;
    protected AboutPage _aboutPage;
    protected LeadershipPage _leadershipPage;
    protected CareersPage _careersPage;
    protected JobListeningPage _jobListeningPage;
    protected InsightsPage _insightsPage;
    protected OurWorkPage _ourWorkPage;

    [SetUp]
    public void BaseEpamSetUp()
    {
        wdm.SetCommonConditionsToWait(GetCommonConditions(driver));
        _actionsSteps = new ActionsSteps(wdm);
        _baseEpamPage = new BaseEpamPage(wdm, _actionsSteps);
        wdm.SetActionsToIntercept(GetCommonInterceptActions(_baseEpamPage));
        _contactUsPage = new ContactUsPage(wdm, _actionsSteps);
        _servicesPage = new ServicesPage(wdm, _actionsSteps);
        _searchPage = new SearchPage(wdm, _actionsSteps);
        _aboutPage = new AboutPage(wdm, _actionsSteps);
        _leadershipPage = new LeadershipPage(wdm, _actionsSteps);
        _careersPage = new CareersPage(wdm, _actionsSteps);
        _navigationBar = new NavigationBar(wdm, _actionsSteps);
        _jobListeningPage = new JobListeningPage(wdm, _actionsSteps);
        _insightsPage = new InsightsPage(wdm, _actionsSteps);
        _ourWorkPage = new OurWorkPage(wdm, _actionsSteps);

        _baseEpamPage.OpenSite();
    }

    private List<Func<bool>> GetCommonConditions(IWebDriver driver)
    {
        return new List<Func<bool>>
        {
            () => "complete".Equals(((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState")?.ToString(), StringComparison.Ordinal),
            () => "0".Equals(((IJavaScriptExecutor)driver).ExecuteScript("return jQuery.active")?.ToString(), StringComparison.Ordinal)
        };
    }

    private List<Action> GetCommonInterceptActions(BaseEpamPage baseEpamPage)
    {
        return new List<Action>
        {
            () => baseEpamPage.AcceptCookiesIfPresent()
        };
    }
}
