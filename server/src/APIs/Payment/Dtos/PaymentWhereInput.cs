using RentalService.Core.Enums;

namespace RentalService.APIs.Dtos;

public class PaymentWhereInput
{
    public double? Amount { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? Id { get; set; }

    public PaymentMethodEnum? PaymentMethod { get; set; }

    public DateTime? PaymentTime { get; set; }

    public string? Rental { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
