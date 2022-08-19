using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Users.Core.Entities;
using Users.Core.Extensions;

namespace Users.Infrastructure.Data.Seed;

public static class AdminSeeder
{
    public static EntityTypeBuilder<Administrator> HasAdmin(this EntityTypeBuilder<Administrator> builder)
    {
        builder.HasData(new List<Administrator>()
        {
            new Administrator
            {
                Email = "system@lazaro.com",
                Password = "S1St3em".Hash(),
                RoleId = 1,
                FullName = "System Admin",
                UserId = 1
            }
        });

        return builder;
    }
}