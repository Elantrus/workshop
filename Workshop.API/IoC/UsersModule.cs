using System.Reflection;
using MediatR;
using Users.Core.Services;

namespace Workshop.API.IoC;

public static class UsersModule
{
    public static void AddUsers(this IServiceCollection serviceCollection, bool development)
    {
        serviceCollection.AddMediatR(typeof(Users.Application.Features.AuthenticateUser).Assembly);
        serviceCollection.AddUsersDatabase(development);
    }
}