using ApiTests.Config;
using RestSharp;

namespace ApiTests.Clients;

public class ExampleResource : BaseApiClient
{
    // Base path constants for each endpoint in this resource.
    public const string All = "items";
    public const string ById = "items/{id}";

    public ExampleResource() : base(ConfigurationLoader.GetTestEnvironment().ApiUri) { }

    // Returns all items.
    public RestResponse GetAll() => Get(All);

    // Returns a single item by id.
    public RestResponse GetById(int id) => Get($"items/{id}");

    // Creates a new item.
    public RestResponse Create(object body) => Post(All, body);

    // Replaces the item with the given id.
    public RestResponse Update(int id, object body) => Put($"items/{id}", body);

    // Deletes the item with the given id.
    public RestResponse Remove(int id) => Delete($"items/{id}");
}
