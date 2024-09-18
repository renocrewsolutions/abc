using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentalService.APIs;
using RentalService.APIs.Common;
using RentalService.APIs.Dtos;
using RentalService.APIs.Errors;

namespace RentalService.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class BikesControllerBase : ControllerBase
{
    protected readonly IBikesService _service;

    public BikesControllerBase(IBikesService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Bike
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "admin,superAdmin,user")]
    public async Task<ActionResult<Bike>> CreateBike(BikeCreateInput input)
    {
        var bike = await _service.CreateBike(input);

        return CreatedAtAction(nameof(Bike), new { id = bike.Id }, bike);
    }

    /// <summary>
    /// Delete one Bike
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "admin,superAdmin,user")]
    public async Task<ActionResult> DeleteBike([FromRoute()] BikeWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteBike(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Bikes
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "admin,superAdmin,user")]
    public async Task<ActionResult<List<Bike>>> Bikes([FromQuery()] BikeFindManyArgs filter)
    {
        return Ok(await _service.Bikes(filter));
    }

    /// <summary>
    /// Meta data about Bike records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> BikesMeta([FromQuery()] BikeFindManyArgs filter)
    {
        return Ok(await _service.BikesMeta(filter));
    }

    /// <summary>
    /// Get one Bike
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "admin,superAdmin,user")]
    public async Task<ActionResult<Bike>> Bike([FromRoute()] BikeWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Bike(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Bike
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "admin,superAdmin,user")]
    public async Task<ActionResult> UpdateBike(
        [FromRoute()] BikeWhereUniqueInput uniqueId,
        [FromQuery()] BikeUpdateInput bikeUpdateDto
    )
    {
        try
        {
            await _service.UpdateBike(uniqueId, bikeUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Connect multiple Rentals records to Bike
    /// </summary>
    [HttpPost("{Id}/rentals")]
    [Authorize(Roles = "admin,superAdmin,user")]
    public async Task<ActionResult> ConnectRentals(
        [FromRoute()] BikeWhereUniqueInput uniqueId,
        [FromQuery()] RentalWhereUniqueInput[] rentalsId
    )
    {
        try
        {
            await _service.ConnectRentals(uniqueId, rentalsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect multiple Rentals records from Bike
    /// </summary>
    [HttpDelete("{Id}/rentals")]
    [Authorize(Roles = "admin,superAdmin,user")]
    public async Task<ActionResult> DisconnectRentals(
        [FromRoute()] BikeWhereUniqueInput uniqueId,
        [FromBody()] RentalWhereUniqueInput[] rentalsId
    )
    {
        try
        {
            await _service.DisconnectRentals(uniqueId, rentalsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find multiple Rentals records for Bike
    /// </summary>
    [HttpGet("{Id}/rentals")]
    [Authorize(Roles = "admin,superAdmin,user")]
    public async Task<ActionResult<List<Rental>>> FindRentals(
        [FromRoute()] BikeWhereUniqueInput uniqueId,
        [FromQuery()] RentalFindManyArgs filter
    )
    {
        try
        {
            return Ok(await _service.FindRentals(uniqueId, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update multiple Rentals records for Bike
    /// </summary>
    [HttpPatch("{Id}/rentals")]
    [Authorize(Roles = "admin,superAdmin,user")]
    public async Task<ActionResult> UpdateRentals(
        [FromRoute()] BikeWhereUniqueInput uniqueId,
        [FromBody()] RentalWhereUniqueInput[] rentalsId
    )
    {
        try
        {
            await _service.UpdateRentals(uniqueId, rentalsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
