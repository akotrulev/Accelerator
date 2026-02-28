namespace PlaywrightTests.Config;

public class TestEnvironmentOptions
{
    public const string SectionName = "TestEnvironment";

    public string BaseUrl { get; set; } = "";
    public string ApiUri { get; set; } = "";
    public string Username { get; set; } = "";
    public string Password { get; set; } = "";
}
