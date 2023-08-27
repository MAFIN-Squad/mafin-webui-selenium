namespace Mafin.Web.UI.Selenium.YapomlExample.Pages.Components;

public partial class MultiSelectionFilterListComponent<TComponent, TConditions>
{
    public void Expand()
    {
        Panel.Click(when => when.IsDisplayed());
    }

    public void Collapse()
    {
        Panel.Click();
    }

    public void Select(params string[] optionNames)
    {
        Expand();

        Flyout.Expect(it => it.IsDisplayed());

        foreach (var optionName in optionNames)
        {
            Flyout.Option(optionName).Click();
        }

        Flyout.Apply.Click();
    }
}
