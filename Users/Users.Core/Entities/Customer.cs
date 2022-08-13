namespace Users.Core.Entities;

public class Customer : User
{
    public static Customer Create(string? email, string? fullname, string? password)
    {
        var customer = new Customer();
        
        customer.WithEmail(email);
        customer.WithPassword(password);
        customer.WithName(fullname);
        customer.WithRole("customer");

        return customer;
    }
}