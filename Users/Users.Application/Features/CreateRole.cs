using Mapster;
using MediatR;
using Users.Core.Entities;
using Users.Core.Exceptions;
using Users.Infrastructure.Data;

namespace Users.Application.Features;

public class CreateRole
{
    public class CreateRoleCommand : IRequest<CreateRoleResult>
    {
        public string? Name { get; set; }
    }

    public class CreateRoleResult
    {
        public long RoleId { get; set; }
    }
    
    public class Handler : IRequestHandler<CreateRoleCommand, CreateRoleResult>
    {
        private readonly UsersDbContext _usersDbContext;
        public Handler(UsersDbContext usersDbContext)
        {   
            _usersDbContext = usersDbContext;
        }
        public async Task<CreateRoleResult> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            var roleAlreadyExist = _usersDbContext.Roles.Any(x =>
                x.Name != null && 
                x.Name.Equals(request.Name, StringComparison.InvariantCultureIgnoreCase));

            if (roleAlreadyExist) throw new RoleAlreadyExistException();

            var roleDb = new Role(request.Name);

            _usersDbContext.Roles.Add(roleDb);

            await _usersDbContext.SaveChangesAsync(cancellationToken);
        
            return roleDb.Adapt<CreateRoleResult>();
        }
    }
}
