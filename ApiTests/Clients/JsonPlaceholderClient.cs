using ApiTests.Config;
using ApiTests.Logging;
using RestSharp;

namespace ApiTests.Clients;

public class JsonPlaceholderClient : RestClient
{
    public JsonPlaceholderClient() : this(ConfigurationLoader.GetTestEnvironment().ApiUri) { }

    public JsonPlaceholderClient(string baseUrl) : base(new RestClientOptions(string.IsNullOrEmpty(baseUrl) ? "https://jsonplaceholder.typicode.com" : baseUrl)) { }

    // Fetches all posts from the /posts endpoint and returns the raw response.
    public RestResponse GetPosts()
    {
        TestLogger.Log("GET /posts");
        return ExecuteAsync(new RestRequest("posts")).GetAwaiter().GetResult();
    }

    // Fetches a single post by id from /posts/{id} and returns the raw response.
    public RestResponse GetPost(int id)
    {
        TestLogger.Log($"GET /posts/{id}");
        return ExecuteAsync(new RestRequest($"posts/{id}")).GetAwaiter().GetResult();
    }
}
