using OpenQA.Selenium.Appium;

namespace AppiumTests.Appium.Config;

public class IosSimulatorConfig
{
    public const string DeviceName = "iPhone 16e";
    public const string PlatformVersion = "26.2";
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

    public static AppiumOptions CreateBrowserOptions(string browserName = "Chrome")
    {
        var options = new AppiumOptions
        {
            PlatformName = "iOS",
            AutomationName = "XCUITest",
            DeviceName = DeviceName,
            PlatformVersion = PlatformVersion,
            BrowserName = browserName
        };

        if (!string.IsNullOrEmpty(Udid))
            options.AddAdditionalAppiumOption("udid", Udid);

        return options;
    }
}
