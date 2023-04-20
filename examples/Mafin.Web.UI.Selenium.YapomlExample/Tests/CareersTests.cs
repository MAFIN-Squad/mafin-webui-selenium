using NUnit.Framework;

namespace Mafin.Web.UI.Selenium.YapomlExample.Tests;

public class CareersTests : AbstractTest
{
    [Test]
    public void CheckSoftwareAndTestingEngineersPositions()
    {
        Ya.HomePage.Navigate("Careers");

        Assert.That(driver.Title, Is.EqualTo("Explore Professional Growth Opportunities | EPAM Careers"));

        Ya.Careers.CareersPage.JobFilter.Skill.Select("Software, System, and Test Engineering");

        Ya.Careers.CareersPage.JobFilter.FindButton.Click();

        var expectedItemName = Ya.Careers.JobListingPage.ResultItems.First().Name.Text;

        Ya.Careers.JobListingPage.ResultItems.First().ViewAndApplyButton.Click();

        Assert.That(Ya.Careers.JobDetailPage.ApplyFor.Name.Text, Is.EqualTo(expectedItemName));
    }
}
