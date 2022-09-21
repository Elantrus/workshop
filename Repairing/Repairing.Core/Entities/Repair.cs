using System.ComponentModel.DataAnnotations;

namespace Repairing.Core.Entities;

public class Repair
{
    [Key]
    public long Id { get; set; }
    
    public string? Description { get; set; }
}