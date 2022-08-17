using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Users.Core.Exceptions;
using Users.Core.Extensions;

namespace Users.Core.Entities;

public class User
{
    [Key]
    public long UserId { get; set; }
    [Required]
    public string? Email { get; set; }
    [Required]
    public string? FullName { get; set; }
    [Required]
    public string? Password { get; set; }
    public virtual Role? Role { get; set; }
    public Guid? RefreshToken { get; set; }
    
    public void WithEmail(string? email)
    {
        if (string.IsNullOrEmpty(email) || !Regex.IsMatch(email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
            throw new InvalidEmailException();

        Email = email;
    }
        
    public void WithName(string? fullname)
    {
        if (string.IsNullOrWhiteSpace(fullname))
            throw new FullNameTooShortException();

        FullName = fullname;
    }
    
    public void WithRole(Role? role)
    {
        Role = role;
    }
    
    public void WithPassword(string? password)
    {
        if (string.IsNullOrEmpty(password) || password.Length < 8) throw new PasswordTooShortException();
        
        if (!Regex.IsMatch(password, "(?=.*\\d)")) throw new PasswordIsTooWeakException("um número.");
        if (!Regex.IsMatch(password, "(?=.*[a-z])")) throw new PasswordIsTooWeakException("um caractére.");
        if (!Regex.IsMatch(password, "(?=.*[A-Z])")) throw new PasswordIsTooWeakException("um caractére maiúsculo.");
        if (!Regex.IsMatch(password, "(?=.*[@#$%])")) throw new PasswordIsTooWeakException("um simbolo.");

        Password = password.Hash();
    }
}