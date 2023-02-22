namespace Mafin.Web.UI.Selenium.YapomlExample.Pages;

partial class BaseComponent<TComponent, TConditions>
{
    public string Value => WrappedElement.GetAttribute("value");
}
