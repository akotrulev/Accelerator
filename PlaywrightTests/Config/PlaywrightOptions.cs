namespace PlaywrightTests.Config;

public class PlaywrightOptions
{
    public const string SectionName = "PlaywrightSettings";

    public string BrowserType { get; set; } = "chromium";
    public bool Headless { get; set; } = false;
    public int ViewportWidth { get; set; } = 1920;
    public int ViewportHeight { get; set; } = 1080;
}
