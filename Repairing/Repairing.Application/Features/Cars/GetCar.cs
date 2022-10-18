using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Repairing.Core.Entities;
using Repairing.Core.Exceptions;
using Repairing.Infrastructure.Data;

namespace Repairing.Application.Features.Cars;

public class GetCar
{
    public class GetCarCommand : IRequest<GetCarResult>
    {
        public string? LicensePlate { get; set; }
    }

    public class GetCarResult
    {
        public string? LicensePlate { get; set;}
    }


    public class Handler : IRequestHandler<GetCarCommand, GetCarResult>
    {
        private readonly RepairingDbContext _dbContext;

        public Handler(RepairingDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<GetCarResult> Handle(GetCarCommand request, CancellationToken cancellationToken)
        {
            var carDb = await _dbContext.Cars.SingleOrDefaultAsync(x =>
                            x.LicensePlate.Equals(request.LicensePlate, StringComparison.InvariantCultureIgnoreCase), cancellationToken: cancellationToken) ??
                        throw new CarNotExistsException();

            return carDb.Adapt<GetCarResult>();
        }
    }
}