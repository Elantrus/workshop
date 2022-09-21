using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repairing.Core.Entities;

public class Car
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    [Required]
    public string Board { get; set; }
    
    [ForeignKey(nameof(Repair))]
    public long RepairId { get; set; }
    
    public virtual Repair Repair { get; set; }
}