using RentalService.Core.Enums;

namespace RentalService.APIs.Dtos;

public class BikeUpdateInput
{
    public DateTime? CreatedAt { get; set; }

    public string? Id { get; set; }

    public string? Location { get; set; }

    public string? Model { get; set; }

    public List<string>? Rentals { get; set; }

    public StatusEnum? Status { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
