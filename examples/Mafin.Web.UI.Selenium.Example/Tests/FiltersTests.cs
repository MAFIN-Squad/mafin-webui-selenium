using Mafin.Web.UI.Selenium.Example.Meta;
using NUnit.Framework;

namespace Mafin.Web.UI.Selenium.Example.Tests;

public class FiltersTests : BaseEpamTest
{
    [Test]
    public void FilterContentOnTheInsightsPage()
    {
        // prepare
        const string expectedFilteringTag = "SOFTWARE & HI-TECH";

        // act
        _navigationBar.NavigateToMenuItemByClick(NavigationMenu.Insights);
        Assert.IsTrue(_insightsPage.IsOnPage(), "Verify that Insights page is opened");
        _insightsPage.SelectIndustries("Software & Hi-Tech");
        _actionsSteps.Click(_insightsPage.ApplyButton);

        var allResultsContainsTagText = _actionsSteps.GetElements(_insightsPage.IndustriesList)
            .All(ul =>
                _actionsSteps.GetElements(ul, _insightsPage.IndustriesItem)
                    .Any(li => _actionsSteps.GetText(li).Contains(expectedFilteringTag)));

        // verify
        Assert.IsTrue(allResultsContainsTagText, "Verify that filtering is applied");
    }
}
