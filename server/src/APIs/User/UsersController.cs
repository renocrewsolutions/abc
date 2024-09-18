using Microsoft.AspNetCore.Mvc;

namespace RentalService.APIs;

[ApiController()]
public class UsersController : UsersControllerBase
{
    public UsersController(IUsersService service)
        : base(service) { }
}
