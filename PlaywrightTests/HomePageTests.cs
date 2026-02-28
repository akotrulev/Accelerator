using PlaywrightTests.Config;
using PlaywrightTests.Pages;
using NUnit.Framework;

namespace PlaywrightTests;

[TestFixture]
public class HomePageTests : BaseTest
{
    [Test]
    public void Navigate_ToBaseUrl_TitleIsNotEmpty()
    {
        var env = ConfigurationLoader.GetTestEnvironment();
        var homePage = new HomePage(Page);

        homePage.Navigate(env.BaseUrl);

        Assert.That(homePage.Title(), Is.Not.Empty);
    }
}
