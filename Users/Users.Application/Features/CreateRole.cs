using MediatR;
using Users.Core.Entities;
using Users.Core.Exceptions;
using Users.Infrastructure.Data;

namespace Users.Application.Features;

public class CreateRole
{
    public class Command : IRequest
    {
        public string? Name { get; set; }
    }
    
    public class Handler : IRequestHandler<Command>
    {
        private readonly UsersDbContext _usersDbContext;
        public Handler(UsersDbContext usersDbContext)
        {   
            _usersDbContext = usersDbContext;
        }
        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
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
}
