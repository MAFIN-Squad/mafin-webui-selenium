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
        _navigationBar.NavigateToMenuItemByClick(NavigationMenu.OurWork);
        Assert.IsTrue(_ourWorkPage.IsOnPage(), "Verify that OurWork page is opened");
        _ourWorkPage
            .SelectTab(tabToSelect)
            .SelectOffice(officeToSelect);

        // verify
        Assert.IsTrue(_ourWorkPage.IsOnPage(), "Verify that OurWork page is still opened");
        var actualOfficeName = _actionsSteps.GetText(_ourWorkPage.GetActiveOffice());
        Assert.AreEqual(officeToSelect, actualOfficeName, "Verify that active office is selected");
        var actualOfficeNames = _ourWorkPage.GetActiveOfficeNames().Select(_actionsSteps.GetText).ToList();
        Assert.AreEqual(expectedOfficeList, actualOfficeNames, "Verify that office lis is equal to expected");
    }
}
