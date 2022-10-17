namespace Users.Core.Entities;

public class Customer : User
{
    public Customer()
    {
        //Entity
    }
    public Customer(string? email, string? fullname, string? password, Role? userRole)
    {
        var customer = new Customer();
        
        customer.WithEmail(email);
        customer.WithPassword(password);
        customer.WithName(fullname);
        customer.WithRole(userRole);
    }
}