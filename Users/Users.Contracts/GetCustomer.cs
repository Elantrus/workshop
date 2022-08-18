using MediatR;

namespace Users.Contracts;

public class GetCustomer
{
    public class GetCustomerCommand : IRequest<GetCustomerResult>
    {
        public long UserId { get; set; }
    }
    public class GetCustomerResult
    {
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}