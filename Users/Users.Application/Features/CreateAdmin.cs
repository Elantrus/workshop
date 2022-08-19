using Mapster;
using MediatR;
using Users.Core.Entities;
using Users.Core.Exceptions;
using Users.Infrastructure.Data;

namespace Users.Application.Features;

public class CreateAdmin
{
    public class Command : IRequest<Result>
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
    }

    public class Result
    {
        public long UserId { get; set; }
    }
    
    public class Handler : IRequestHandler<Command, Result>
    {
        private readonly UsersDbContext _usersDbContext;
        public Handler(UsersDbContext usersDbContext)
        {
            _usersDbContext = usersDbContext;
        }
        public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
        {
            if (request is null) throw new ArgumentNullException();

            var adminAlreadyExist = _usersDbContext.Administrators.SingleOrDefault(x =>
                x.Email != null &&
                x.Email.Equals(request.Email, StringComparison.InvariantCultureIgnoreCase));

            if (adminAlreadyExist is not null) throw new EmailAlreadyExistsException();

            var adminRole = _usersDbContext.Roles.SingleOrDefault(x => x.Name != null && x.Name.Equals("admin")) ?? throw new RoleNotFoundException();
        
            var adminDb = Administrator.Create(request.Email, request.Name, request.Password, adminRole);

            _usersDbContext.Administrators.Add(adminDb);
        
            await _usersDbContext.SaveChangesAsync(cancellationToken);

            return adminDb.Adapt<Result>();
        }
    }
}
