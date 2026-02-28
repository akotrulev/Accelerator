using Microsoft.Playwright;

namespace PlaywrightTests.Pages;

public abstract class BasePage
{
    protected IPage Page { get; }

    protected BasePage(IPage page)
    {
        Page = page;
    }

    protected void NavigateTo(string url) =>
        Page.GotoAsync(url).GetAwaiter().GetResult();

    protected void Click(string selector) =>
        Page.ClickAsync(selector).GetAwaiter().GetResult();

    protected void Fill(string selector, string value) =>
        Page.FillAsync(selector, value).GetAwaiter().GetResult();

    protected string InnerText(string selector) =>
        Page.InnerTextAsync(selector).GetAwaiter().GetResult();

    protected bool IsVisible(string selector) =>
        Page.IsVisibleAsync(selector).GetAwaiter().GetResult();
}
