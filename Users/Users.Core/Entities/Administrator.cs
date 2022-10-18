namespace Users.Core.Entities;

public class Administrator : User
{
    public Administrator()
    {
        //Entity
    }
    public Administrator(string? email, string? fullname, string? password, Role? adminRole)
    {
        this.WithEmail(email);
        this.WithPassword(password);
        this.WithName(fullname);
        this.WithRole(adminRole);
    }
}