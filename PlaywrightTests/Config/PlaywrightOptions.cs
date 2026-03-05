namespace PlaywrightTests.Config;

public class PlaywrightOptions
{
    public const string SectionName = "PlaywrightSettings";

    public string BrowserType     { get; set; } = "chromium";
    public bool   Headless        { get; set; } = false;
    public int    ViewportWidth   { get; set; } = 1920;
    public int    ViewportHeight  { get; set; } = 1080;

    // Slow-motion delay in milliseconds applied between Playwright actions (0 = disabled).
    public float SlowMo           { get; set; } = 0;

    // Default timeout in milliseconds for all Playwright actions and assertions (0 = use Playwright default).
    public float DefaultTimeoutMs { get; set; } = 0;

    // Set to false to disable video recording (e.g. for local debugging runs).
    public bool RecordVideo       { get; set; } = true;
}
