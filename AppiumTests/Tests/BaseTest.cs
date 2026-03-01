using Allure.Net.Commons;
using AppiumTests.Appium;
using AppiumTests.Appium.Config;
using AppiumTests.Config;
using AppiumTests.Logging;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Service;

namespace AppiumTests.Tests;

[TestFixture]
public abstract class BaseTest
{
    private static readonly string ExecutionMode =
        Environment.GetEnvironmentVariable("EXECUTION_MODE") ?? "Local";

    private static readonly string TargetPlatform =
        Environment.GetEnvironmentVariable("TARGET_PLATFORM") ?? "Android";

    private static readonly string TestType =
        Environment.GetEnvironmentVariable("TEST_TYPE") ?? "Browser";

    private AppiumLocalService? _appiumService;
    protected AppiumDriver Driver { get; private set; } = null!;

    // Starts a local Appium server before any test in the fixture runs; skipped when running on BrowserStack.
    [OneTimeSetUp]
    public void StartAppiumServer()
    {
        if (ExecutionMode.Equals("BrowserStack", StringComparison.OrdinalIgnoreCase))
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
        Driver = ExecutionMode.Equals("BrowserStack", StringComparison.OrdinalIgnoreCase)
            ? CreateBrowserStackDriver()
            : CreateLocalDriver();
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

    // Builds a local AppiumDriver for the target platform and test type using the running Appium service.
    private AppiumDriver CreateLocalDriver()
    {
        var serverUrl = _appiumService!.ServiceUrl.ToString();
        var isIos = TargetPlatform.Equals("iOS", StringComparison.OrdinalIgnoreCase);
        var appPath = Environment.GetEnvironmentVariable("APP_PATH");

        if (TestType.Equals("NativeApp", StringComparison.OrdinalIgnoreCase))
            return isIos
                ? AppiumDriverFactory.CreateIosDriver(appPath: appPath, serverUrl: serverUrl)
                : AppiumDriverFactory.CreateAndroidDriver(appPath: appPath, serverUrl: serverUrl);

        return isIos
            ? AppiumDriverFactory.CreateIosBrowserDriver(serverUrl: serverUrl)
            : AppiumDriverFactory.CreateAndroidBrowserDriver(serverUrl: serverUrl);
    }

    // Builds an AppiumDriver connected to BrowserStack using credentials from config.
    private static AppiumDriver CreateBrowserStackDriver()
    {
        var bs = ConfigurationLoader.GetBrowserStackOptions();
        return TestType.Equals("NativeApp", StringComparison.OrdinalIgnoreCase)
            ? AppiumDriverFactory.CreateBrowserStackNativeDriver(bs, TargetPlatform)
            : AppiumDriverFactory.CreateBrowserStackBrowserDriver(bs, TargetPlatform);
    }
}
