using NUnit.Framework;

namespace Mafin.Web.UI.Selenium.YapomlExample.Tests;

public class OurOfficesTests : AbstractTest
{
    [Test]
    public void CheckBelarussianOffices()
    {
        var expectedOfficeList = new List<string> { "BREST", "GOMEL", "GRODNO", "MINSK", "MOGILEV", "VITEBSK" };

        Ya.HomePage.Navigate("OUR WORK");

        var section = Ya.OurWorkPage.OurOfficesSection;
        section.Regions.First(r => r.Text == "EMEA").Click();
        section.Locations.First(l => l.Text == "BELARUS").Click();

        Assert.That(section.DetailsSection.When(it => it.IsDisplayed()).Offices.Select(o => o.Name.Text), Is.EqualTo(expectedOfficeList));
    }
}
