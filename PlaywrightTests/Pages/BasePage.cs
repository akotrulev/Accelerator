using Microsoft.Playwright;
using PlaywrightTests.Logging;

namespace PlaywrightTests.Pages;

public abstract class BasePage
{
    protected IPage Page { get; }

    protected BasePage(IPage page)
    {
        Page = page;
    }

    protected void NavigateTo(string url)
    {
        TestLogger.Log($"NavigateTo: url={url}");
        Page.GotoAsync(url).GetAwaiter().GetResult();
    }

    protected void Click(string selector)
    {
        TestLogger.Log($"Click: selector={selector}");
        Page.ClickAsync(selector).GetAwaiter().GetResult();
    }

    protected void Fill(string selector, string value)
    {
        TestLogger.Log($"Fill: selector={selector}, value={value}");
        Page.FillAsync(selector, value).GetAwaiter().GetResult();
    }

    protected string InnerText(string selector)
    {
        TestLogger.Log($"InnerText: selector={selector}");
        return Page.InnerTextAsync(selector).GetAwaiter().GetResult();
    }

    protected bool IsVisible(string selector)
    {
        TestLogger.Log($"IsVisible: selector={selector}");
        return Page.IsVisibleAsync(selector).GetAwaiter().GetResult();
    }
}
