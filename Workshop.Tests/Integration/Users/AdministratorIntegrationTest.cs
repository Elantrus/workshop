using System.Net.Http.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NUnit.Framework;
using Users.Application.Features;

namespace Workshop.Tests.Integration.Users;

public class CreateAdministratorIntegrationTest
{
    [Test]
    public async Task Test_CreateAdministratorShouldReturnUserId()
    {
        var testServer = new IntegrationTestServer();
        await testServer.LoginAsSystem();

        var createUserCommand = new CreateAdmin.CreateAdminCommand()
        {
            Email = "testuser@gmail.com",
            Name = "Lazaro Junior",
            Password = "Pass0wor0d@@"
        };

        var response = await testServer.Client.PostAsJsonAsync("/api/administrator", createUserCommand);
        var serializedResponseContent = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<CreateAdmin.CreateAdminResult>(serializedResponseContent);
        
        Assert.True(response.IsSuccessStatusCode);
        Assert.NotNull(result);
        Assert.NotZero(result.UserId);
    }
}