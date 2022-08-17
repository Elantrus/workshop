using MediatR;

namespace Users.Contracts;

public class CreateRole
{
    public class CreateRoleCommand : IRequest
    {
        public string? Name { get; set; }
    }
}