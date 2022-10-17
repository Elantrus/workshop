using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Repairing.Core.Exceptions;

namespace Repairing.Core.Entities;

public class Car
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    [NotNull]
    public string? LicensePlate { get; set; }
    
    [ForeignKey(nameof(Repair))]
    public long RepairId { get; set; }

    public virtual List<Repair> Repairs { get; set; } = new List<Repair>();

    public Car()
    {
        //Entity
    }

    public Car(string? licensePlate)
    {
        if (string.IsNullOrEmpty(licensePlate)) throw new LicensePlateIsNullException();

        LicensePlate = licensePlate;
    }
}