using Microsoft.Extensions.Configuration;

namespace TestCore.Config;

public static class BaseConfigurationLoader
{
    // Loads appsettings.json, an environment-specific overlay, and environment variables.
    // envVarName is the environment variable that holds the current environment name (e.g. "TEST_ENV").
    public static IConfiguration BuildConfiguration(string envVarName = "TEST_ENV")
    {
        var env = Environment.GetEnvironmentVariable(envVarName) ?? "Test";
        return new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
            .AddJsonFile($"appsettings.{env}.json", optional: true, reloadOnChange: false)
            .AddEnvironmentVariables()
            .Build();
    }
}
