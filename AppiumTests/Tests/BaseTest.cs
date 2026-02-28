using AppiumTests.Appium;
using AppiumTests.Appium.Config;
using AppiumTests.Config;
using NUnit.Framework;
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

    [OneTimeTearDown]
    public void StopAppiumServer()
    {
        _appiumService?.Dispose();
    }

    [SetUp]
    public void SetUp()
    {
        Driver = ExecutionMode.Equals("BrowserStack", StringComparison.OrdinalIgnoreCase)
            ? CreateBrowserStackDriver()
            : CreateLocalDriver();
    }

    [TearDown]
    public void TearDown()
    {
        Driver?.Quit();
        Driver?.Dispose();
    }

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

    private static AppiumDriver CreateBrowserStackDriver()
    {
        var bs = ConfigurationLoader.GetBrowserStackOptions();
        return TestType.Equals("NativeApp", StringComparison.OrdinalIgnoreCase)
            ? AppiumDriverFactory.CreateBrowserStackNativeDriver(bs, TargetPlatform)
            : AppiumDriverFactory.CreateBrowserStackBrowserDriver(bs, TargetPlatform);
    }
}
