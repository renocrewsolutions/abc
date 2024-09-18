using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentalService.APIs;
using RentalService.APIs.Common;
using RentalService.APIs.Dtos;
using RentalService.APIs.Errors;

namespace RentalService.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class RentalsControllerBase : ControllerBase
{
    protected readonly IRentalsService _service;

    public RentalsControllerBase(IRentalsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Rental
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Rental>> CreateRental(RentalCreateInput input)
    {
        var rental = await _service.CreateRental(input);

        return CreatedAtAction(nameof(Rental), new { id = rental.Id }, rental);
    }

    /// <summary>
    /// Delete one Rental
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DeleteRental([FromRoute()] RentalWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteRental(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Rentals
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<Rental>>> Rentals([FromQuery()] RentalFindManyArgs filter)
    {
        return Ok(await _service.Rentals(filter));
    }

    /// <summary>
    /// Meta data about Rental records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> RentalsMeta(
        [FromQuery()] RentalFindManyArgs filter
    )
    {
        return Ok(await _service.RentalsMeta(filter));
    }

    /// <summary>
    /// Get one Rental
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Rental>> Rental([FromRoute()] RentalWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Rental(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Rental
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdateRental(
        [FromRoute()] RentalWhereUniqueInput uniqueId,
        [FromQuery()] RentalUpdateInput rentalUpdateDto
    )
    {
        try
        {
            await _service.UpdateRental(uniqueId, rentalUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Get a Bike record for Rental
    /// </summary>
    [HttpGet("{Id}/bike")]
    public async Task<ActionResult<List<Bike>>> GetBike(
        [FromRoute()] RentalWhereUniqueInput uniqueId
    )
    {
        var bike = await _service.GetBike(uniqueId);
        return Ok(bike);
    }

    /// <summary>
    /// Connect multiple Payments records to Rental
    /// </summary>
    [HttpPost("{Id}/payments")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> ConnectPayments(
        [FromRoute()] RentalWhereUniqueInput uniqueId,
        [FromQuery()] PaymentWhereUniqueInput[] paymentsId
    )
    {
        try
        {
            await _service.ConnectPayments(uniqueId, paymentsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect multiple Payments records from Rental
    /// </summary>
    [HttpDelete("{Id}/payments")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DisconnectPayments(
        [FromRoute()] RentalWhereUniqueInput uniqueId,
        [FromBody()] PaymentWhereUniqueInput[] paymentsId
    )
    {
        try
        {
            await _service.DisconnectPayments(uniqueId, paymentsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find multiple Payments records for Rental
    /// </summary>
    [HttpGet("{Id}/payments")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<Payment>>> FindPayments(
        [FromRoute()] RentalWhereUniqueInput uniqueId,
        [FromQuery()] PaymentFindManyArgs filter
    )
    {
        try
        {
            return Ok(await _service.FindPayments(uniqueId, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update multiple Payments records for Rental
    /// </summary>
    [HttpPatch("{Id}/payments")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdatePayments(
        [FromRoute()] RentalWhereUniqueInput uniqueId,
        [FromBody()] PaymentWhereUniqueInput[] paymentsId
    )
    {
        try
        {
            await _service.UpdatePayments(uniqueId, paymentsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Get a User record for Rental
    /// </summary>
    [HttpGet("{Id}/user")]
    public async Task<ActionResult<List<User>>> GetUser(
        [FromRoute()] RentalWhereUniqueInput uniqueId
    )
    {
        var user = await _service.GetUser(uniqueId);
        return Ok(user);
    }
}
