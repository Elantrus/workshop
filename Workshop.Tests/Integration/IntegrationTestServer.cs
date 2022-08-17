using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Workshop.Tests.Integration;

public class IntegrationTestServer
{
    public readonly HttpClient Client;

    public IntegrationTestServer()
    {
        var webFactory = new WebApplicationFactory<Program>();

        Client = webFactory.CreateDefaultClient();
    }
}