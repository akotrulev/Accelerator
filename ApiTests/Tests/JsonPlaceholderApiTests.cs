using ApiTests.Clients;
using NUnit.Framework;

namespace ApiTests.Tests;

[TestFixture]
public class JsonPlaceholderApiTests : BaseApiTest
{
    [Test]
    public void GetPosts_ReturnsOk()
    {
        var client = new ExampleResource();
        var response = client.GetAll();

        Assert.That(response.IsSuccessful, Is.True);
        Assert.That(response.Content, Is.Not.Null);
    }

    [Test]
    public void GetPost_ById_ReturnsOk()
    {
        var client = new ExampleResource();
        var response = client.GetById(1);

        Assert.That(response.IsSuccessful, Is.True);
        Assert.That(response.Content, Is.Not.Null);
    }
}
