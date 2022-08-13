using System.Reflection;
using MediatR;
using Users.Core.Services;

namespace Workshop.API.IoC;

public static class UsersModule
{
    public static void AddUsers(this IServiceCollection serviceCollection, bool development)
    {
        serviceCollection.AddMediatR(typeof(Users.Application.Features.CreateCustomerApplicationHandler).Assembly);
        serviceCollection.AddUsersDatabase(development);
        serviceCollection.AddSingleton<ITokenService>(provider =>
        {
            string? environmentJwtPassword = null;

            if (!development)
            {
                environmentJwtPassword = Environment.GetEnvironmentVariable("jwt_security") ??
                                             throw new Exception(
                                                 "Para produção a variável jwt_security deve ser definida.");
            }

            return string.IsNullOrEmpty(environmentJwtPassword)
                ? new TokenService("E)H@McQfTjWnZr4u7x!A%D*G-JaNdRgU")
                : new TokenService(environmentJwtPassword);
        });
    }
}