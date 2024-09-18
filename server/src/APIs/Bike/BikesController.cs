using Microsoft.AspNetCore.Mvc;

namespace RentalService.APIs;

[ApiController()]
public class BikesController : BikesControllerBase
{
    public BikesController(IBikesService service)
        : base(service) { }
}
