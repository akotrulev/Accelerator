using OpenQA.Selenium.Appium;

namespace Accelerator.Appium.Config;

public class IosSimulatorConfig
{
    public const string DeviceName = "iPhone 16";
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
}
