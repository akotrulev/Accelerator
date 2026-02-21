using Accelerator.Tests.Config;
using Accelerator.Tests.Pages;
using Xunit;

namespace Accelerator.Tests;

public class HomePageTests : BaseTest
{
    [Fact]
    public async Task HomePage_Loads_And_DisplaysHeading()
    {
        var env = ConfigurationLoader.GetTestEnvironment();
        var baseUrl = string.IsNullOrEmpty(env.BaseUrl) ? "https://example.com" : env.BaseUrl;
        var homePage = new HomePage(Page);
        await homePage.GoToAsync(baseUrl);

        var heading = await homePage.GetHeadingTextAsync();
        Assert.Contains("Example", heading);
    }
}
