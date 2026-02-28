namespace AppiumTests.Config;

public class BrowserStackOptions
{
    public const string SectionName = "BrowserStack";

    public string UserName { get; set; } = "";
    public string AccessKey { get; set; } = "";

    // Android real device
    public string AndroidDeviceName { get; set; } = "Samsung Galaxy S23";
    public string AndroidOsVersion { get; set; } = "13.0";
    public string AndroidBrowserName { get; set; } = "chrome";
    public string AndroidAppUrl { get; set; } = "";   // bs://... for native; set via env var

    // iOS real device
    public string IosDeviceName { get; set; } = "iPhone 14";
    public string IosOsVersion { get; set; } = "16";
    public string IosBrowserName { get; set; } = "safari";
    public string IosAppUrl { get; set; } = "";       // bs://... for native; set via env var

    // Session metadata shown in BrowserStack dashboard
    public string ProjectName { get; set; } = "Accelerator";
    public string BuildName { get; set; } = "Local Build";
    public string SessionName { get; set; } = "AppiumTests";

    // Computed — never stored as plain text in config
    public string HubUrl => $"https://{UserName}:{AccessKey}@hub-cloud.browserstack.com/wd/hub";
}
