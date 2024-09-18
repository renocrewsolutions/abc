using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentalService.APIs;
using RentalService.APIs.Common;
using RentalService.APIs.Dtos;
using RentalService.APIs.Errors;

namespace RentalService.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class UsersControllerBase : ControllerBase
{
    protected readonly IUsersService _service;

    public UsersControllerBase(IUsersService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one User
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "admin,superAdmin,user")]
    public async Task<ActionResult<User>> CreateUser(UserCreateInput input)
    {
        var user = await _service.CreateUser(input);

        return CreatedAtAction(nameof(User), new { id = user.Id }, user);
    }

    /// <summary>
    /// Delete one User
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "admin,superAdmin,user")]
    public async Task<ActionResult> DeleteUser([FromRoute()] UserWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteUser(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Users
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "admin,superAdmin,user")]
    public async Task<ActionResult<List<User>>> Users([FromQuery()] UserFindManyArgs filter)
    {
        return Ok(await _service.Users(filter));
    }

    /// <summary>
    /// Meta data about User records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> UsersMeta([FromQuery()] UserFindManyArgs filter)
    {
        return Ok(await _service.UsersMeta(filter));
    }

    /// <summary>
    /// Get one User
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "admin,superAdmin,user")]
    public async Task<ActionResult<User>> User([FromRoute()] UserWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.User(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one User
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "admin,superAdmin,user")]
    public async Task<ActionResult> UpdateUser(
        [FromRoute()] UserWhereUniqueInput uniqueId,
        [FromQuery()] UserUpdateInput userUpdateDto
    )
    {
        try
        {
            await _service.UpdateUser(uniqueId, userUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Connect multiple Rentals records to User
    /// </summary>
    [HttpPost("{Id}/rentals")]
    [Authorize(Roles = "admin,superAdmin,user")]
    public async Task<ActionResult> ConnectRentals(
        [FromRoute()] UserWhereUniqueInput uniqueId,
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
    /// Disconnect multiple Rentals records from User
    /// </summary>
    [HttpDelete("{Id}/rentals")]
    [Authorize(Roles = "admin,superAdmin,user")]
    public async Task<ActionResult> DisconnectRentals(
        [FromRoute()] UserWhereUniqueInput uniqueId,
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
    /// Find multiple Rentals records for User
    /// </summary>
    [HttpGet("{Id}/rentals")]
    [Authorize(Roles = "admin,superAdmin,user")]
    public async Task<ActionResult<List<Rental>>> FindRentals(
        [FromRoute()] UserWhereUniqueInput uniqueId,
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
    /// Update multiple Rentals records for User
    /// </summary>
    [HttpPatch("{Id}/rentals")]
    [Authorize(Roles = "admin,superAdmin,user")]
    public async Task<ActionResult> UpdateRentals(
        [FromRoute()] UserWhereUniqueInput uniqueId,
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
