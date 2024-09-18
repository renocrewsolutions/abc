using RentalService.Infrastructure;

namespace RentalService.APIs;

public class UsersService : UsersServiceBase
{
    public UsersService(RentalServiceDbContext context)
        : base(context) { }
}
