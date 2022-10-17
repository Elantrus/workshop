using MediatR;
using Repairing.Infrastructure.Data;

namespace Repairing.Application.Features.Repairs;

public class CreateRepair
{
    public class CreateRepairCommand : IRequest<CreateRepairResult>
    {
        public long UserId { get; set; }
        public long CarId { get; set; }
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
        public Task<CreateRepairResult> Handle(CreateRepairCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
    
    
}