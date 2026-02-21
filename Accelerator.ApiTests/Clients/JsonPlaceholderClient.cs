using Accelerator.ApiTests.Config;
using RestSharp;

namespace Accelerator.ApiTests.Clients;

public class JsonPlaceholderClient : RestClient
{
    public JsonPlaceholderClient() : this(ConfigurationLoader.GetTestEnvironment().ApiUri) { }

    public JsonPlaceholderClient(string baseUrl) : base(new RestClientOptions(string.IsNullOrEmpty(baseUrl) ? "https://jsonplaceholder.typicode.com" : baseUrl)) { }

    public RestResponse GetPosts() =>
        ExecuteAsync(new RestRequest("posts")).GetAwaiter().GetResult();

    public RestResponse GetPost(int id) =>
        ExecuteAsync(new RestRequest($"posts/{id}")).GetAwaiter().GetResult();
}
