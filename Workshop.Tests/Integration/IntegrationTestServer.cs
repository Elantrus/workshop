using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Users.Contracts;

namespace Workshop.Tests.Integration;

public class IntegrationTestServer
{
    public readonly HttpClient Client;

    public IntegrationTestServer()
    {
        var webFactory = new WebApplicationFactory<Program>();

        Client = webFactory.CreateDefaultClient();
    }

    public async Task<CreateCustomer.CreateCustomerResult> CreateCustomer(string email, string password)
    {
        var createUserCommand = new CreateCustomer.CreateCustomerCommand
        {
            Email = email,
            Name = "Lazaro Junior",
            Password = password
        };

        var response = await this.Client.PostAsJsonAsync("/api/customer", createUserCommand);
        var serializedResponseContent = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<CreateCustomer.CreateCustomerResult>(serializedResponseContent);
    }
    
    public async Task CreateCustomerAndLogin(string email, string password)
    {
        var createCustomerResult =  await CreateCustomer(email, password);
        
        var authenticationCommand = new AuthenticateUser.AuthenticateUserCommand
        {
            Email = email,
            Password = password
        };

        var result = await Post<AuthenticateUser.AuthenticateUserResult>("/api/authentication", authenticationCommand);

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