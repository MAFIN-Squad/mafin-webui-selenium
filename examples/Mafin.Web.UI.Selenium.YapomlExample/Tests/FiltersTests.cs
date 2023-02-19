using NUnit.Framework;

namespace Mafin.Web.UI.Selenium.YapomlExample.Tests;

public class FiltersTests : AbstractTest
{
    [Test]
    public void FilterContentOnTheInsightsPage()
    {
        Ya.HomePage.Navigate("INSIGHTS");

        var insightsPage = Ya.Insights.InsightsPage;
        insightsPage.Filter.When(it => it.IsDisplayed()).Industry.Select("Software & Hi-Tech");

        Assert.That(insightsPage.Tags.Select(t => t.Text), Is.EqualTo(new List<string> { "SOFTWARE & HI-TECH" }));

        insightsPage.Filter.ContentType.Select("Interviews");
        Assert.That(insightsPage.FilterErrorMessage.When(it => it.IsDisplayed()).Text, Is.EqualTo("Sorry, your search returned no results. Please try another combination or check out our latest content\r\n."));
    }
}
