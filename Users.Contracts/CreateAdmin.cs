using MediatR;

namespace Users.Contracts;

public class CreateAdmin
{
    public class CreateAdminCommand : IRequest<CreateAdminResult>
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
    }

    public class CreateAdminResult
    {
        public long UserId { get; set; }
    }
}