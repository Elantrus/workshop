using System.ComponentModel.DataAnnotations;
using Users.Core.Exceptions;

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
        if (string.IsNullOrEmpty(name)) throw new RoleNameTooShortException();
        Name = name;
    }
}