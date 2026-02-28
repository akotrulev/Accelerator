using OpenQA.Selenium.Appium;

namespace AppiumTests.Appium.Config;

public class AndroidEmulatorConfig
{
    public const string DeviceName = "Pixel_6_API_34";
    public const string Avd = "Pixel_6_API_34";

    public static AppiumOptions CreateOptions(string? appPath = null, string? appPackage = null, string? appActivity = null)
    {
        var options = new AppiumOptions
        {
            PlatformName = "Android",
            AutomationName = "UiAutomator2",
            DeviceName = DeviceName
        };

        options.AddAdditionalAppiumOption("avd", Avd);

        if (!string.IsNullOrEmpty(appPath))
            options.App = appPath;

        if (!string.IsNullOrEmpty(appPackage))
            options.AddAdditionalAppiumOption("appPackage", appPackage);

        if (!string.IsNullOrEmpty(appActivity))
            options.AddAdditionalAppiumOption("appActivity", appActivity);

        return options;
    }
}
