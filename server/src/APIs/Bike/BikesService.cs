using RentalService.Infrastructure;

namespace RentalService.APIs;

public class BikesService : BikesServiceBase
{
    public BikesService(RentalServiceDbContext context)
        : base(context) { }
}
