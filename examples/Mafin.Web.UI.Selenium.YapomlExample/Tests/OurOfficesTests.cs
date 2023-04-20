using NUnit.Framework;

namespace Mafin.Web.UI.Selenium.YapomlExample.Tests;

public class OurOfficesTests : AbstractTest
{
    [Test]
    public void CheckBelarussianOffices()
    {
        var expectedOfficeList = new List<string> { "BREST", "GOMEL", "GRODNO", "MINSK", "MOGILEV", "VITEBSK" };

        Ya.HomePage.ExploreOurClientsWork.Click();

        var section = Ya.ClientWorkPage.OurOfficesSection;
        section.Regions.First(r => r.Text == "EMEA").Click();
        section.Locations.First(l => l.Text == "BELARUS").Click();
        section.DetailsSection.When(it => it.IsDisplayed()).Offices.FirstOrDefault()
            ?.When(it => it.Name.Text.IsNot(string.Empty));

        Assert.That(section.DetailsSection.Offices.Select(o => o.Name.Text), Is.EqualTo(expectedOfficeList));
    }
}
