namespace RentalService.APIs.Dtos;

public class RentalCreateInput
{
    public Bike? Bike { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? EndTime { get; set; }

    public string? Id { get; set; }

    public List<Payment>? Payments { get; set; }

    public double? Price { get; set; }

    public DateTime? StartTime { get; set; }

    public DateTime UpdatedAt { get; set; }

    public User? User { get; set; }
}
