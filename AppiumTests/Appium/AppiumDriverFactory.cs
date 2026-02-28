using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.iOS;
using AppiumTests.Config;
using Config = AppiumTests.Appium.Config;

namespace AppiumTests.Appium;

public static class AppiumDriverFactory
{
    private static string DefaultServerUrl => Config.AppiumSettings.ServerHost;

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

    public static AndroidDriver CreateAndroidBrowserDriver(string browserName = "Chrome", string? serverUrl = null)
    {
        var options = Config.AndroidEmulatorConfig.CreateBrowserOptions(browserName);
        var uri = new Uri(serverUrl ?? DefaultServerUrl);
        return new AndroidDriver(uri, options);
    }

    public static AppiumDriver CreateBrowserStackBrowserDriver(BrowserStackOptions bs, string platform)
    {
        var hubUri = new Uri(bs.HubUrl);
        if (platform.Equals("iOS", StringComparison.OrdinalIgnoreCase))
            return new IOSDriver(hubUri, Config.BrowserStackConfig.CreateIosBrowserOptions(bs));
        return new AndroidDriver(hubUri, Config.BrowserStackConfig.CreateAndroidBrowserOptions(bs));
    }

    public static AppiumDriver CreateBrowserStackNativeDriver(BrowserStackOptions bs, string platform)
    {
        var hubUri = new Uri(bs.HubUrl);
        if (platform.Equals("iOS", StringComparison.OrdinalIgnoreCase))
            return new IOSDriver(hubUri, Config.BrowserStackConfig.CreateIosNativeOptions(bs));
        return new AndroidDriver(hubUri, Config.BrowserStackConfig.CreateAndroidNativeOptions(bs));
    }
}
