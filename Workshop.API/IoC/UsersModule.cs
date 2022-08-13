using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Users.Infrastructure.IoC;

public static class UsersModule
{
    public static void AddUsers(this IServiceCollection serviceCollection, bool development)
    {
        serviceCollection.AddMediatR(typeof(Users.Application));
        serviceCollection.AddUsersDatabase(development);
    }
}