using MediatR;
using Microsoft.EntityFrameworkCore;
using Repairing.Infrastructure.Data;
using Users.Infrastructure.Data;
using Workshop.Common;

namespace Workshop.API.IoC;

public static class RepairingModule
{
    public static void AddRepairing(this IServiceCollection serviceCollection, IWebHostEnvironment environment)
    {
        //serviceCollection.AddMediatR(typeof(Repairing.Application.Features).Assembly);

        if (environment.IsDevelopment())
            serviceCollection.AddInMemoryRepairingDatabase();
        else
            serviceCollection.AddRepairingDatabase(environment);
    }
    
    public static void UseMigrateRepairing(this WebApplication app, IWebHostEnvironment environment)
    {
        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetService<UsersDbContext>();

            if (dbContext == null) throw new NullReferenceException();
            
            if(!environment.IsDevelopment())
                dbContext.Database.Migrate();

            dbContext.Database.EnsureCreated();
        }
    }

    private static void AddInMemoryRepairingDatabase(this IServiceCollection services)
    {
        services.AddDbContext<UsersDbContext>(options =>
        {
            options.UseInMemoryDatabase("workshop.repairing");
        });
    }
    private static void AddRepairingDatabase(this IServiceCollection services, IWebHostEnvironment environment)
    {
        var connectionString = $"Data Source={EnvVariables.WorkshopDbHost}\\SQLEXPRESS;Initial Catalog=workshop.repairing;User=sharp;Password=sharp";

        services.AddDbContext<UsersDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });
    }
}