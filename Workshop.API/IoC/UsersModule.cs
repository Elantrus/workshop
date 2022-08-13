using System.Reflection;
using MediatR;

namespace Workshop.API.IoC;

public static class UsersModule
{
    public static void AddUsers(this IServiceCollection serviceCollection, bool development)
    {
        serviceCollection.AddMediatR(typeof(Users.Application.Features.CreateUserApplicationHandler).Assembly);
        serviceCollection.AddUsersDatabase(development);
    }
}