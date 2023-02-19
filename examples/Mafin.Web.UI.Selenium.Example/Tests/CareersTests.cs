using Mafin.Web.UI.Selenium.Example.Meta;
using NUnit.Framework;

namespace Mafin.Web.UI.Selenium.Example.Tests;

public class CareersTests : BaseEpamTest
{
    [Test]
    public void CheckSoftwareAndTestingEngineersPositions()
    {
        // prepare
        const int indexOfTheTargetItem = 1;

        // act
        _navigationBar.NavigateToMenuItemByClick(NavigationMenu.Careers);
        Assert.IsTrue(_careersPage.IsOnPage(), "Verify that Careers page is opened");
        _careersPage.SelectSkills("Software, System, and Test Engineering");
        _actionsSteps.Click(_careersPage.FindButton);

        var item = _actionsSteps.FindElementInTheListByIndex(_careersPage.SearchResultItem, indexOfTheTargetItem);
        var itemTitle = _actionsSteps.GetText(_actionsSteps.GetElement(item, _careersPage.SearchResultItemName));
        var viewAndApplyItemButton = _actionsSteps.GetElement(item, _careersPage.SearchResultItemViewAndApplyButton);
        _actionsSteps.Click(viewAndApplyItemButton);

        // verify
        Assert.IsTrue(_jobListeningPage.IsOnPage(), "Verify that Job Listening page is opened");
        var actualJobTitle = _actionsSteps.GetText(_jobListeningPage.ApplyForJobTitle);
        Assert.AreEqual(itemTitle, actualJobTitle, "Verify that Job Title is equal to expected");
    }
}
