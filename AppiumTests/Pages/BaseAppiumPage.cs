using TestCore.Logging;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;

namespace AppiumTests.Pages;

public abstract class BaseAppiumPage
{
    protected IWebDriver Driver { get; }

    protected BaseAppiumPage(IWebDriver driver)
    {
        Driver = driver;
    }

    protected AppiumElement FindByAccessibilityId(string id)
    {
        TestLogger.Log($"FindByAccessibilityId: id={id}");
        return new AppiumElement(Driver.FindElement(MobileBy.AccessibilityId(id)));
    }

    protected AppiumElement FindById(string id)
    {
        TestLogger.Log($"FindById: id={id}");
        return new AppiumElement(Driver.FindElement(MobileBy.Id(id)));
    }

    protected AppiumElement FindByXPath(string xpath)
    {
        TestLogger.Log($"FindByXPath: xpath={xpath}");
        return new AppiumElement(Driver.FindElement(MobileBy.XPath(xpath)));
    }
}
