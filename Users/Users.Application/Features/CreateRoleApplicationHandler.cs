using MediatR;
using Users.Contracts;
using Users.Core.Entities;
using Users.Core.Exceptions;
using Users.Infrastructure.Data;

namespace Users.Application.Features;

public class CreateRoleApplicationHandler : IRequestHandler<CreateRole.CreateRoleCommand>
{
    private readonly UsersDbContext _usersDbContext;
    public CreateRoleApplicationHandler(UsersDbContext usersDbContext)
    {   
        _usersDbContext = usersDbContext;
    }
    public async Task<Unit> Handle(CreateRole.CreateRoleCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request?.Name)) throw new RoleNameTooShortException();

            var roleAlreadyExist = _usersDbContext.Roles.Any(x =>
            x.Name != null && 
            x.Name.Equals(request.Name, StringComparison.InvariantCultureIgnoreCase));

        if (roleAlreadyExist) throw new RoleAlreadyExistException();

        var roleDb = Role.Create(request.Name);

        _usersDbContext.Roles.Add(roleDb);

        await _usersDbContext.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}