using System.Net;
using System.Threading.Tasks;
using NUnit.Framework;
using Users.Application.Features;

namespace Workshop.Tests.Integration.Users;

public class RoleIntegrationTest
{
    [Test]
    public async Task Test_RoleShouldBeCreated()
    {
        var testServer = new IntegrationTestServer();
        await testServer.LoginAsSystem();
        var createRoleCommand = new CreateRole.CreateRoleCommand
        {
            Name = "CustomRole"
        };
        var result = await testServer.Post<CreateRole.CreateRoleResult>("/api/role", createRoleCommand);
        Assert.IsTrue(result.Success);
    }
    
    [Test]
    public async Task Test_CreateRoleShouldReturnForbidden()
    {
        var testServer = new IntegrationTestServer();
        await testServer.CreateCustomerAndLogin("teste@gmail.com", "sTr30Fas@#");

        var createRoleCommand = new CreateRole.CreateRoleCommand
        {
            Name = "CustomRole"
        };
        
        var result = await testServer.Post<object>("/api/role", createRoleCommand);
        Assert.IsFalse(result.Success);
        Assert.IsTrue(result.StatusCode == HttpStatusCode.Forbidden);
    }
    
    [Test]
    public async Task Test_AdminShouldHaveNewRole()
    {
        var testServer = new IntegrationTestServer();
        await testServer.LoginAsSystem();
        var createCustomerResponse = await testServer.CreateCustomer("teste@gmail.com", "sTr30Fas@#");
        var createRoleCommand = new CreateRole.CreateRoleCommand
        {
            Name = "CustomRole"
        };
        var createRoleResponse = await testServer.Post<CreateRole.CreateRoleResult>("/api/role", createRoleCommand);
        var assignRoleToAdminCommand = new AssignAdministratorRole.AssignAdministratorRoleCommand()
        {
            RoleId = createRoleResponse.DeserializedResponse.RoleId,
            UserId = createCustomerResponse.UserId
        };
        
        var result = await testServer.Patch<AssignAdministratorRole.AssignAdministratorRoleCommand>("/api/role", createRoleCommand);
        
        Assert.IsFalse(result.Success);
    }
}