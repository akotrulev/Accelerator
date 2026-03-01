using Microsoft.Playwright;

namespace PlaywrightTests.Pages;

public class HomePage : BasePage
{
    public HomePage(IPage page) : base(page) { }

    // Navigates to the given URL via the base page helper.
    public void Navigate(string url) => NavigateTo(url);

    // Returns the current page title.
    public string Title() => Page.TitleAsync().GetAwaiter().GetResult();
}
