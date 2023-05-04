using NUnit.Framework;

namespace Mafin.Web.UI.Selenium.YapomlExample.Tests;

public class SearchTests : AbstractTest
{
    [Test]
    public void SearchByWord()
    {
        var results = Ya.HomePage.Header.Search
            .Search("RPA").Results;

        Assert.That(results.Count, Is.GreaterThan(0));

        results[0].Title.Click();

        Assert.That(driver.Title, Is.EqualTo("RPA Vs Cognitive Automation: Which Technology Will Drive IT Spends for CIOs? I EPAM"));
    }

    [Test]
    public void VerifyLeadershipPage()
    {
        Ya.HomePage.Header.Search.Search("about", usingKeyboard: true)
            .Results[0].Title.Click();

        Assert.That(driver.Title, Is.EqualTo("One of the Fastest-Growing Public Tech Companies | About EPAM"));

        Ya.About.AboutPage.SeeAll.Click();

        Assert.That(driver.Title, Is.EqualTo("Leadership"));

        var firstDirector = Ya.About.WhoWeAre.LeadershipPage.Directors[0];
        Assert.That(firstDirector.Name.Firstname.Text, Is.EqualTo("Arkadiy"));
        Assert.That(firstDirector.Name.Lastname.Text, Is.EqualTo("Dobkin"));
    }
}
