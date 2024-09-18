using RentalService.Infrastructure;

namespace RentalService.APIs;

public class PaymentsService : PaymentsServiceBase
{
    public PaymentsService(RentalServiceDbContext context)
        : base(context) { }
}
