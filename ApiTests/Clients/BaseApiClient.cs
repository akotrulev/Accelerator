using TestCore.Logging;
using RestSharp;

namespace ApiTests.Clients;

public abstract class BaseApiClient : RestClient
{
    protected BaseApiClient(string baseUri) : base(new RestClientOptions(baseUri)) { }

    // Constructor overload that sets a bearer token as a default Authorization header.
    protected BaseApiClient(string baseUri, string bearerToken) : this(baseUri)
    {
        SetDefaultHeader("Authorization", $"Bearer {bearerToken}");
    }

    // Adds or replaces a default header sent with every request from this client.
    public void SetDefaultHeader(string name, string value) =>
        this.AddDefaultHeader(name, value);

    private void LogResponse(string method, string path, RestResponse response)
    {
        TestLogger.Log($"{method} {path} -> HTTP {(int)response.StatusCode} ({response.StatusCode})");
        if (!response.IsSuccessful)
            TestLogger.Log(
                $"Response body: {(response.Content?.Length > 500 ? response.Content[..500] + "…" : response.Content)}",
                LogLevel.Warning);
    }

    // Sends a GET request to the given path and returns the raw response.
    protected RestResponse Get(string path)
    {
        var response = ExecuteAsync(new RestRequest(path)).GetAwaiter().GetResult();
        LogResponse("GET", path, response);
        return response;
    }

    // Sends a POST request with an optional JSON body.
    protected RestResponse Post(string path, object? body = null)
    {
        var request = new RestRequest(path, Method.Post);
        if (body != null) request.AddJsonBody(body);
        var response = ExecuteAsync(request).GetAwaiter().GetResult();
        LogResponse("POST", path, response);
        return response;
    }

    // Sends a PUT request with an optional JSON body.
    protected RestResponse Put(string path, object? body = null)
    {
        var request = new RestRequest(path, Method.Put);
        if (body != null) request.AddJsonBody(body);
        var response = ExecuteAsync(request).GetAwaiter().GetResult();
        LogResponse("PUT", path, response);
        return response;
    }

    // Sends a PATCH request with an optional JSON body.
    protected RestResponse Patch(string path, object? body = null)
    {
        var request = new RestRequest(path, Method.Patch);
        if (body != null) request.AddJsonBody(body);
        var response = ExecuteAsync(request).GetAwaiter().GetResult();
        LogResponse("PATCH", path, response);
        return response;
    }

    // Sends a DELETE request to the given path.
    protected RestResponse Delete(string path)
    {
        var response = ExecuteAsync(new RestRequest(path, Method.Delete)).GetAwaiter().GetResult();
        LogResponse("DELETE", path, response);
        return response;
    }
}
