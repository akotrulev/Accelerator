using Microsoft.Playwright;
using TestCore.Logging;

namespace PlaywrightTests.Locators;

public class SyncLocator
{
    private readonly ILocator _locator;

    public SyncLocator(ILocator locator)
    {
        _locator = locator;
    }

    // Factory / chaining

    public SyncLocator Find(string selector)
    {
        TestLogger.Log($"SyncLocator.Find: selector={selector}");
        return new SyncLocator(_locator.Locator(selector));
    }

    public SyncLocator Nth(int index)
    {
        TestLogger.Log($"SyncLocator.Nth: index={index}");
        return new SyncLocator(_locator.Nth(index));
    }

    public SyncLocator First()
    {
        TestLogger.Log("SyncLocator.First");
        return new SyncLocator(_locator.First);
    }

    public SyncLocator Last()
    {
        TestLogger.Log("SyncLocator.Last");
        return new SyncLocator(_locator.Last);
    }

    public SyncLocator WithText(string text)
    {
        TestLogger.Log($"SyncLocator.WithText: text={text}");
        return new SyncLocator(_locator.Filter(new LocatorFilterOptions { HasText = text }));
    }

    // Actions

    public void Click(LocatorClickOptions? options = null)
    {
        TestLogger.Log("SyncLocator.Click");
        _locator.ClickAsync(options).GetAwaiter().GetResult();
    }

    public void DblClick(LocatorDblClickOptions? options = null)
    {
        TestLogger.Log("SyncLocator.DblClick");
        _locator.DblClickAsync(options).GetAwaiter().GetResult();
    }

    public void Fill(string value, LocatorFillOptions? options = null)
    {
        TestLogger.Log($"SyncLocator.Fill: value={value}");
        _locator.FillAsync(value, options).GetAwaiter().GetResult();
    }

    public void Clear(LocatorClearOptions? options = null)
    {
        TestLogger.Log("SyncLocator.Clear");
        _locator.ClearAsync(options).GetAwaiter().GetResult();
    }

    public void Type(string text, LocatorPressSequentiallyOptions? options = null)
    {
        TestLogger.Log($"SyncLocator.Type: text={text}");
        _locator.PressSequentiallyAsync(text, options).GetAwaiter().GetResult();
    }

    public void Press(string key, LocatorPressOptions? options = null)
    {
        TestLogger.Log($"SyncLocator.Press: key={key}");
        _locator.PressAsync(key, options).GetAwaiter().GetResult();
    }

    public void Hover(LocatorHoverOptions? options = null)
    {
        TestLogger.Log("SyncLocator.Hover");
        _locator.HoverAsync(options).GetAwaiter().GetResult();
    }

    public void Focus(LocatorFocusOptions? options = null)
    {
        TestLogger.Log("SyncLocator.Focus");
        _locator.FocusAsync(options).GetAwaiter().GetResult();
    }

    public void Check(LocatorCheckOptions? options = null)
    {
        TestLogger.Log("SyncLocator.Check");
        _locator.CheckAsync(options).GetAwaiter().GetResult();
    }

    public void Uncheck(LocatorUncheckOptions? options = null)
    {
        TestLogger.Log("SyncLocator.Uncheck");
        _locator.UncheckAsync(options).GetAwaiter().GetResult();
    }

    public void SelectOption(string value, LocatorSelectOptionOptions? options = null)
    {
        TestLogger.Log($"SyncLocator.SelectOption: value={value}");
        _locator.SelectOptionAsync(value, options).GetAwaiter().GetResult();
    }

    public void SelectOption(SelectOptionValue value, LocatorSelectOptionOptions? options = null)
    {
        TestLogger.Log("SyncLocator.SelectOption: value=<SelectOptionValue>");
        _locator.SelectOptionAsync(value, options).GetAwaiter().GetResult();
    }

    public void SelectOption(string[] values, LocatorSelectOptionOptions? options = null)
    {
        TestLogger.Log($"SyncLocator.SelectOption: values=[{string.Join(", ", values)}]");
        _locator.SelectOptionAsync(values, options).GetAwaiter().GetResult();
    }

    public void ScrollIntoView(LocatorScrollIntoViewIfNeededOptions? options = null)
    {
        TestLogger.Log("SyncLocator.ScrollIntoView");
        _locator.ScrollIntoViewIfNeededAsync(options).GetAwaiter().GetResult();
    }

    public void Tap(LocatorTapOptions? options = null)
    {
        TestLogger.Log("SyncLocator.Tap");
        _locator.TapAsync(options).GetAwaiter().GetResult();
    }

    public void DispatchEvent(string type, object? eventInit = null, LocatorDispatchEventOptions? options = null)
    {
        TestLogger.Log($"SyncLocator.DispatchEvent: type={type}");
        _locator.DispatchEventAsync(type, eventInit, options).GetAwaiter().GetResult();
    }

    public void DragTo(SyncLocator target, LocatorDragToOptions? options = null)
    {
        TestLogger.Log("SyncLocator.DragTo");
        _locator.DragToAsync(target._locator, options).GetAwaiter().GetResult();
    }

    // Queries

    public string? TextContent(LocatorTextContentOptions? options = null)
    {
        TestLogger.Log("SyncLocator.TextContent");
        return _locator.TextContentAsync(options).GetAwaiter().GetResult();
    }

    public string InnerHTML(LocatorInnerHTMLOptions? options = null)
    {
        TestLogger.Log("SyncLocator.InnerHTML");
        return _locator.InnerHTMLAsync(options).GetAwaiter().GetResult();
    }

    public string InnerText(LocatorInnerTextOptions? options = null)
    {
        TestLogger.Log("SyncLocator.InnerText");
        return _locator.InnerTextAsync(options).GetAwaiter().GetResult();
    }

    public string InputValue(LocatorInputValueOptions? options = null)
    {
        TestLogger.Log("SyncLocator.InputValue");
        return _locator.InputValueAsync(options).GetAwaiter().GetResult();
    }

    public string? GetAttribute(string name, LocatorGetAttributeOptions? options = null)
    {
        TestLogger.Log($"SyncLocator.GetAttribute: name={name}");
        return _locator.GetAttributeAsync(name, options).GetAwaiter().GetResult();
    }

    public bool IsVisible(LocatorIsVisibleOptions? options = null)
    {
        TestLogger.Log("SyncLocator.IsVisible");
        return _locator.IsVisibleAsync(options).GetAwaiter().GetResult();
    }

    public bool IsHidden(LocatorIsHiddenOptions? options = null)
    {
        TestLogger.Log("SyncLocator.IsHidden");
        return _locator.IsHiddenAsync(options).GetAwaiter().GetResult();
    }

    public bool IsEnabled(LocatorIsEnabledOptions? options = null)
    {
        TestLogger.Log("SyncLocator.IsEnabled");
        return _locator.IsEnabledAsync(options).GetAwaiter().GetResult();
    }

    public bool IsDisabled(LocatorIsDisabledOptions? options = null)
    {
        TestLogger.Log("SyncLocator.IsDisabled");
        return _locator.IsDisabledAsync(options).GetAwaiter().GetResult();
    }

    public bool IsChecked(LocatorIsCheckedOptions? options = null)
    {
        TestLogger.Log("SyncLocator.IsChecked");
        return _locator.IsCheckedAsync(options).GetAwaiter().GetResult();
    }

    public bool IsEditable(LocatorIsEditableOptions? options = null)
    {
        TestLogger.Log("SyncLocator.IsEditable");
        return _locator.IsEditableAsync(options).GetAwaiter().GetResult();
    }

    public int Count()
    {
        TestLogger.Log("SyncLocator.Count");
        return _locator.CountAsync().GetAwaiter().GetResult();
    }

    public IReadOnlyList<string> AllTextContents()
    {
        TestLogger.Log("SyncLocator.AllTextContents");
        return _locator.AllTextContentsAsync().GetAwaiter().GetResult();
    }

    public IReadOnlyList<string> AllInnerTexts()
    {
        TestLogger.Log("SyncLocator.AllInnerTexts");
        return _locator.AllInnerTextsAsync().GetAwaiter().GetResult();
    }

    // Waits

    public void WaitFor(LocatorWaitForOptions? options = null)
    {
        TestLogger.Log("SyncLocator.WaitFor");
        _locator.WaitForAsync(options).GetAwaiter().GetResult();
    }

    public void WaitForVisible()
    {
        TestLogger.Log("SyncLocator.WaitForVisible");
        _locator.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible }).GetAwaiter().GetResult();
    }

    public void WaitForHidden()
    {
        TestLogger.Log("SyncLocator.WaitForHidden");
        _locator.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Hidden }).GetAwaiter().GetResult();
    }

    // Screenshot

    public byte[] Screenshot(LocatorScreenshotOptions? options = null)
    {
        TestLogger.Log("SyncLocator.Screenshot");
        return _locator.ScreenshotAsync(options).GetAwaiter().GetResult();
    }

    public void ScreenshotToFile(string path, LocatorScreenshotOptions? options = null)
    {
        TestLogger.Log($"SyncLocator.ScreenshotToFile: path={path}");
        var opts = options ?? new LocatorScreenshotOptions();
        opts.Path = path;
        _locator.ScreenshotAsync(opts).GetAwaiter().GetResult();
    }

    // Evaluate

    public T Evaluate<T>(string expression, object? arg = null)
    {
        TestLogger.Log($"SyncLocator.Evaluate: expression={expression}");
        return _locator.EvaluateAsync<T>(expression, arg).GetAwaiter().GetResult();
    }

    public void EvaluateVoid(string expression, object? arg = null)
    {
        TestLogger.Log($"SyncLocator.EvaluateVoid: expression={expression}");
        _locator.EvaluateAsync(expression, arg).GetAwaiter().GetResult();
    }
}

public static class SyncLocatorExtensions
{
    public static SyncLocator Sync(this ILocator locator) => new SyncLocator(locator);
}
