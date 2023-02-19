using Mafin.Web.UI.Selenium.Example.Core;
using Mafin.Web.UI.Selenium.Example.Models;
using Mafin.Web.UI.Selenium.Example.Steps;
using OpenQA.Selenium;

namespace Mafin.Web.UI.Selenium.Example.Pages;

public class ContactUsPage : BaseEpamPage
{
    public ContactUsPage(WdmExtended wdm, ActionsSteps actions)
        : base(wdm, actions)
    {
    }

    public override string Path => "about/who-we-are/contact";

    // mandatory fields
    public By Reason => By.CssSelector("span[aria-labelledby*='constructor_mail_subjects-container']");

    public By ReasonList => By.CssSelector("li[id*='constructor_mail_subjects-result']");

    public By FirstName => By.Name("user_first_name");

    public By LastName => By.Name("user_last_name");

    public By Email => By.Name("user_email");

    public By Phone => By.Name("user_phone");

    public By Location => By.CssSelector("span[aria-labelledby*='user_country-container']");

    public By LocationList => By.CssSelector("li[id*='user_country-result']");

    private string LocationListParametrized => "//li[contains(@id,'user_country-result')][contains(text(), '{0}')]";

    public By HowDidYouHearAboutEpam => By.CssSelector("span[aria-labelledby*='how_hear_about-container']");

    public By HowDidYouHearAboutEpamList => By.CssSelector("li[id*='how_hear_about-result']");

    // checkboxes
    public By PrivatePolicyCheckBox => By.CssSelector("label[for='new_form_gdprConsent']");

    public By PrivatePolicyCheckBoxInput => By.Id("new_form_gdprConsent");

    public By SubscribeCheckBox => By.CssSelector("label[for*='form_constructor_subscription_checkbox']");

    public By SubscribeCheckBoxInput => By.Name("subscriptions");

    // methods
    public ContactUsPage FillMandatoryFields(ContactUsRequiredFieldsModel model)
    {
        SelectReasonForYourInquiry(model.Reason);

        _actions.TypeText(FirstName, model.FirstName);
        _actions.TypeText(LastName, model.LastName);
        _actions.TypeText(Email, model.Email);
        _actions.TypeText(Phone, model.Phone);

        SelectLocationFast(model.Location)
            .SelectHowDidYouHearAboutEpam(model.HowDidYouHearAboutEpam);

        return this;
    }

    public List<By> GetMandatoryFields()
    {
        return new List<By> { Reason, FirstName, LastName, Email, Phone, Location, HowDidYouHearAboutEpam };
    }

    public List<By> GetCheckBoxes()
    {
        return new List<By> { PrivatePolicyCheckBox, SubscribeCheckBox };
    }

    public List<By> GetCheckBoxesInputs()
    {
        return new List<By> { PrivatePolicyCheckBoxInput, SubscribeCheckBoxInput };
    }

    public ContactUsPage SelectReasonForYourInquiry(string reasonToSelectValue)
    {
        _actions.SelectValueInTheList(Reason, ReasonList, reasonToSelectValue);
        return this;
    }

    public ContactUsPage SelectLocation(string locationToSelectValue)
    {
        _actions.SelectValueInTheList(Location, LocationList, locationToSelectValue);
        return this;
    }

    public ContactUsPage SelectLocationFast(string locationToSelectValue)
    {
        _actions.Click(Location);
        var replacedLocator = string.Format(LocationListParametrized, locationToSelectValue);
        var valueToSelect = _actions.MoveToElement(Wdm.GetElement(By.XPath(replacedLocator)));

        _actions.ScrollToElement(valueToSelect).Click();
        return this;
    }

    public ContactUsPage SelectHowDidYouHearAboutEpam(string how)
    {
        _actions.SelectValueInTheList(HowDidYouHearAboutEpam, HowDidYouHearAboutEpamList, how);
        return this;
    }
}
