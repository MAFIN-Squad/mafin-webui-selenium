using Mafin.Web.UI.Selenium.Example.Meta;
using NUnit.Framework;

namespace Mafin.Web.UI.Selenium.Example.Tests;

public class FiltersTests : BaseEpamTest
{
    [Test]
    public void FilterContentOnTheInsightsPage()
    {
        // prepare
        const string expectedErrorMessageText = "Sorry, your search returned no results. Please try another combination or check out our latest content\r\n.";
        const string expectedFilteringTag = "SOFTWARE & HI-TECH";

        // act
        _navigationBar.NavigateToMenuItemByClick(NavigationMenu.Insights);
        Assert.IsTrue(_insightsPage.IsOnPage(), "Verify that Insights page is opened");
        _insightsPage.SelectIndustries("Software & Hi-Tech");
        var allResultsContainsTagText = _actionsSteps.GetElements(_insightsPage.IndustriesItem)
            .All(x => _actionsSteps.GetText(x).Contains(expectedFilteringTag));
        Assert.IsTrue(allResultsContainsTagText, "Verify that filtering is applied");
        _insightsPage.SelectContentTypes("Interviews");

        // verify
        var actualText = _actionsSteps.GetText(_insightsPage.ErrorMessage);
        Assert.AreEqual(expectedErrorMessageText, actualText, "Verify that error message is equal to expected");
    }
}
