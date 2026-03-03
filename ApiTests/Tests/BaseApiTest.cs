using TestCore.Logging;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using RestSharp;

namespace ApiTests.Tests;

[TestFixture]
public abstract class BaseApiTest
{
    protected RestClient Client { get; }

    protected BaseApiTest() { Client = null!; }

    protected BaseApiTest(string baseUrl)
    {
        Client = new RestClient(new RestClientOptions(baseUrl));
    }

    [SetUp]
    public void SetUp() => TestLogger.Clear();

    [TearDown]
    public void TearDown()
    {
        if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
        {
            TestContext.Out.WriteLine("--- Test Action Log ---");
            foreach (var entry in TestLogger.GetLogs())
                TestContext.Out.WriteLine(entry);
            TestContext.Out.WriteLine("--- End of Log ---");
        }
    }
}
