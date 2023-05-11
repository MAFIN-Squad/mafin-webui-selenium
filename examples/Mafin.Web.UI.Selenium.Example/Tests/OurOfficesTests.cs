using Mafin.Web.UI.Selenium.Example.Meta;
using NUnit.Framework;

namespace Mafin.Web.UI.Selenium.Example.Tests;

public class OurOfficesTests : BaseEpamTest
{
    [Test]
    public void CheckBelarussianOffices()
    {
        // prepare
        const string tabToSelect = "EMEA";
        const string officeToSelect = "BELARUS";
        var expectedOfficeList = new List<string> { "BREST", "GOMEL", "GRODNO", "MINSK", "MOGILEV", "VITEBSK" };

        // act
        _actionsSteps.Click(_homePage.ExploreOurClientWork);
        Assert.IsTrue(_clientWorkPage.IsOnPage(), "Verify that ClientWork page is opened");
        _clientWorkPage
            .SelectTab(tabToSelect)
            .SelectOffice(officeToSelect);

        // verify
        Assert.IsTrue(_clientWorkPage.IsOnPage(), "Verify that OurWork page is still opened");
        var actualOfficeName = _clientWorkPage.GetActiveOfficeCountryName();
        Assert.AreEqual(officeToSelect, actualOfficeName, "Verify that active office is selected");
        var actualOfficeNames = _clientWorkPage.GetActiveOfficeNames().Select(_actionsSteps.GetText).ToList();
        Assert.AreEqual(expectedOfficeList, actualOfficeNames, "Verify that office lis is equal to expected");
    }
}
