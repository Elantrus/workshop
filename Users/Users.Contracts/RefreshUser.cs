using MediatR;

namespace Users.Contracts;

public class RefreshUser
{
    public class RefreshUserCommand : IRequest<RefreshUserResult>
    {
        public string? RefreshToken { get; set; }
    }

    public class RefreshUserResult
    {
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
    }
}