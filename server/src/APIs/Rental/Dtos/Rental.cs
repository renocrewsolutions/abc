namespace RentalService.APIs.Dtos;

public class Rental
{
    public string? Bike { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? EndTime { get; set; }

    public string Id { get; set; }

    public List<string>? Payments { get; set; }

    public double? Price { get; set; }

    public DateTime? StartTime { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? User { get; set; }
}
