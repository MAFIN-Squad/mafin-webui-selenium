namespace Mafin.Web.UI.Selenium.YapomlExample.Pages.Components;

partial class MultiSelectionFilterListComponent<TComponent, TConditions>
{
    public void Expand()
    {
        Panel.When(it => it.IsDisplayed()).Click();
    }

    public void Collapse()
    {
        Panel.Click();
    }

    public void Select(params string[] optionNames)
    {
        Expand();

        Flyout.When(it => it.IsDisplayed());

        foreach (var optionName in optionNames)
        {
            Flyout.Option(optionName).Click();
        }

        Flyout.Apply.Click();
    }
}
