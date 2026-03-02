using System.Linq;
using AppiumTests.Logging;
using OpenQA.Selenium;

namespace AppiumTests.Appium.Pages;

public class AppiumElement
{
    private readonly IWebElement _element;

    public AppiumElement(IWebElement element) { _element = element; }

    // Factory / chaining

    // Returns an AppiumElement for the first child element matching the given locator.
    public AppiumElement Find(By by)
    {
        TestLogger.Log($"AppiumElement.Find: by={by}");
        return new AppiumElement(_element.FindElement(by));
    }

    // Returns all child elements matching the given locator as AppiumElements.
    public IReadOnlyList<AppiumElement> FindAll(By by)
    {
        TestLogger.Log($"AppiumElement.FindAll: by={by}");
        return _element.FindElements(by).Select(e => new AppiumElement(e)).ToList();
    }

    // Actions

    // Clicks the element.
    public void Click()
    {
        TestLogger.Log("AppiumElement.Click");
        _element.Click();
    }

    // Clears the content of the element (inputs/textareas).
    public void Clear()
    {
        TestLogger.Log("AppiumElement.Clear");
        _element.Clear();
    }

    // Types the given text into the element.
    public void SendKeys(string text)
    {
        TestLogger.Log($"AppiumElement.SendKeys: text={text}");
        _element.SendKeys(text);
    }

    // Submits a form element or a child of a form element.
    public void Submit()
    {
        TestLogger.Log("AppiumElement.Submit");
        _element.Submit();
    }

    // Queries

    // Returns the visible text content of the element.
    public string GetText()
    {
        TestLogger.Log("AppiumElement.GetText");
        return _element.Text;
    }

    // Returns the tag name of the element.
    public string GetTagName()
    {
        TestLogger.Log("AppiumElement.GetTagName");
        return _element.TagName;
    }

    // Returns the value of the given HTML attribute, or null if absent.
    public string? GetAttribute(string name)
    {
        TestLogger.Log($"AppiumElement.GetAttribute: name={name}");
        return _element.GetAttribute(name);
    }

    // Returns the value of the given DOM attribute, or null if absent.
    public string? GetDomAttribute(string name)
    {
        TestLogger.Log($"AppiumElement.GetDomAttribute: name={name}");
        return _element.GetDomAttribute(name);
    }

    // Returns the computed CSS value of the given property.
    public string GetCssValue(string propertyName)
    {
        TestLogger.Log($"AppiumElement.GetCssValue: propertyName={propertyName}");
        return _element.GetCssValue(propertyName);
    }

    // Returns true if the element is rendered and visible.
    public bool IsDisplayed()
    {
        TestLogger.Log("AppiumElement.IsDisplayed");
        return _element.Displayed;
    }

    // Returns true if the element is enabled (not disabled).
    public bool IsEnabled()
    {
        TestLogger.Log("AppiumElement.IsEnabled");
        return _element.Enabled;
    }

    // Returns true if the element is selected (checkboxes, radio buttons, options).
    public bool IsSelected()
    {
        TestLogger.Log("AppiumElement.IsSelected");
        return _element.Selected;
    }
}
