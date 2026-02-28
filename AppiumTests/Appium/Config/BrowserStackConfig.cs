using OpenQA.Selenium.Appium;
using AppiumTests.Config;

namespace AppiumTests.Appium.Config;

public static class BrowserStackConfig
{
    public static AppiumOptions CreateAndroidBrowserOptions(BrowserStackOptions bs)
    {
        var options = new AppiumOptions
        {
            PlatformName = "Android",
            AutomationName = "UiAutomator2",
            DeviceName = bs.AndroidDeviceName,
            BrowserName = bs.AndroidBrowserName
        };
        options.AddAdditionalAppiumOption("bstack:options", BuildBstackOptions(bs, bs.AndroidDeviceName, bs.AndroidOsVersion));
        return options;
    }

    public static AppiumOptions CreateIosBrowserOptions(BrowserStackOptions bs)
    {
        var options = new AppiumOptions
        {
            PlatformName = "iOS",
            AutomationName = "XCUITest",
            DeviceName = bs.IosDeviceName,
            BrowserName = bs.IosBrowserName
        };
        options.AddAdditionalAppiumOption("bstack:options", BuildBstackOptions(bs, bs.IosDeviceName, bs.IosOsVersion));
        return options;
    }

    public static AppiumOptions CreateAndroidNativeOptions(BrowserStackOptions bs)
    {
        var options = new AppiumOptions
        {
            PlatformName = "Android",
            AutomationName = "UiAutomator2",
            DeviceName = bs.AndroidDeviceName,
            App = bs.AndroidAppUrl
        };
        options.AddAdditionalAppiumOption("bstack:options", BuildBstackOptions(bs, bs.AndroidDeviceName, bs.AndroidOsVersion));
        return options;
    }

    public static AppiumOptions CreateIosNativeOptions(BrowserStackOptions bs)
    {
        var options = new AppiumOptions
        {
            PlatformName = "iOS",
            AutomationName = "XCUITest",
            DeviceName = bs.IosDeviceName,
            App = bs.IosAppUrl
        };
        options.AddAdditionalAppiumOption("bstack:options", BuildBstackOptions(bs, bs.IosDeviceName, bs.IosOsVersion));
        return options;
    }

    private static Dictionary<string, object> BuildBstackOptions(BrowserStackOptions bs, string deviceName, string osVersion) =>
        new()
        {
            ["userName"]      = bs.UserName,
            ["accessKey"]     = bs.AccessKey,
            ["deviceName"]    = deviceName,
            ["osVersion"]     = osVersion,
            ["projectName"]   = bs.ProjectName,
            ["buildName"]     = bs.BuildName,
            ["sessionName"]   = bs.SessionName,
            ["appiumVersion"] = "2.0.0"
        };
}
