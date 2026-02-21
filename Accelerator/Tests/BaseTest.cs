using Accelerator.Appium;
using Xunit;

namespace Accelerator.Tests;

public class BaseTest
{
    [Fact]
    public void test()
    {
// Create iOS driver
        var iosDriver = AppiumDriverFactory.CreateIosDriver(appPath: "/path/to/app.app");

// Create Android driver
        var androidDriver = AppiumDriverFactory.CreateAndroidDriver(
            appPath: "/path/to/app.apk",
            appPackage: "com.example.app",
            appActivity: ".MainActivity");
    }
}