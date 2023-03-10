using NUnit.Framework;

namespace Mafin.Web.UI.Selenium.YapomlExample.Tests;

public class ContactUsTests : AbstractTest
{
    [Test]
    public void FillInContactUsForm()
    {
        Ya.HomePage.Header.ContactUsButton.Click();

        var contactUsPage = Ya.About.WhoWeAre.ContactPage;

        contactUsPage.TheReasonForYourInquiry.Select("Careers");
        contactUsPage.FirstName.SendKeys("TestFirstName");
        contactUsPage.LastName.SendKeys("TestLastName");
        contactUsPage.Email.SendKeys("testemail@test.com");
        contactUsPage.Phone.SendKeys("1234567890");
        contactUsPage.Location.Select("Japan");
        contactUsPage.HowDidYouHearAboutEpam.Select("Worked at EPAM");
        contactUsPage.Consent.Check();

        Assert.That(contactUsPage.TheReasonForYourInquiry.SelectedOption, Is.EqualTo("Careers"));
        Assert.That(contactUsPage.FirstName.Value, Is.EqualTo("TestFirstName"));
        Assert.That(contactUsPage.Email.Value, Is.EqualTo("testemail@test.com"));
        Assert.That(contactUsPage.Phone.Value, Is.EqualTo("1234567890"));
        Assert.That(contactUsPage.Location.SelectedOption, Is.EqualTo("Japan"));
        Assert.That(contactUsPage.HowDidYouHearAboutEpam.SelectedOption, Is.EqualTo("Worked at EPAM"));
        Assert.That(contactUsPage.Consent.IsChecked, Is.True);
    }

    [Test]
    public void GoToContactsFromServices()
    {
        Ya.HomePage.Header.ContactUsButton.Click();

        Assert.That(driver.Title, Is.EqualTo("Learn more about EPAM and Contact Us | EPAM"));
    }
}
