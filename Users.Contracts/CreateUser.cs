using MediatR;

namespace Users.Contracts;

public class CreateUser
{
    public class Command : IRequest<Result>
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Name { get; set; }
    }

    public class Result
    {
        public long UserId { get; set; }
    }
}