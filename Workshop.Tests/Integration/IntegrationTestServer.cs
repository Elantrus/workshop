using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Users.Application.Features;

namespace Workshop.Tests.Integration;

public class IntegrationTestServer
{
    public readonly HttpClient Client;

    public IntegrationTestServer()
    {
        var webFactory = new WebApplicationFactory<Program>();

        Client = webFactory.CreateDefaultClient();
    }

    public async Task<CreateCustomer.Result> CreateCustomer(string email, string password)
    {
        var createUserCommand = new CreateCustomer.Command
        {
            Email = email,
            Name = "Lazaro Junior",
            Password = password
        };

        var response = await this.Client.PostAsJsonAsync("/api/customer", createUserCommand);
        var serializedResponseContent = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<CreateCustomer.Result>(serializedResponseContent);
    }

    public async Task LoginAsSystem()
    {
        var authenticationCommand = new AuthenticateUser.Command
        {
            Email = "system@lazaro.com",
            Password = "S1St3em"
        };
        
        var result = await Post<AuthenticateUser.Result>("/api/authentication", authenticationCommand);

        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", result.Token);
    }
    
    public async Task CreateCustomerAndLogin(string email, string password)
    {
        var createCustomerResult =  await CreateCustomer(email, password);
        
        var authenticationCommand = new AuthenticateUser.Command
        {
            Email = email,
            Password = password
        };

        var result = await Post<AuthenticateUser.Result>("/api/authentication", authenticationCommand);

        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", result.Token);
    }

    public async Task<TResult> Post<TResult>(string requestUri, object payload)
    {
        var response = await this.Client.PostAsJsonAsync(requestUri, payload);
        var serializedResponseContent = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<TResult>(serializedResponseContent);
    }
    
    public async Task<TResult> Get<TResult>(string requestUri)
    {
        var response = await this.Client.GetAsync(requestUri);
        var serializedResponseContent = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<TResult>(serializedResponseContent);
    }
}