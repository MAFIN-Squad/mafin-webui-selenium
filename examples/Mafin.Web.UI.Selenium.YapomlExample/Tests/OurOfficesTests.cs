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
        section.Regions["EMEA"].Click();
        section.Locations["BELARUS"].Click();
        section.DetailsSection.Expect(it => it.IsDisplayed()).Offices[0].Expect(it => it.Name.Text.IsNot(string.Empty));

        Assert.That(section.DetailsSection.Offices.Select(o => o.Name.Text), Is.EqualTo(expectedOfficeList));
    }
}
