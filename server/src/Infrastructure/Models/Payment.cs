using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RentalService.Core.Enums;

namespace RentalService.Infrastructure.Models;

[Table("Payments")]
public class PaymentDbModel
{
    [Range(-999999999, 999999999)]
    public double? Amount { get; set; }

    [Required()]
    public DateTime CreatedAt { get; set; }

    [Key()]
    [Required()]
    public string Id { get; set; }

    public PaymentMethodEnum? PaymentMethod { get; set; }

    public DateTime? PaymentTime { get; set; }

    public string? RentalId { get; set; }

    [ForeignKey(nameof(RentalId))]
    public RentalDbModel? Rental { get; set; } = null;

    [Required()]
    public DateTime UpdatedAt { get; set; }
}
