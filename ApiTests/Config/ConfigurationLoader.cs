using Microsoft.Extensions.Configuration;
using TestCore.Config;

namespace ApiTests.Config;

public static class ConfigurationLoader
{
    public static IConfiguration Root { get; } = BaseConfigurationLoader.BuildConfiguration();

    // Reads the TestEnvironment config section and returns it as a strongly-typed options object.
    public static TestEnvironmentOptions GetTestEnvironment() =>
        Root.GetSection(TestEnvironmentOptions.SectionName).Get<TestEnvironmentOptions>() ?? new TestEnvironmentOptions();
}
