using Microsoft.Playwright;

namespace PlaywrightTests.Pages;

public class HomePage : BasePage
{
    public HomePage(IPage page) : base(page) { }

    public void Navigate(string url) => NavigateTo(url);

    public string Title() => Page.TitleAsync().GetAwaiter().GetResult();
}
