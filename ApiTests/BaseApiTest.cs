using RestSharp;

namespace ApiTests;

public abstract class BaseApiTest
{
    protected RestClient Client { get; }

    protected BaseApiTest(string baseUrl)
    {
        Client = new RestClient(new RestClientOptions(baseUrl));
    }
}
