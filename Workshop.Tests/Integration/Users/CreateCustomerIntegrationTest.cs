using System.Net.Http.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NUnit.Framework;
using Users.Contracts;

namespace Workshop.Tests.Integration.Users;

public class CreateCustomerIntegrationTest
{
    [Test]
    public async Task Test_CreateCustomerShouldReturnUserId()
    {
        var testServer = new IntegrationTestServer();

        var createUserCommand = new CreateCustomer.CreateCustomerCommand
        {
            Email = "testuser@gmail.com",
            Name = "Lazaro Junior",
            Password = "Pass0wor0d@@"
        };

        var response = await testServer.Client.PostAsJsonAsync("/api/customer", createUserCommand);
        var serializedResponseContent = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<CreateCustomer.CreateCustomerResult>(serializedResponseContent);
        
        Assert.True(response.IsSuccessStatusCode);
        Assert.NotNull(result);
        Assert.NotZero(result.UserId);
    }
}