using Mapster;
using MediatR;
using Users.Contracts;
using Users.Core.Entities;
using Users.Core.Exceptions;
using Users.Infrastructure.Data;

namespace Users.Application.Features;

public class CreateAdminApplicationHandler : IRequestHandler<CreateAdmin.CreateAdminCommand, CreateAdmin.CreateAdminResult>
{
    private readonly UsersDbContext _usersDbContext;
    public CreateAdminApplicationHandler(UsersDbContext usersDbContext)
    {
        _usersDbContext = usersDbContext;
    }
    public async Task<CreateAdmin.CreateAdminResult> Handle(CreateAdmin.CreateAdminCommand request, CancellationToken cancellationToken)
    {
        if (request is null) throw new ArgumentNullException();

        var adminAlreadyExist = _usersDbContext.Administrators.SingleOrDefault(x =>
            x.Email != null &&
            x.Email.Equals(request.Email, StringComparison.InvariantCultureIgnoreCase));

        if (adminAlreadyExist is not null) throw new EmailAlreadyExistsException();

        var adminRole = _usersDbContext.Roles.SingleOrDefault(x => x.Name != null && x.Name.Equals("admin"));
        
        var adminDb = Administrator.Create(request.Email, request.Name, request.Password, adminRole);

        _usersDbContext.Administrators.Add(adminDb);
        
        await _usersDbContext.SaveChangesAsync(cancellationToken);

        return adminDb.Adapt<CreateAdmin.CreateAdminResult>();
    }
}