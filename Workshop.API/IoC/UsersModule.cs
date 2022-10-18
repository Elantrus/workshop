using System.Reflection;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Users.Core.Services;
using Users.Infrastructure.Data;
using Workshop.Common;

namespace Workshop.API.IoC;

public static class UsersModule
{
    
    public static void AddUsers(this IServiceCollection serviceCollection, IWebHostEnvironment environment)
    {
        serviceCollection.AddMediatR(typeof(Users.Application.Features.AssignAdministratorRole).Assembly);

        if (environment.IsDevelopment())
            serviceCollection.AddInMemoryUsersDatabase();
        else
            serviceCollection.AddUsersDatabase(environment);
    }
    
    public static void UseMigrateUsers(this WebApplication app, IWebHostEnvironment environment)
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

    private static void AddInMemoryUsersDatabase(this IServiceCollection services)
    {
        services.AddDbContext<UsersDbContext>(options =>
        {
            options.UseInMemoryDatabase("workshop.users");
        });
    }
    private static void AddUsersDatabase(this IServiceCollection services, IWebHostEnvironment environment)
    {
        var connectionString = $"Data Source={EnvVariables.WorkshopDbHost}\\SQLEXPRESS;Initial Catalog=workshop.users;User=sharp;Password=sharp";

        services.AddDbContext<UsersDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });
    }
    
}