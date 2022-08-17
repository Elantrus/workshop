using Mapster;
using MediatR;
using Users.Contracts;
using Users.Core.Entities;
using Users.Core.Exceptions;
using Users.Infrastructure.Data;

namespace Users.Application.Features;

public class CreateCustomerApplicationHandler : IRequestHandler<CreateCustomer.CreateCustomerCommand, CreateCustomer.CreateCustomerResult>
{
    private readonly UsersDbContext _usersDbContext;
    public CreateCustomerApplicationHandler(UsersDbContext usersDbContext)
    {
        _usersDbContext = usersDbContext;
    }
    public async Task<CreateCustomer.CreateCustomerResult> Handle(CreateCustomer.CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        if (request is null) throw new ArgumentNullException();

        var customerAlreadyExist = _usersDbContext.Customers.SingleOrDefault(x =>
            x.Email != null &&
            x.Email.Equals(request.Email, StringComparison.InvariantCultureIgnoreCase));

        if (customerAlreadyExist is not null) throw new EmailAlreadyExistsException();

        var customerRole = _usersDbContext.Roles.SingleOrDefault(x => x.Name != null && x.Name.Equals("customer"));

        var customerDb = Customer.Create(request.Email, request.Name, request.Password, customerRole);

        _usersDbContext.Customers.Add(customerDb);
        
        await _usersDbContext.SaveChangesAsync(cancellationToken);

        return customerDb.Adapt<CreateCustomer.CreateCustomerResult>();
    }
}