using System.Linq;
using Microsoft.Playwright;
using PlaywrightTests.Logging;

namespace PlaywrightTests.Pages;

public abstract class BasePage
{
    protected IPage Page { get; }

    protected BasePage(IPage page)
    {
        Page = page;
    }

    // Navigates the browser to the given URL and waits for the navigation to complete.
    protected void NavigateTo(string url)
    {
        TestLogger.Log($"NavigateTo: url={url}");
        Page.GotoAsync(url).GetAwaiter().GetResult();
    }

    // Returns a SyncLocator for elements matching the given CSS/XPath selector.
    protected SyncLocator Find(string selector)
    {
        TestLogger.Log($"Find: selector={selector}");
        return new SyncLocator(Page.Locator(selector));
    }

    // Returns a SyncLocator for elements containing the given text.
    protected SyncLocator FindByText(string text, PageGetByTextOptions? options = null)
    {
        TestLogger.Log($"FindByText: text={text}");
        return new SyncLocator(Page.GetByText(text, options));
    }

    // Returns a SyncLocator for elements with the given ARIA role.
    protected SyncLocator FindByRole(AriaRole role, PageGetByRoleOptions? options = null)
    {
        TestLogger.Log($"FindByRole: role={role}");
        return new SyncLocator(Page.GetByRole(role, options));
    }

    // Returns a SyncLocator for form elements associated with the given label.
    protected SyncLocator FindByLabel(string label, PageGetByLabelOptions? options = null)
    {
        TestLogger.Log($"FindByLabel: label={label}");
        return new SyncLocator(Page.GetByLabel(label, options));
    }

    // Returns a SyncLocator for input elements with the given placeholder text.
    protected SyncLocator FindByPlaceholder(string placeholder, PageGetByPlaceholderOptions? options = null)
    {
        TestLogger.Log($"FindByPlaceholder: placeholder={placeholder}");
        return new SyncLocator(Page.GetByPlaceholder(placeholder, options));
    }

    // Returns a SyncLocator for elements with the given data-testid attribute.
    protected SyncLocator FindByTestId(string testId)
    {
        TestLogger.Log($"FindByTestId: testId={testId}");
        return new SyncLocator(Page.GetByTestId(testId));
    }

    // Returns a SyncLocator for elements with the given title attribute.
    protected SyncLocator FindByTitle(string title, PageGetByTitleOptions? options = null)
    {
        TestLogger.Log($"FindByTitle: title={title}");
        return new SyncLocator(Page.GetByTitle(title, options));
    }

    // Returns a SyncLocator for image elements with the given alt text.
    protected SyncLocator FindByAltText(string altText, PageGetByAltTextOptions? options = null)
    {
        TestLogger.Log($"FindByAltText: altText={altText}");
        return new SyncLocator(Page.GetByAltText(altText, options));
    }

    // Returns a SyncFrameLocator for the iframe matching the given selector.
    protected SyncFrameLocator GetFrame(string selector)
    {
        TestLogger.Log($"GetFrame: selector={selector}");
        return new SyncFrameLocator(Page.FrameLocator(selector));
    }

    // Returns all cookies visible to the current page.
    protected IReadOnlyList<BrowserContextCookiesResult> GetCookies()
    {
        TestLogger.Log("GetCookies");
        return Page.Context.CookiesAsync().GetAwaiter().GetResult();
    }

    // Returns the value of the cookie with the given name, or null if absent.
    protected string? GetCookie(string name)
    {
        TestLogger.Log($"GetCookie: name={name}");
        return Page.Context.CookiesAsync().GetAwaiter().GetResult()
                   .FirstOrDefault(c => c.Name == name)?.Value;
    }

    // Adds a cookie to the current browser context.
    protected void AddCookie(Cookie cookie)
    {
        TestLogger.Log($"AddCookie: name={cookie.Name}");
        Page.Context.AddCookiesAsync([cookie]).GetAwaiter().GetResult();
    }

    // Clears all cookies from the current browser context.
    protected void ClearCookies()
    {
        TestLogger.Log("ClearCookies");
        Page.Context.ClearCookiesAsync().GetAwaiter().GetResult();
    }

    // Returns the value of a localStorage item by key, or null if absent.
    protected string? GetLocalStorage(string key)
    {
        TestLogger.Log($"GetLocalStorage: key={key}");
        return Page.EvaluateAsync<string?>($"localStorage.getItem({System.Text.Json.JsonSerializer.Serialize(key)})")
                   .GetAwaiter().GetResult();
    }

    // Sets a localStorage item.
    protected void SetLocalStorage(string key, string value)
    {
        TestLogger.Log($"SetLocalStorage: key={key}");
        Page.EvaluateAsync($"localStorage.setItem({System.Text.Json.JsonSerializer.Serialize(key)}, {System.Text.Json.JsonSerializer.Serialize(value)})")
            .GetAwaiter().GetResult();
    }

    // Removes a localStorage item by key.
    protected void RemoveLocalStorage(string key)
    {
        TestLogger.Log($"RemoveLocalStorage: key={key}");
        Page.EvaluateAsync($"localStorage.removeItem({System.Text.Json.JsonSerializer.Serialize(key)})")
            .GetAwaiter().GetResult();
    }

    // Clears all localStorage items.
    protected void ClearLocalStorage()
    {
        TestLogger.Log("ClearLocalStorage");
        Page.EvaluateAsync("localStorage.clear()").GetAwaiter().GetResult();
    }
}
