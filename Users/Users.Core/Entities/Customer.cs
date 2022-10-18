namespace Users.Core.Entities;

public class Customer : User
{
    public Customer()
    {
        //Entity
    }
    public Customer(string? email, string? fullname, string? password, Role? userRole)
    {
        this.WithEmail(email);
        this.WithPassword(password);
        this.WithName(fullname);
        this.WithRole(userRole);
    }
}