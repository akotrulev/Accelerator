using AppiumTests.Logging;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;

namespace AppiumTests.Appium.Pages;

public abstract class BaseAppiumPage
{
    protected IWebDriver Driver { get; }

    protected BaseAppiumPage(IWebDriver driver)
    {
        Driver = driver;
    }

    protected IWebElement FindByAccessibilityId(string id)
    {
        TestLogger.Log($"FindByAccessibilityId: id={id}");
        return Driver.FindElement(MobileBy.AccessibilityId(id));
    }

    protected IWebElement FindById(string id)
    {
        TestLogger.Log($"FindById: id={id}");
        return Driver.FindElement(MobileBy.Id(id));
    }

    protected IWebElement FindByXPath(string xpath)
    {
        TestLogger.Log($"FindByXPath: xpath={xpath}");
        return Driver.FindElement(MobileBy.XPath(xpath));
    }
}
