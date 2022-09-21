using MediatR;
using Microsoft.EntityFrameworkCore;
using Repairing.Infrastructure.Data;
using Users.Infrastructure.Data;

namespace Workshop.API.IoC;

public static class RepairingModule
{
    public static void AddRepairing(this IServiceCollection serviceCollection, bool development)
    {
        serviceCollection.AddRepairingDatabase(development);
    }

    private static void AddRepairingDatabase(this IServiceCollection services, bool development)
    {
        string databaseName = $"workshop.repairing";
        if (development)
        {
            services.AddDbContext<RepairingDbContext>(options =>
            {
                options.UseInMemoryDatabase(databaseName);
            });
        }
        else
        {
            var cn = Environment.GetEnvironmentVariable("WORKSHOP_DB");

            if (string.IsNullOrWhiteSpace(cn))
                cn = $"Data Source=localhost\\SQLEXPRESS;Initial Catalog={databaseName};User=sharp;Password=sharp";
            
            services.AddDbContext<RepairingDbContext>(options =>
            {
                options.UseSqlServer(cn);
            });
        }
        
    }
}