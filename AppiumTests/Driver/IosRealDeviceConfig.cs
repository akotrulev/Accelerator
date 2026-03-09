using OpenQA.Selenium.Appium;

namespace AppiumTests.Driver;

public class IosRealDeviceConfig
{
    public static AppiumOptions CreateOptions(string udid, string? bundleId = null)
    {
        var options = new AppiumOptions
        {
            PlatformName = "iOS",
            AutomationName = "XCUITest"
        };

        options.AddAdditionalAppiumOption("udid", udid);
        options.AddAdditionalAppiumOption("noReset", true);

        if (!string.IsNullOrEmpty(bundleId))
            options.AddAdditionalAppiumOption("bundleId", bundleId);

        return options;
    }
}
