using MediatR;

namespace Users.Contracts;

public class CreateCustomer
{
    public class CreateCustomerCommand : IRequest<CreateCustomerResult>
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Name { get; set; }
    }

    public class CreateCustomerResult
    {
        public long UserId { get; set; }
    }
}