using AppiumTests.Appium;
using AppiumTests.Appium.Config;
using NUnit.Framework;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Appium.Service;

namespace AppiumTests.Tests;

[TestFixture]
public abstract class BaseTest
{
    private AppiumLocalService _appiumService = null!;
    protected IOSDriver Driver { get; private set; } = null!;

    [OneTimeSetUp]
    public void StartAppiumServer()
    {
        var serverUri = new Uri(AppiumSettings.ServerUrl);
        _appiumService = new AppiumServiceBuilder()
            .WithIPAddress(serverUri.Host)
            .UsingPort(serverUri.Port)
            .Build();
        _appiumService.Start();
    }

    [OneTimeTearDown]
    public void StopAppiumServer()
    {
        _appiumService?.Dispose();
    }

    [SetUp]
    public void SetUp()
    {
        Driver = AppiumDriverFactory.CreateIosBrowserDriver(serverUrl: _appiumService.ServiceUrl.ToString());
    }

    [TearDown]
    public void TearDown()
    {
        Driver?.Quit();
        Driver?.Dispose();
    }
}
