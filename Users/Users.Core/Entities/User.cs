using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
using Users.Core.Exceptions;
using Users.Core.Extensions;

namespace Users.Core.Entities;

public class User
{
    [Key]
    public long UserId { get; set; }
    
    [Required]
    [MinLength(5)]
    [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")]
    public string? Email { get; set; }
    
    [Required]
    [MinLength(5)]
    public string? FullName { get; set; }
    
    [Required]
    [MinLength(8)]
    public string? Password { get; set; }
    
    [ForeignKey(nameof(Role))]
    public long RoleId { get; set; }
    
    public virtual Role? Role { get; set; }
    
    public Guid? RefreshToken { get; set; }
    
    protected void WithEmail(string? email)
    {
        if (string.IsNullOrEmpty(email) || !Regex.IsMatch(email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
            throw new InvalidEmailException();

        Email = email;
    }
        
    protected void WithName(string? fullname)
    {
        if (string.IsNullOrWhiteSpace(fullname) || fullname.Length < 5)
            throw new FullNameTooShortException();

        FullName = fullname;
    }

    protected void WithRole(Role? role)
    {
        Role = role;
    }
    
    protected void WithPassword(string? password)
    {
        if (string.IsNullOrEmpty(password) || password.Length < 8) throw new PasswordTooShortException();
        
        if (!Regex.IsMatch(password, "(?=.*\\d)")) throw new PasswordIsTooWeakException("um número.");
        if (!Regex.IsMatch(password, "(?=.*[a-z])")) throw new PasswordIsTooWeakException("um caractére.");
        if (!Regex.IsMatch(password, "(?=.*[A-Z])")) throw new PasswordIsTooWeakException("um caractére maiúsculo.");
        if (!Regex.IsMatch(password, "(?=.*[@#$%])")) throw new PasswordIsTooWeakException("um simbolo.");

        Password = password.Hash();
    }
}