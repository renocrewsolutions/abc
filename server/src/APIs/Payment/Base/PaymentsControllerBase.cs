using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentalService.APIs;
using RentalService.APIs.Common;
using RentalService.APIs.Dtos;
using RentalService.APIs.Errors;

namespace RentalService.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class PaymentsControllerBase : ControllerBase
{
    protected readonly IPaymentsService _service;

    public PaymentsControllerBase(IPaymentsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Payment
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "admin,superAdmin,user")]
    public async Task<ActionResult<Payment>> CreatePayment(PaymentCreateInput input)
    {
        var payment = await _service.CreatePayment(input);

        return CreatedAtAction(nameof(Payment), new { id = payment.Id }, payment);
    }

    /// <summary>
    /// Delete one Payment
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "admin,superAdmin,user")]
    public async Task<ActionResult> DeletePayment([FromRoute()] PaymentWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeletePayment(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Payments
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "admin,superAdmin,user")]
    public async Task<ActionResult<List<Payment>>> Payments(
        [FromQuery()] PaymentFindManyArgs filter
    )
    {
        return Ok(await _service.Payments(filter));
    }

    /// <summary>
    /// Meta data about Payment records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> PaymentsMeta(
        [FromQuery()] PaymentFindManyArgs filter
    )
    {
        return Ok(await _service.PaymentsMeta(filter));
    }

    /// <summary>
    /// Get one Payment
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "admin,superAdmin,user")]
    public async Task<ActionResult<Payment>> Payment([FromRoute()] PaymentWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Payment(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Payment
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "admin,superAdmin,user")]
    public async Task<ActionResult> UpdatePayment(
        [FromRoute()] PaymentWhereUniqueInput uniqueId,
        [FromQuery()] PaymentUpdateInput paymentUpdateDto
    )
    {
        try
        {
            await _service.UpdatePayment(uniqueId, paymentUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Get a rental record for Payment
    /// </summary>
    [HttpGet("{Id}/rental")]
    public async Task<ActionResult<List<Rental>>> GetRental(
        [FromRoute()] PaymentWhereUniqueInput uniqueId
    )
    {
        var rental = await _service.GetRental(uniqueId);
        return Ok(rental);
    }
}
