using MediatR;
using Users.Core.Exceptions;
using Users.Infrastructure.Data;

namespace Users.Application.Features;

public class AssignAdministratorRole
{
    public class AssignAdministratorRoleCommand : IRequest
    {
        public long UserId { get; set; }
        public long RoleId { get; set; }
    }
    
    public class Handler : IRequestHandler<AssignAdministratorRoleCommand>
    {
        private readonly UsersDbContext _dbContext;

        public Handler(UsersDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(AssignAdministratorRoleCommand request,
            CancellationToken cancellationToken)
        {
            var roleDb = _dbContext.Roles.SingleOrDefault(x => x.RoleId == request.RoleId) ??
                         throw new RoleNotFoundException();

            var adminUserDb = _dbContext.Administrators.SingleOrDefault(x => x.UserId == request.UserId) ??
                              throw new UserNotFoundException();

            adminUserDb.Role = roleDb;

            await _dbContext.SaveChangesAsync(cancellationToken);
        
            return Unit.Value;
        }
    }
}
