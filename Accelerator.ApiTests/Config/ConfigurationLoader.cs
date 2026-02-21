using Microsoft.Extensions.Configuration;

namespace Accelerator.ApiTests.Config;

public static class ConfigurationLoader
{
    public static IConfiguration Root { get; } = new ConfigurationBuilder()
        .SetBasePath(AppContext.BaseDirectory)
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
        .AddJsonFile("appsettings.Test.json", optional: true, reloadOnChange: false)
        .AddEnvironmentVariables()
        .Build();

    public static TestEnvironmentOptions GetTestEnvironment() =>
        Root.GetSection(TestEnvironmentOptions.SectionName).Get<TestEnvironmentOptions>() ?? new TestEnvironmentOptions();
}
