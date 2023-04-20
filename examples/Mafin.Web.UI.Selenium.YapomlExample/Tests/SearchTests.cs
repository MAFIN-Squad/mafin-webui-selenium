using NUnit.Framework;

namespace Mafin.Web.UI.Selenium.YapomlExample.Tests;

public class SearchTests : AbstractTest
{
    [Test]
    public void SearchByWord()
    {
        var results = Ya.HomePage.Header.Search.When(it => it.SearchButton.IsEnabled()).Search("automation").Results;

        Assert.That(results.Count, Is.GreaterThan(0));

        results.First().Title.Click();

        Assert.That(driver.Title, Is.EqualTo("Intelligent Automation Services | EPAM"));
    }

    [Test]
    public void VerifyLeadershipPage()
    {
        var results = Ya.HomePage.Header.Search.Search("about", usingKeyboard: true).Results;

        results.First().Title.Click();

        Assert.That(driver.Title, Is.EqualTo("About"));

        Ya.About.AboutPage.SeeAll.Click();

        Assert.That(driver.Title, Is.EqualTo("Leadership"));

        Assert.That(Ya.About.WhoWeAre.LeadershipPage.Directors.First().Name.Text.Replace("\r\n", " ").ToUpper(),
            Is.EqualTo("ARKADIY DOBKIN"));
    }
}
