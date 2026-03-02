using Microsoft.Playwright;
using PlaywrightTests.Logging;

namespace PlaywrightTests.Pages;

public class SyncFrameLocator
{
    private readonly IFrameLocator _frame;

    public SyncFrameLocator(IFrameLocator frame) { _frame = frame; }

    // Returns a SyncLocator for elements matching the selector within this frame.
    public SyncLocator Find(string selector)
    {
        TestLogger.Log($"SyncFrameLocator.Find: selector={selector}");
        return new SyncLocator(_frame.Locator(selector));
    }

    // Returns a SyncLocator for elements containing the given text within this frame.
    public SyncLocator FindByText(string text, FrameLocatorGetByTextOptions? options = null)
    {
        TestLogger.Log($"SyncFrameLocator.FindByText: text={text}");
        return new SyncLocator(_frame.GetByText(text, options));
    }

    // Returns a SyncLocator for elements with the given ARIA role within this frame.
    public SyncLocator FindByRole(AriaRole role, FrameLocatorGetByRoleOptions? options = null)
    {
        TestLogger.Log($"SyncFrameLocator.FindByRole: role={role}");
        return new SyncLocator(_frame.GetByRole(role, options));
    }

    // Returns a SyncLocator for form elements associated with the given label within this frame.
    public SyncLocator FindByLabel(string label, FrameLocatorGetByLabelOptions? options = null)
    {
        TestLogger.Log($"SyncFrameLocator.FindByLabel: label={label}");
        return new SyncLocator(_frame.GetByLabel(label, options));
    }

    // Returns a SyncLocator for input elements with the given placeholder within this frame.
    public SyncLocator FindByPlaceholder(string placeholder, FrameLocatorGetByPlaceholderOptions? options = null)
    {
        TestLogger.Log($"SyncFrameLocator.FindByPlaceholder: placeholder={placeholder}");
        return new SyncLocator(_frame.GetByPlaceholder(placeholder, options));
    }

    // Returns a SyncLocator for elements with the given data-testid attribute within this frame.
    public SyncLocator FindByTestId(string testId)
    {
        TestLogger.Log($"SyncFrameLocator.FindByTestId: testId={testId}");
        return new SyncLocator(_frame.GetByTestId(testId));
    }

    // Returns a SyncLocator for elements with the given title attribute within this frame.
    public SyncLocator FindByTitle(string title, FrameLocatorGetByTitleOptions? options = null)
    {
        TestLogger.Log($"SyncFrameLocator.FindByTitle: title={title}");
        return new SyncLocator(_frame.GetByTitle(title, options));
    }

    // Returns a SyncLocator for image elements with the given alt text within this frame.
    public SyncLocator FindByAltText(string altText, FrameLocatorGetByAltTextOptions? options = null)
    {
        TestLogger.Log($"SyncFrameLocator.FindByAltText: altText={altText}");
        return new SyncLocator(_frame.GetByAltText(altText, options));
    }

    // Returns a SyncFrameLocator for a nested iframe matching the given selector.
    public SyncFrameLocator GetFrame(string selector)
    {
        TestLogger.Log($"SyncFrameLocator.GetFrame: selector={selector}");
        return new SyncFrameLocator(_frame.FrameLocator(selector));
    }
}
