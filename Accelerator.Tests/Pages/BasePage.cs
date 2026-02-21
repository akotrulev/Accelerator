using Microsoft.Playwright;

namespace Accelerator.Tests.Pages;

public abstract class BasePage
{
    protected IPage Page { get; }

    protected BasePage(IPage page)
    {
        Page = page;
    }

    protected ILocator Locator(string selector) => Page.Locator(selector);

    protected ILocator GetByRole(AriaRole role, string? name = null) =>
        name != null ? Page.GetByRole(role, new() { Name = name }) : Page.GetByRole(role);

    protected Task NavigateAsync(string url) => Page.GotoAsync(url);

    protected Task<string> GetTitleAsync() => Page.TitleAsync();

    protected Task<string> GetUrlAsync() => Task.FromResult(Page.Url);
}
