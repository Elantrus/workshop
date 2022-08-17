namespace Users.Core.Entities;

public class Administrator : User
{
    public static Administrator Create(string? email, string? fullname, string? password, Role? adminRole)
    {
        var administrator = new Administrator();
        
        administrator.WithEmail(email);
        administrator.WithPassword(password);
        administrator.WithName(fullname);
        administrator.WithRole(adminRole);

        return administrator;
    }
}