using Microsoft.Playwright;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using PlaywrightTests.Config;
using PlaywrightTests.Logging;

namespace PlaywrightTests;

[TestFixture]
public abstract class BaseTest
{
    protected IPlaywright Playwright { get; private set; } = null!;
    protected IBrowser Browser { get; private set; } = null!;
    protected IBrowserContext Context { get; private set; } = null!;
    protected IPage Page { get; private set; } = null!;

    [SetUp]
    public void SetUp()
    {
        TestLogger.Clear();
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

        Context = Browser.NewContextAsync(new BrowserNewContextOptions
        {
            ViewportSize    = new ViewportSize { Width = options.ViewportWidth, Height = options.ViewportHeight },
            RecordVideoDir  = "videos/",
            RecordVideoSize = new RecordVideoSize { Width = options.ViewportWidth, Height = options.ViewportHeight }
        }).GetAwaiter().GetResult();

        Context.Tracing.StartAsync(new TracingStartOptions
        {
            Title       = $"{TestContext.CurrentContext.Test.ClassName}.{TestContext.CurrentContext.Test.Name}",
            Screenshots = true,
            Snapshots   = true,
            Sources      = true
        }).GetAwaiter().GetResult();

        Page = Context.NewPageAsync().GetAwaiter().GetResult();

        Page.Console += (_, msg) => Console.WriteLine(msg.Text);
        Page.PageError += (_, err) => Console.Error.WriteLine(err);

        Page.Console += (_, msg) =>
        {
            if (msg.Type == "error")
                Console.WriteLine($"[ERROR] {msg.Text}");
        };
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
            Page.ScreenshotAsync(new PageScreenshotOptions
            {
                Path     = $"failures/{TestContext.CurrentContext.Test.Name}.png",
                FullPage = true
            }).GetAwaiter().GetResult();
        }

        // Stop tracing — save zip on failure, discard on pass (Path = null)
        string? tracePath = null;
        if (failed)
        {
            Directory.CreateDirectory("playwright-traces");
            tracePath = Path.Combine(
                "playwright-traces",
                $"{TestContext.CurrentContext.Test.ClassName}.{TestContext.CurrentContext.Test.Name}.zip");
        }
        Context.Tracing.StopAsync(new TracingStopOptions { Path = tracePath })
               .GetAwaiter().GetResult();

        // Must close context (not just page) to finalise video
        Context.CloseAsync().GetAwaiter().GetResult();

        if (failed)
            Console.WriteLine($"Video: {Page.Video!.PathAsync().GetAwaiter().GetResult()}");
        else
            Page.Video!.DeleteAsync().GetAwaiter().GetResult();

        Browser.DisposeAsync().GetAwaiter().GetResult();
        Playwright.Dispose();
    }
}
