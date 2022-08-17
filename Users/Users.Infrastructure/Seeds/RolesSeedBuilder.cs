using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Users.Core.Entities;

namespace Users.Infrastructure.Seeds;

public static class RolesSeedBuilder
{
    public static EntityTypeBuilder<Role> HasRoles(this EntityTypeBuilder<Role> builder)
    {
        builder.HasData(new List<Role>()
        {
            new Role
            {
                RoleId = 1,
                Name = "admin"
            },
            new Role
            {
                RoleId = 2,
                Name = "customer"
            }
        });

        return builder;
    }
}
