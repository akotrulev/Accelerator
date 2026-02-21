using Microsoft.Playwright;
using Xunit;

namespace Accelerator.Tests;

public abstract class BaseTest : IAsyncLifetime
{
    protected IPlaywright Playwright { get; private set; } = null!;
    protected IBrowser Browser { get; private set; } = null!;
    protected IPage Page { get; private set; } = null!;

    public async ValueTask InitializeAsync()
    {
        Playwright = await Microsoft.Playwright.Playwright.CreateAsync();
        Browser = await Playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = false
        });
        Page = await Browser.NewPageAsync();
    }

    public async ValueTask DisposeAsync()
    {
        if (Playwright is IAsyncDisposable playwrightAsyncDisposable)
            await playwrightAsyncDisposable.DisposeAsync();
        else
            Playwright.Dispose();
        await Browser.DisposeAsync();
    }
}
