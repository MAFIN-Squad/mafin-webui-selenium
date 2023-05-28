using NUnit.Framework;

namespace Mafin.Web.UI.Selenium.YapomlExample.Tests;

public class CareersTests : AbstractTest
{
    [Test]
    public void CheckSoftwareAndTestingEngineersPositions()
    {
        Ya.HomePage.Navigate("Careers");

        Ya.HomePage.Expect(that => that.Title.Is("Explore Professional Growth Opportunities | EPAM Careers"));

        Ya.Careers.CareersPage.JobFilter.Skill.Select("Software, System, and Test Engineering");

        Ya.Careers.CareersPage.JobFilter.FindButton.Click();

        var expectedItemName = Ya.Careers.JobListingPage.ResultItems[0].Name.Text;

        Ya.Careers.JobListingPage.ResultItems[0].ViewAndApplyButton.Click();

        Ya.Careers.JobDetailPage.ApplyFor.Name.Expect(that => that.Text.Is(expectedItemName));
    }
}
