using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Repairing.Core.Exceptions;

namespace Repairing.Core.Entities;

public class Repair
{
    [Key] public long RepairId { get; set; }

    public long UserId { get; set; }

    [NotNull] public virtual Car Car { get; set; }

    public string? Description { get; set; }

    public Repair()
    {
        //Entity
    }

    public Repair(Car? carDb, long userId, string? repairDescription)
    {
        if (userId == default) throw new InvalidUserException();
        if (carDb is null) throw new CarNotExistsException();

        Car = carDb;
        Description = repairDescription;
        UserId = userId;
    }
}