using TestCore.Logging;
using RestSharp;

namespace ApiTests.Clients;

public abstract class BaseApiClient : RestClient
{
    protected BaseApiClient(string baseUri) : base(new RestClientOptions(baseUri)) { }

    // Sends a GET request to the given path and returns the raw response.
    protected RestResponse Get(string path)
    {
        TestLogger.Log($"GET {path}");
        return ExecuteAsync(new RestRequest(path)).GetAwaiter().GetResult();
    }

    // Sends a POST request with an optional JSON body.
    protected RestResponse Post(string path, object? body = null)
    {
        TestLogger.Log($"POST {path}");
        var request = new RestRequest(path, Method.Post);
        if (body != null) request.AddJsonBody(body);
        return ExecuteAsync(request).GetAwaiter().GetResult();
    }

    // Sends a PUT request with an optional JSON body.
    protected RestResponse Put(string path, object? body = null)
    {
        TestLogger.Log($"PUT {path}");
        var request = new RestRequest(path, Method.Put);
        if (body != null) request.AddJsonBody(body);
        return ExecuteAsync(request).GetAwaiter().GetResult();
    }

    // Sends a DELETE request to the given path.
    protected RestResponse Delete(string path)
    {
        TestLogger.Log($"DELETE {path}");
        return ExecuteAsync(new RestRequest(path, Method.Delete)).GetAwaiter().GetResult();
    }
}
