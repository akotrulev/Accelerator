using Microsoft.Playwright;

namespace Accelerator.Tests.Pages;

public class HomePage : BasePage
{
    public HomePage(IPage page) : base(page) { }

    // Locators
    private ILocator Heading => Locator("h1");

    // Actions
    public async Task GoToAsync(string baseUrl = "https://example.com")
    {
        await NavigateAsync(baseUrl);
    }

    public async Task<string> GetHeadingTextAsync() => await Heading.TextContentAsync() ?? string.Empty;
}
