using ApiTests.Clients;
using NUnit.Framework;

namespace ApiTests;

[TestFixture]
public class JsonPlaceholderApiTests
{
    [Test]
    public void GetPosts_ReturnsOk()
    {
        var client = new JsonPlaceholderClient();
        var response = client.GetPosts();

        Assert.That(response.IsSuccessful, Is.True);
        Assert.That(response.Content, Is.Not.Null);
    }

    [Test]
    public void GetPost_ById_ReturnsOk()
    {
        var client = new JsonPlaceholderClient();
        var response = client.GetPost(1);

        Assert.That(response.IsSuccessful, Is.True);
        Assert.That(response.Content, Is.Not.Null);
    }
}
