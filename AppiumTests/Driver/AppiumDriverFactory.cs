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
        if (AppiumRunSettings.ExecutionMode == ExecutionMode.BrowserStack)
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

        if (AppiumRunSettings.ExecutionMode == ExecutionMode.BrowserStack)
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

    /// <summary>
    /// Creates a driver for a real physical device connected via USB.
    /// The device is identified by DEVICE_UDID; the pre-installed app is activated via
    /// BUNDLE_ID (iOS) or APP_PACKAGE + APP_ACTIVITY (Android). No app install is performed.
    /// </summary>
    public static AppiumDriver CreateUsbDeviceDriver(MobilePlatform platform, string? serverUrl = null)
    {
        var udid = Environment.GetEnvironmentVariable(EnvVars.DeviceUdid)
            ?? throw new InvalidOperationException($"Environment variable '{EnvVars.DeviceUdid}' is required for UsbDevice mode.");

        var uri = new Uri(serverUrl ?? AppiumSettings.ServerHost);

        if (platform == MobilePlatform.iOS)
        {
            var bundleId = Environment.GetEnvironmentVariable(EnvVars.BundleId);
            return new IOSDriver(uri, IosRealDeviceConfig.CreateOptions(udid, bundleId));
        }
        else
        {
            var appPackage  = Environment.GetEnvironmentVariable(EnvVars.AppPackage);
            var appActivity = Environment.GetEnvironmentVariable(EnvVars.AppActivity);
            return new AndroidDriver(uri, AndroidRealDeviceConfig.CreateOptions(udid, appPackage, appActivity));
        }
    }
}
