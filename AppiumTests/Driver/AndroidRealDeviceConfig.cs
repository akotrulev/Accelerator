using OpenQA.Selenium.Appium;

namespace AppiumTests.Driver;

public class AndroidRealDeviceConfig
{
    public static AppiumOptions CreateOptions(string udid, string? appPackage = null, string? appActivity = null)
    {
        var options = new AppiumOptions
        {
            PlatformName = "Android",
            AutomationName = "UiAutomator2"
        };

        options.AddAdditionalAppiumOption("udid", udid);
        options.AddAdditionalAppiumOption("noReset", true);

        if (!string.IsNullOrEmpty(appPackage))
            options.AddAdditionalAppiumOption("appPackage", appPackage);

        if (!string.IsNullOrEmpty(appActivity))
            options.AddAdditionalAppiumOption("appActivity", appActivity);

        return options;
    }
}
