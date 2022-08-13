namespace Users.Core.Entities;

public class Customer : User
{
    public Customer(string? email, string? fullName, string? password) : base(email, fullName, password)
    {
    }
}