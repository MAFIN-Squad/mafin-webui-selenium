using NUnit.Framework;

namespace Mafin.Web.UI.Selenium.YapomlExample.Tests;

public class FiltersTests : AbstractTest
{
    [Test]
    public void FilterContentOnTheInsightsPage()
    {
        Ya.HomePage.Navigate("Insights");

        var insightsPage = Ya.Insights.InsightsPage;
        insightsPage.Industry.Expect(it => it.IsDisplayed()).Select("Software & Hi-Tech");

        Assert.That(insightsPage.Tags.Select(t => t.Text), Is.EqualTo(new List<string> { "Software & Hi-Tech" }));
    }
}
