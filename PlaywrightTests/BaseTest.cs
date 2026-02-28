using Microsoft.Playwright;
using NUnit.Framework;

namespace PlaywrightTests;

[TestFixture]
public abstract class BaseTest
{
    protected IPlaywright Playwright { get; private set; } = null!;
    protected IBrowser Browser { get; private set; } = null!;
    protected IPage Page { get; private set; } = null!;

    [SetUp]
    public void SetUp()
    {
        Playwright = Microsoft.Playwright.Playwright.CreateAsync().GetAwaiter().GetResult();
        Browser = Playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = true })
                            .GetAwaiter().GetResult();
        Page = Browser.NewPageAsync().GetAwaiter().GetResult();
    }

    [TearDown]
    public void TearDown()
    {
        Browser.DisposeAsync().GetAwaiter().GetResult();
        Playwright.Dispose();
    }
}
