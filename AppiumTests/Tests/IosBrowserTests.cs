using AppiumTests.Config;
using NUnit.Framework;

namespace AppiumTests.Tests;

[TestFixture]
public class IosBrowserTests : BaseTest
{
    [Test]
    public void OpenChrome_OnIosSimulator_PageTitleIsNotEmpty()
    {
        var env = ConfigurationLoader.GetTestEnvironment();
        Driver.Navigate().GoToUrl(env.BaseUrl);
        Assert.That(Driver.Title, Is.Not.Empty);
    }
}
