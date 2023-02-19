using Mafin.Web.UI.Selenium.Example.Meta;
using Mafin.Web.UI.Selenium.Example.Models;
using NUnit.Framework;

namespace Mafin.Web.UI.Selenium.Example.Tests;

public class ContactUsTests : BaseEpamTest
{
    [Test]
    public void FillInContactUsForm()
    {
        // prepare
        var contactForm = new ContactUsRequiredFieldsModel
        {
            Reason = "Careers",
            FirstName = "TestFirstName",
            LastName = "TestLastName",
            Email = "testemail@test.com",
            Phone = "1234567890",
            Location = "Japan",
            HowDidYouHearAboutEpam = "Worked at EPAM"
        };

        // act
        _navigationBar.NavigateToMenuItemByClick(NavigationMenu.ContactUs);
        _contactUsPage.FillMandatoryFields(contactForm);
        _contactUsPage.GetCheckBoxes().ForEach(by =>
        {
            _actionsSteps.Click(by);
        });

        // verify
        Assert.Multiple(() =>
        {
            _contactUsPage.GetMandatoryFields().ForEach(by =>
            {
                var actualText = _actionsSteps.GetText(by);
                Assert.IsNotEmpty(actualText, $"Verify that the field '{by}' is not empty");
            });
        });
        Assert.Multiple(() =>
        {
            _contactUsPage.GetCheckBoxesInputs().ForEach(by =>
            {
                Assert.IsTrue(_actionsSteps.IsChecked(by), $"Verify that the checkbox '{by}' is checked");
            });
        });
    }

    [Test]
    public void GoToContactsFromServices()
    {
        // prepare
        _servicesPage.OpenPage();

        // act
        _actionsSteps.Click(_servicesPage.ContactUsButton);

        // verify
        Assert.IsTrue(_contactUsPage.IsOnPage(), "Verify that Contact Us page is opened");
    }
}
