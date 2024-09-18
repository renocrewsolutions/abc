using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RentalService.Core.Enums;

namespace RentalService.Infrastructure.Models;

[Table("Bikes")]
public class BikeDbModel
{
    [Required()]
    public DateTime CreatedAt { get; set; }

    [Key()]
    [Required()]
    public string Id { get; set; }

    [StringLength(1000)]
    public string? Location { get; set; }

    [StringLength(1000)]
    public string? Model { get; set; }

    public List<RentalDbModel>? Rentals { get; set; } = new List<RentalDbModel>();

    public StatusEnum? Status { get; set; }

    [Required()]
    public DateTime UpdatedAt { get; set; }
}
