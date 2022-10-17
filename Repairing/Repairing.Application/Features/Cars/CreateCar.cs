using MediatR;
using Repairing.Core.Entities;
using Repairing.Infrastructure.Data;

namespace Repairing.Application.Features.Cars;

public class CreateCar
{
    public class CreateCarCommand : IRequest
    {
        public string? LicensePlate { get; set; }
    }


    public class Handler: IRequestHandler<CreateCarCommand>
    {
        private readonly RepairingDbContext _dbContext;
        public Handler(RepairingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(CreateCarCommand request, CancellationToken cancellationToken)
        {
            var carDb = _dbContext.Cars.SingleOrDefault(x =>
                x.LicensePlate.Equals(request.LicensePlate, StringComparison.InvariantCultureIgnoreCase));

            if (carDb is not null) return Unit.Value;

            carDb = new Car(request.LicensePlate);

            await _dbContext.Cars.AddAsync(carDb, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }
}