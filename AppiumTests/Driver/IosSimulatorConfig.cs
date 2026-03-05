using OpenQA.Selenium.Appium;

namespace AppiumTests.Driver;

public class IosSimulatorConfig
{
    public const string DeviceName = "iPhone 16e";
    public const string PlatformVersion = "18.0";
    public const string Udid = ""; // Leave empty for default simulator

    public static AppiumOptions CreateOptions(string? appPath = null)
    {
        var options = new AppiumOptions
        {
            PlatformName = "iOS",
            AutomationName = "XCUITest",
            DeviceName = DeviceName,
            PlatformVersion = PlatformVersion
        };

        if (!string.IsNullOrEmpty(appPath))
            options.App = appPath;

        if (!string.IsNullOrEmpty(Udid))
            options.AddAdditionalAppiumOption("udid", Udid);

        return options;
    }

    public static AppiumOptions CreateBrowserOptions(string browserName = "Safari")
    {
        var options = new AppiumOptions
        {
            PlatformName = "iOS",
            AutomationName = "XCUITest",
            DeviceName = DeviceName,
            PlatformVersion = PlatformVersion,
            BrowserName = browserName
        };

        options.AddAdditionalAppiumOption("webviewConnectTimeout", 2000);

        if (!string.IsNullOrEmpty(Udid))
            options.AddAdditionalAppiumOption("udid", Udid);

        return options;
    }
}
