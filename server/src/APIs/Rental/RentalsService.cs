using RentalService.Infrastructure;

namespace RentalService.APIs;

public class RentalsService : RentalsServiceBase
{
    public RentalsService(RentalServiceDbContext context)
        : base(context) { }
}
