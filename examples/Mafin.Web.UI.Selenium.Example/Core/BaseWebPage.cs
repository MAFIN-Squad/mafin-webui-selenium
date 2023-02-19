namespace Mafin.Web.UI.Selenium.Example.Core;

public abstract class BaseWebPage
{
    protected BaseWebPage(WdmExtended wdm)
    {
        Wdm = wdm;
    }

    public abstract Uri SiteUrl { get; }

    public virtual string Path => string.Empty;

    protected WdmExtended Wdm { get; }

    public BaseWebPage OpenSite()
    {
        Wdm.GetDriver().Navigate().GoToUrl(SiteUrl);
        return this;
    }

    public BaseWebPage OpenPage()
    {
        var targetPath = Path.StartsWith("/") ? Path[1..] : Path;
        Wdm.GetDriver().Navigate().GoToUrl(SiteUrl + targetPath);
        return this;
    }

    public bool IsOnPage()
    {
        var currentUrl = Wdm.GetDriver().Url;
        currentUrl = currentUrl.Replace(SiteUrl.ToString(), string.Empty);
        var targetPath = Path.StartsWith("/") ? Path[1..] : Path;
        return currentUrl.StartsWith(targetPath);
    }
}
