using Microsoft.Extensions.Configuration;

namespace PlaywrightTests.Config;

public static class ConfigurationLoader
{
    public static IConfiguration Root { get; } = BuildConfiguration();

    private static IConfiguration BuildConfiguration()
    {
        var env = Environment.GetEnvironmentVariable("TEST_ENV") ?? "Test";
        return new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
            .AddJsonFile($"appsettings.{env}.json", optional: true, reloadOnChange: false)
            .AddEnvironmentVariables()
            .Build();
    }

    public static TestEnvironmentOptions GetTestEnvironment() =>
        Root.GetSection(TestEnvironmentOptions.SectionName).Get<TestEnvironmentOptions>() ?? new TestEnvironmentOptions();

    public static PlaywrightOptions GetPlaywrightOptions() =>
        Root.GetSection(PlaywrightOptions.SectionName).Get<PlaywrightOptions>() ?? new PlaywrightOptions();
}
