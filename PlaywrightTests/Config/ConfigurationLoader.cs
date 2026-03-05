using Microsoft.Extensions.Configuration;
using TestCore.Config;

namespace PlaywrightTests.Config;

public static class ConfigurationLoader
{
    public static IConfiguration Root { get; } = BaseConfigurationLoader.BuildConfiguration();

    // Reads the TestEnvironment config section and returns it as a strongly-typed options object.
    public static TestEnvironmentOptions GetTestEnvironment() =>
        Root.GetSection(TestEnvironmentOptions.SectionName).Get<TestEnvironmentOptions>() ?? new TestEnvironmentOptions();

    // Reads the PlaywrightSettings config section and returns it as a strongly-typed options object.
    public static PlaywrightOptions GetPlaywrightOptions() =>
        Root.GetSection(PlaywrightOptions.SectionName).Get<PlaywrightOptions>() ?? new PlaywrightOptions();
}
