using System.Threading.Tasks;
using NUnit.Framework;
using Users.Contracts;

namespace Workshop.Tests.Integration.Users;

public class GetCustomerIntegrationTest
{
    [Test]
    public async Task Test_GetCustomerShouldReturnCustomerInfo()
    {
        var testServer = new IntegrationTestServer();
        var email = "spynrt@gmail.com";
        var password = "S4$roNg)9";
        await testServer.CreateCustomerAndLogin(email, password);

        var result = await testServer.Get<GetCustomer.GetCustomerResult>("/api/customer");
        
        Assert.NotNull(result);
        Assert.NotNull(result.Email);
        Assert.NotNull(result.FullName);
    }
}