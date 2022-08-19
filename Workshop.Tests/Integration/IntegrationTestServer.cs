using System.Net;
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

    public async Task<string?> LoginAsSystem()
    {
        var authenticationCommand = new AuthenticateUser.AuthenticateUserCommand
        {
            Email = "system@lazaro.com",
            Password = "S1St3em"
        };
        
        var result = await Post<AuthenticateUser.AuthenticateUserResult>("/api/authentication", authenticationCommand);
        var deserializedResponse = result.DeserializedResponse;

        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", deserializedResponse?.Token);

        return deserializedResponse?.RefreshToken;
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
        var deserializedResponse = result.DeserializedResponse;
        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", deserializedResponse?.Token);
    }

    public async Task<BaseResponse<TResult>> Post<TResult>(string requestUri, object payload)
    {
        var response = await this.Client.PostAsJsonAsync(requestUri, payload);
        var serializedResponseContent = await response.Content.ReadAsStringAsync();

        return SerializeResponseOrError<TResult>(response.IsSuccessStatusCode, serializedResponseContent, response.StatusCode);
    }
    
    public async Task<BaseResponse<TResult>> Patch<TResult>(string requestUri, object payload)
    {
        var stringContent = new StringContent(JsonConvert.SerializeObject(payload));
        var response = await this.Client.PatchAsync(requestUri, stringContent);
        var serializedResponseContent = await response.Content.ReadAsStringAsync();

        return SerializeResponseOrError<TResult>(response.IsSuccessStatusCode, serializedResponseContent, response.StatusCode);
    }
    
    public async Task<BaseResponse<TResult>> Get<TResult>(string requestUri)
    {
        var response = await this.Client.GetAsync(requestUri);
        var serializedResponseContent = await response.Content.ReadAsStringAsync();
        return SerializeResponseOrError<TResult>(response.IsSuccessStatusCode, serializedResponseContent, response.StatusCode);
    }

    private BaseResponse<TResult> SerializeResponseOrError<TResult>(bool responseIsSuccessStatusCode, string serializedResponseContent, HttpStatusCode statusCode)
    {
        if (!responseIsSuccessStatusCode)
        {
            var deserializedBaseResponse = JsonConvert.DeserializeObject<BaseResponse<TResult>>(serializedResponseContent);

            if (deserializedBaseResponse is null) deserializedBaseResponse = new BaseResponse<TResult>();
            deserializedBaseResponse.StatusCode = statusCode;
            deserializedBaseResponse.Success = responseIsSuccessStatusCode;
            return deserializedBaseResponse;
        }

        return new BaseResponse<TResult>()
        {
            DeserializedResponse = JsonConvert.DeserializeObject<TResult>(serializedResponseContent),
            Success = responseIsSuccessStatusCode
        };
    }

   

}