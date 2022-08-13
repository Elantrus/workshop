using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Users.Infrastructure.Data;

namespace Users.Infrastructure.IoC;

public static class DatabaseModule
{
    public static void MigrateAndSeedUsers(this WebApplication app, bool development)
    {
        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetService<UsersDbContext>();

            if (dbContext == null) throw new NullReferenceException();
            
            if(development)
                dbContext.Database.Migrate();
            
        }
    }
    
    internal static void AddUsersDatabase(this IServiceCollection services, bool development)
    {
        if (development)
        {
            services.AddDbContext<UsersDbContext>(options =>
            {
                options.UseInMemoryDatabase("workshop.users");
            });
        }
        else
        {
            var cn = Environment.GetEnvironmentVariable("WORKSHOP_DB");

            if (string.IsNullOrWhiteSpace(cn))
                cn = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=workshop.users;User=sharp;Password=sharp";
            
            services.AddDbContext<UsersDbContext>(options =>
            {
                options.UseSqlServer(cn);
            });
        }
        
    }
}