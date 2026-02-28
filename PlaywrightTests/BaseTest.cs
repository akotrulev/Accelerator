using Microsoft.Playwright;
using NUnit.Framework;
using PlaywrightTests.Config;

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
        var options = ConfigurationLoader.GetPlaywrightOptions();

        Playwright = Microsoft.Playwright.Playwright.CreateAsync().GetAwaiter().GetResult();

        IBrowserType browserType = options.BrowserType.ToLower() switch
        {
            "firefox" => Playwright.Firefox,
            "webkit"  => Playwright.Webkit,
            _         => Playwright.Chromium
        };

        Browser = browserType.LaunchAsync(new BrowserTypeLaunchOptions { Headless = options.Headless })
                             .GetAwaiter().GetResult();

        Page = Browser.NewPageAsync(new BrowserNewPageOptions
        {
            ViewportSize = new ViewportSize { Width = options.ViewportWidth, Height = options.ViewportHeight }
        }).GetAwaiter().GetResult();
    }

    [TearDown]
    public void TearDown()
    {
        Browser.DisposeAsync().GetAwaiter().GetResult();
        Playwright.Dispose();
    }
}
