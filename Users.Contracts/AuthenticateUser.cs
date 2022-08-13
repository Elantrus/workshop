using MediatR;

namespace Users.Contracts;

public class AuthenticateUser
{
    public class Command : IRequest<Result>
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }

    public class Result
    {
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
    }
}