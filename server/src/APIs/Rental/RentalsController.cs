using Microsoft.AspNetCore.Mvc;

namespace RentalService.APIs;

[ApiController()]
public class RentalsController : RentalsControllerBase
{
    public RentalsController(IRentalsService service)
        : base(service) { }
}
