using System.Text.RegularExpressions;
using Users.Core.Exceptions;
using Users.Core.Extensions;

namespace Users.Domain.Entities;

public class UserBase
{
    public long UserId { get; set; }
    public string? Email { get; set; }
    public string? FullName { get; set; }
    public string? Password { get; set; }

    public UserBase(string? email, string? fullname, string? password)
    {
        SetEmail(email);
        SetFullName(fullname);
        SetPassword(password);
    }

    private void SetEmail(string? email)
    {
        if (string.IsNullOrEmpty(email) || !Regex.IsMatch(email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
            throw new InvalidEmailException();

        Email = email;
    }

    private void SetFullName(string? fullname)
    {
        if (string.IsNullOrWhiteSpace(fullname))
            throw new FullNameTooShortException();

        FullName = fullname;
    }
    
    private void SetPassword(string? password)
    {
        if (string.IsNullOrEmpty(password) || password.Length < 8) throw new PasswordTooShortException();
        
        if (!Regex.IsMatch(password, "(?=.*\\d)")) throw new PasswordIsTooWeakException("um número.");
        if (!Regex.IsMatch(password, "(?=.*[a-z])")) throw new PasswordIsTooWeakException("um caractére.");
        if (!Regex.IsMatch(password, "(?=.*[A-Z])")) throw new PasswordIsTooWeakException("um caractére maiúsculo.");
        if (!Regex.IsMatch(password, "(?=.*[@#$%])")) throw new PasswordIsTooWeakException("um simbolo.");

        Password = password.Hash();
    }
}