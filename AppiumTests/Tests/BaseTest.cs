using Allure.Net.Commons;
using AppiumTests.Config;
using AppiumTests.Driver;
using AppiumTests.Enums;
using TestCore.Logging;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Service;


namespace AppiumTests.Tests;

[TestFixture]

public abstract class BaseTest
{
    private AppiumLocalService? _appiumService;
    protected AppiumDriver Driver { get; private set; } = null!;

    // Starts a local Appium server before any test in the fixture runs; skipped when running on BrowserStack.
    [OneTimeSetUp]
    public void StartAppiumServer()
    {
        if (AppiumRunSettings.ExecutionMode.Equals("BrowserStack", StringComparison.OrdinalIgnoreCase))
            return;

        _appiumService = new AppiumServiceBuilder()
            .WithIPAddress(AppiumSettings.ServerHost)
            .UsingAnyFreePort()
            .Build();
        _appiumService.Start();
    }

    // Disposes the local Appium server after all tests in the fixture have finished.
    [OneTimeTearDown]
    public void StopAppiumServer()
    {
        _appiumService?.Dispose();
    }

    // Clears the action log and creates a fresh driver before each test.
    [SetUp]
    public void SetUp()
    {
        TestLogger.Clear();

        var serverUrl = _appiumService?.ServiceUrl?.ToString();
        var browser   = AppiumRunSettings.TargetPlatform == MobilePlatform.iOS ? Browser.Safari : Browser.Chrome;

        Driver = AppiumRunSettings.TestType.Equals("NativeApp", StringComparison.OrdinalIgnoreCase)
            ? AppiumDriverFactory.CreateNativeAppDriver(AppiumRunSettings.TargetPlatform, serverUrl)
            : AppiumDriverFactory.CreateBrowserDriver(AppiumRunSettings.TargetPlatform, browser, serverUrl);
    }

    // On failure: dumps the action log and captures a screenshot as an Allure attachment. Always quits the driver.
    [TearDown]
    public void TearDown()
    {
        if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
        {
            TestContext.Out.WriteLine("--- Test Action Log ---");
            foreach (var entry in TestLogger.GetLogs())
                TestContext.Out.WriteLine(entry);
            TestContext.Out.WriteLine("--- End of Log ---");

            // Screenshot → Allure attachment
            var screenshotPath = Path.Combine(
                "failures",
                $"{TestContext.CurrentContext.Test.Name}.png");
            Directory.CreateDirectory("failures");
            ((ITakesScreenshot)Driver).GetScreenshot().SaveAsFile(screenshotPath);
            AllureApi.AddAttachment("Screenshot", "image/png", screenshotPath);
        }

        Driver?.Quit();
        Driver?.Dispose();
    }
}
