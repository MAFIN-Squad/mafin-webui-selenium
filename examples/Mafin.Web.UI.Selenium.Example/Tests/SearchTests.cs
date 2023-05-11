using NUnit.Framework;
using OpenQA.Selenium;

namespace Mafin.Web.UI.Selenium.Example.Tests;

public class SearchTests : BaseEpamTest
{
    [Test]
    public void SearchByWord()
    {
        // prepare
        const string textToFind = "RPA";
        const string expectedPageTitle = "RPA Vs Cognitive Automation: Which Technology Will Drive IT Spends for CIOs? I EPAM";

        // act
        _actionsSteps.Click(_navigationBar.SearchIcon);
        _actionsSteps.TypeText(_navigationBar.SearchInput, textToFind);
        _actionsSteps.Click(_navigationBar.FindButton);

        Assert.IsTrue(_searchPage.IsOnPage(), "Verify that Search page is opened");
        var searchResults = _actionsSteps.GetElements(_searchPage.SearchResultsLinks);
        Assert.IsTrue(searchResults.Any(), "Verify that Search results are present");
        _actionsSteps.Click(searchResults.First());

        // verify
        var actualTitle = wdm.GetDriver().Title;
        Assert.AreEqual(expectedPageTitle, actualTitle, "Verify that page title is equal to expected");
    }

    [Test]
    public void VerifyLeadershipPage()
    {
        // prepare
        const string textToFind = "About";
        const string expectedName = "ARKADIY DOBKIN";

        // act
        _actionsSteps.Click(_navigationBar.SearchIcon);
        _actionsSteps.TypeText(_navigationBar.SearchInput, textToFind);
        _actionsSteps.SendKeys(Keys.Enter);
        Assert.IsTrue(_searchPage.IsOnPage(), "Verify that Search page is opened");
        _actionsSteps.ClickOnFirstInTheListByText(_searchPage.SearchResultsLinks, textToFind);
        Assert.IsTrue(_aboutPage.IsOnPage(), "Verify that About page is opened");
        _actionsSteps.Click(_aboutPage.SeeAllLeaderShipLink);
        Assert.IsTrue(_leadershipPage.IsOnPage(), "Verify that Leadership page is opened");

        // verify
        var element = _actionsSteps.GetElements(_leadershipPage.Names)
            .FirstOrDefault(e => e.Text.Replace("\r\n", " ").ToUpper().Equals(expectedName));
        Assert.IsNotNull(element, $"Verify that leadership name contains '{expectedName}'");
    }
}
