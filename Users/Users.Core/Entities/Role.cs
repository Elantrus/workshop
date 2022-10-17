using System.ComponentModel.DataAnnotations;

namespace Users.Core.Entities;

public class Role
{
    [Key]
    public long RoleId { get; set; }
    
    [Required]
    public string? Name { get; set; }

    public List<User> Users { get; set; } = new List<User>();

    public Role()
    {
        //Entity
    }

    public Role(string? name)
    {
        var role = new Role
        {
            Name = name
        };
    }
}