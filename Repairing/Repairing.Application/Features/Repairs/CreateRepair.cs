using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Repairing.Core.Entities;
using Repairing.Core.Exceptions;
using Repairing.Infrastructure.Data;

namespace Repairing.Application.Features.Repairs;

public class CreateRepair
{
    public class CreateRepairCommand : IRequest<CreateRepairResult>
    {
        public long UserId { get; set; }
        public string? CarLicensePlate { get; set; }
        public string? RepairDescription { get; set; }
    }

    public class CreateRepairResult 
    {
        public long RepairId { get; set; }
    }

    public class Handler: IRequestHandler<CreateRepairCommand, CreateRepairResult>
    {
        private readonly RepairingDbContext _dbContext;
        public Handler(RepairingDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<CreateRepairResult> Handle(CreateRepairCommand request, CancellationToken cancellationToken)
        {
            var carDb = await _dbContext.Cars.SingleOrDefaultAsync(x =>
                            x.LicensePlate.Equals(request.CarLicensePlate,
                                StringComparison.InvariantCultureIgnoreCase), cancellationToken: cancellationToken);

            var repairDb = new Repair(carDb, request.UserId, request.RepairDescription);

            _dbContext.Repairs.Add(repairDb);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return repairDb.Adapt<CreateRepairResult>();
        }
    }
    
    
}