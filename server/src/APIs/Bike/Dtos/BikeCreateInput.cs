using RentalService.Core.Enums;

namespace RentalService.APIs.Dtos;

public class BikeCreateInput
{
    public DateTime CreatedAt { get; set; }

    public string? Id { get; set; }

    public string? Location { get; set; }

    public string? Model { get; set; }

    public List<Rental>? Rentals { get; set; }

    public StatusEnum? Status { get; set; }

    public DateTime UpdatedAt { get; set; }
}
