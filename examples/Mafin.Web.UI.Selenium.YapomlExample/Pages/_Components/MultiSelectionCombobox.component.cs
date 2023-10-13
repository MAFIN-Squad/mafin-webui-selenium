namespace Mafin.Web.UI.Selenium.YapomlExample.Pages.Components;

public partial class MultiSelectionComboboxComponent<TComponent, TConditions, TCondition>
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

        Flyout.Expect(it => it.IsDisplayed().Elapsed(TimeSpan.FromSeconds(1)));

        foreach (var optionName in optionNames)
        {
            Flyout.Option(optionName).Click();
        }

        Collapse();
    }
}
