using MediatR;

namespace Users.Contracts;

public class AssignAdministratorRole
{
    public class AssignAdministratorRoleCommand : IRequest
    {
        public long UserId { get; set; }
        public long RoleId { get; set; }
    }
}