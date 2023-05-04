using NUnit.Framework;

namespace Mafin.Web.UI.Selenium.YapomlExample.Tests;

public class SearchTests : AbstractTest
{
    [Test]
    public void SearchByWord()
    {
        var results = Ya.HomePage.Header.Search.When(it => it.SearchButton.IsEnabled()).Search("RPA").Results;

        Assert.That(results.Count, Is.GreaterThan(0));

        results.First().Title.Click();

        Assert.That(driver.Title, Is.EqualTo("RPA Vs Cognitive Automation: Which Technology Will Drive IT Spends for CIOs? I EPAM"));
    }

    [Test]
    public void VerifyLeadershipPage()
    {
        var results = Ya.HomePage.Header.Search.Search("about", usingKeyboard: true).Results;

        results.First().Title.Click();

        Assert.That(driver.Title, Is.EqualTo("One of the Fastest-Growing Public Tech Companies | About EPAM"));

        Ya.About.AboutPage.SeeAll.Click();

        Assert.That(driver.Title, Is.EqualTo("Leadership"));

        Assert.That(Ya.About.WhoWeAre.LeadershipPage.Directors.First().Name.Text.Replace("\r\n", " ").ToUpper(),
            Is.EqualTo("ARKADIY DOBKIN"));
    }
}
