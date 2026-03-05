using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.iOS;
using AppiumTests.Config;
using AppiumTests.Enums;

namespace AppiumTests.Driver;

public static class AppiumDriverFactory
{
    /// <summary>
    /// Creates a browser driver for the given platform.
    /// Whether to connect locally or via BrowserStack is determined by AppiumRunSettings.ExecutionMode.
    /// </summary>
    public static AppiumDriver CreateBrowserDriver(MobilePlatform platform, Browser browser, string? serverUrl = null)
    {
        if (AppiumRunSettings.ExecutionMode.Equals("BrowserStack", StringComparison.OrdinalIgnoreCase))
        {
            var bs = ConfigurationLoader.GetBrowserStackOptions();
            var hubUri = new Uri(bs.HubUrl);
            return platform == MobilePlatform.iOS
                ? new IOSDriver(hubUri, BrowserStackConfig.CreateIosBrowserOptions(bs))
                : new AndroidDriver(hubUri, BrowserStackConfig.CreateAndroidBrowserOptions(bs));
        }

        var uri = new Uri(serverUrl ?? AppiumSettings.ServerHost);
        return platform == MobilePlatform.iOS
            ? new IOSDriver(uri, IosSimulatorConfig.CreateBrowserOptions(browser.ToString()))
            : new AndroidDriver(uri, AndroidEmulatorConfig.CreateBrowserOptions(browser.ToString()));
    }

    /// <summary>
    /// Creates a native app driver for the given platform.
    /// Whether to connect locally or via BrowserStack is determined by AppiumRunSettings.ExecutionMode.
    /// App path for local runs is read from the APP_PATH environment variable.
    /// </summary>
    public static AppiumDriver CreateNativeAppDriver(MobilePlatform platform, string? serverUrl = null)
    {
        var isIos = platform == MobilePlatform.iOS;

        if (AppiumRunSettings.ExecutionMode.Equals("BrowserStack", StringComparison.OrdinalIgnoreCase))
        {
            var bs = ConfigurationLoader.GetBrowserStackOptions();
            var hubUri = new Uri(bs.HubUrl);
            return isIos
                ? new IOSDriver(hubUri, BrowserStackConfig.CreateIosNativeOptions(bs))
                : new AndroidDriver(hubUri, BrowserStackConfig.CreateAndroidNativeOptions(bs));
        }

        var appPath = Environment.GetEnvironmentVariable(EnvVars.AppPath);
        var uri = new Uri(serverUrl ?? AppiumSettings.ServerHost);
        return isIos
            ? new IOSDriver(uri, IosSimulatorConfig.CreateOptions(appPath))
            : new AndroidDriver(uri, AndroidEmulatorConfig.CreateOptions(appPath));
    }
}
