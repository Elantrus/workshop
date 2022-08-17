using MediatR;

namespace Users.Contracts;

public class AuthenticateUser
{
    public class AuthenticateUserCommand : IRequest<AuthenticateUserResult>
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }

    public class AuthenticateUserResult
    {
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
    }
}