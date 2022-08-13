namespace Users.Domain.Entities;

public class Customer : UserBase
{
    public Customer(string email, string fullName, string password) : base(email, fullName, password)
    {
    }
}