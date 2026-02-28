using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.iOS;
using Config = AppiumTests.Appium.Config;

namespace AppiumTests.Appium;

public static class AppiumDriverFactory
{
    private static string DefaultServerUrl => Config.AppiumSettings.ServerUrl;

    public static AndroidDriver CreateAndroidDriver(string? appPath = null, string? appPackage = null, string? appActivity = null, string? serverUrl = null)
    {
        var options = Config.AndroidEmulatorConfig.CreateOptions(appPath, appPackage, appActivity);
        var uri = new Uri(serverUrl ?? DefaultServerUrl);
        return new AndroidDriver(uri, options);
    }

    public static IOSDriver CreateIosDriver(string? appPath = null, string? serverUrl = null)
    {
        var options = Config.IosSimulatorConfig.CreateOptions(appPath);
        var uri = new Uri(serverUrl ?? DefaultServerUrl);
        return new IOSDriver(uri, options);
    }

    public static IOSDriver CreateIosBrowserDriver(string browserName = "Safari", string? serverUrl = null)
    {
        var options = Config.IosSimulatorConfig.CreateBrowserOptions(browserName);
        var uri = new Uri(serverUrl ?? DefaultServerUrl);
        return new IOSDriver(uri, options);
    }
}
