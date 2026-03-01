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
}
