using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentalService.Infrastructure.Models;

[Table("Rentals")]
public class RentalDbModel
{
    public string? BikeId { get; set; }

    [ForeignKey(nameof(BikeId))]
    public BikeDbModel? Bike { get; set; } = null;

    [Required()]
    public DateTime CreatedAt { get; set; }

    public DateTime? EndTime { get; set; }

    [Key()]
    [Required()]
    public string Id { get; set; }

    public List<PaymentDbModel>? Payments { get; set; } = new List<PaymentDbModel>();

    [Range(-999999999, 999999999)]
    public double? Price { get; set; }

    public DateTime? StartTime { get; set; }

    [Required()]
    public DateTime UpdatedAt { get; set; }

    public string? UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public UserDbModel? User { get; set; } = null;
}
