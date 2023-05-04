namespace Mafin.Web.UI.Selenium.YapomlExample.Pages.Components;

partial class MultiSelectionComboboxComponent<TComponent, TConditions>
{
    public void Expand()
    {
        SelectionPane.Click(when => when.IsDisplayed());
    }

    public void Collapse()
    {
        SelectionPane.Click();
    }

    public void Select(params string[] optionNames)
    {
        Expand();

        Flyout.When(it => it.IsDisplayed().Elapsed(TimeSpan.FromSeconds(1)));

        foreach (var optionName in optionNames)
        {
            Flyout.Option(optionName).Click();
        }

        Collapse();
    }
}
