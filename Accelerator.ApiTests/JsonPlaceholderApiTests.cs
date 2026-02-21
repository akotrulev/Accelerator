using Accelerator.ApiTests.Clients;
using Xunit;

namespace Accelerator.ApiTests;

public class JsonPlaceholderApiTests
{
    [Fact]
    public void GetPosts_ReturnsOk()
    {
        var client = new JsonPlaceholderClient();
        var response = client.GetPosts();

        Assert.True(response.IsSuccessful);
        Assert.NotNull(response.Content);
    }

    [Fact]
    public void GetPost_ById_ReturnsOk()
    {
        var client = new JsonPlaceholderClient();
        var response = client.GetPost(1);

        Assert.True(response.IsSuccessful);
        Assert.NotNull(response.Content);
    }
}
