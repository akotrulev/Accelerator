using Allure.Net.Commons;
using Microsoft.Playwright;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using PlaywrightTests.Config;
using TestCore.Logging;

namespace PlaywrightTests.Tests;

[TestFixture]
public abstract class BaseTest
{
    protected IPlaywright Playwright { get; private set; } = null!;
    protected IBrowser    Browser    { get; private set; } = null!;
    protected IBrowserContext Context { get; private set; } = null!;
    protected IPage       Page       { get; private set; } = null!;

    private PlaywrightOptions _options = null!;

    [SetUp]
    public void SetUp()
    {
        TestLogger.Clear();
        _options = ConfigurationLoader.GetPlaywrightOptions();

        Playwright = Microsoft.Playwright.Playwright.CreateAsync().GetAwaiter().GetResult();

        IBrowserType browserType = _options.BrowserType.ToLower() switch
        {
            "firefox" => Playwright.Firefox,
            "webkit"  => Playwright.Webkit,
            _         => Playwright.Chromium
        };

        Browser = browserType.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = _options.Headless,
            SlowMo   = _options.SlowMo > 0 ? _options.SlowMo : null
        }).GetAwaiter().GetResult();

        var contextOptions = new BrowserNewContextOptions
        {
            ViewportSize = new ViewportSize { Width = _options.ViewportWidth, Height = _options.ViewportHeight }
        };

        if (_options.RecordVideo)
        {
            contextOptions.RecordVideoDir  = "videos/";
            contextOptions.RecordVideoSize = new RecordVideoSize
            {
                Width  = _options.ViewportWidth,
                Height = _options.ViewportHeight
            };
        }

        Context = Browser.NewContextAsync(contextOptions).GetAwaiter().GetResult();

        Context.Tracing.StartAsync(new TracingStartOptions
        {
            Title       = $"{TestContext.CurrentContext.Test.ClassName}.{TestContext.CurrentContext.Test.Name}",
            Screenshots = true,
            Snapshots   = true,
            Sources     = true
        }).GetAwaiter().GetResult();

        Page = Context.NewPageAsync().GetAwaiter().GetResult();

        if (_options.DefaultTimeoutMs > 0)
            Page.SetDefaultTimeout(_options.DefaultTimeoutMs);

        // Single merged console handler: prefix errors, pass everything else through.
        Page.Console  += (_, msg) => Console.WriteLine(msg.Type == "error" ? $"[ERROR] {msg.Text}" : msg.Text);
        Page.PageError += (_, err) => Console.Error.WriteLine(err);
    }

    [TearDown]
    public void TearDown()
    {
        var failed = TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed;

        if (failed)
        {
            TestContext.Out.WriteLine("--- Test Action Log ---");
            foreach (var entry in TestLogger.GetLogs())
                TestContext.Out.WriteLine(entry);
            TestContext.Out.WriteLine("--- End of Log ---");

            Directory.CreateDirectory("failures");
            var screenshotPath = $"failures/{TestContext.CurrentContext.Test.Name}.png";
            Page.ScreenshotAsync(new PageScreenshotOptions { Path = screenshotPath, FullPage = true })
                .GetAwaiter().GetResult();
            AllureApi.AddAttachment("Screenshot", "image/png", screenshotPath);
        }

        // Stop tracing — save zip on failure, discard on pass.
        string? tracePath = null;
        if (failed)
        {
            Directory.CreateDirectory("playwright-traces");
            tracePath = Path.Combine(
                "playwright-traces",
                $"{TestContext.CurrentContext.Test.ClassName}.{TestContext.CurrentContext.Test.Name}.zip");
        }
        Context.Tracing.StopAsync(new TracingStopOptions { Path = tracePath }).GetAwaiter().GetResult();

        if (failed && tracePath != null)
            AllureApi.AddAttachment("Trace", "application/zip", tracePath);

        // Must close context (not just page) to finalise video.
        Context.CloseAsync().GetAwaiter().GetResult();

        if (_options.RecordVideo)
        {
            if (failed)
                Console.WriteLine($"Video: {Page.Video!.PathAsync().GetAwaiter().GetResult()}");
            else
                Page.Video!.DeleteAsync().GetAwaiter().GetResult();
        }

        Browser.DisposeAsync().GetAwaiter().GetResult();
        Playwright.Dispose();
    }
}
