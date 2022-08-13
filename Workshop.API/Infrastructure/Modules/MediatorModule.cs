using System.Reflection;
using MediatR;

namespace Workshop.API.Infrastructure.Modules;

public static class MediatorModule
{
    public static void AddMediator(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddMediatR(Assembly.GetExecutingAssembly());
    }
}