using Microsoft.Extensions.Configuration;
using TestCore.Config;

namespace AppiumTests.Config;

public static class ConfigurationLoader
{
    public static IConfiguration Root { get; } = BaseConfigurationLoader.BuildConfiguration();

    public static TestEnvironmentOptions GetTestEnvironment() =>
        Root.GetSection(TestEnvironmentOptions.SectionName).Get<TestEnvironmentOptions>() ?? new TestEnvironmentOptions();

    public static BrowserStackOptions GetBrowserStackOptions() =>
        Root.GetSection(BrowserStackOptions.SectionName).Get<BrowserStackOptions>() ?? new BrowserStackOptions();
}
