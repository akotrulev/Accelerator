using OpenQA.Selenium;
using OpenQA.Selenium.Appium;

namespace Accelerator.Appium.Pages;

public abstract class BaseAppiumPage
{
    protected IWebDriver Driver { get; }

    protected BaseAppiumPage(IWebDriver driver)
    {
        Driver = driver;
    }

    protected IWebElement FindByAccessibilityId(string id) =>
        Driver.FindElement(MobileBy.AccessibilityId(id));

    protected IWebElement FindById(string id) =>
        Driver.FindElement(MobileBy.Id(id));

    protected IWebElement FindByXPath(string xpath) =>
        Driver.FindElement(MobileBy.XPath(xpath));
}
