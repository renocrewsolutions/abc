using Microsoft.EntityFrameworkCore;
using RentalService.APIs;
using RentalService.APIs.Common;
using RentalService.APIs.Dtos;
using RentalService.APIs.Errors;
using RentalService.APIs.Extensions;
using RentalService.Infrastructure;
using RentalService.Infrastructure.Models;

namespace RentalService.APIs;

public abstract class PaymentsServiceBase : IPaymentsService
{
    protected readonly RentalServiceDbContext _context;

    public PaymentsServiceBase(RentalServiceDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Payment
    /// </summary>
    public async Task<Payment> CreatePayment(PaymentCreateInput createDto)
    {
        var payment = new PaymentDbModel
        {
            Amount = createDto.Amount,
            CreatedAt = createDto.CreatedAt,
            PaymentMethod = createDto.PaymentMethod,
            PaymentTime = createDto.PaymentTime,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            payment.Id = createDto.Id;
        }
        if (createDto.Rental != null)
        {
            payment.Rental = await _context
                .Rentals.Where(rental => createDto.Rental.Id == rental.Id)
                .FirstOrDefaultAsync();
        }

        _context.Payments.Add(payment);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<PaymentDbModel>(payment.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Payment
    /// </summary>
    public async Task DeletePayment(PaymentWhereUniqueInput uniqueId)
    {
        var payment = await _context.Payments.FindAsync(uniqueId.Id);
        if (payment == null)
        {
            throw new NotFoundException();
        }

        _context.Payments.Remove(payment);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Payments
    /// </summary>
    public async Task<List<Payment>> Payments(PaymentFindManyArgs findManyArgs)
    {
        var payments = await _context
            .Payments.Include(x => x.Rental)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return payments.ConvertAll(payment => payment.ToDto());
    }

    /// <summary>
    /// Meta data about Payment records
    /// </summary>
    public async Task<MetadataDto> PaymentsMeta(PaymentFindManyArgs findManyArgs)
    {
        var count = await _context.Payments.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Payment
    /// </summary>
    public async Task<Payment> Payment(PaymentWhereUniqueInput uniqueId)
    {
        var payments = await this.Payments(
            new PaymentFindManyArgs { Where = new PaymentWhereInput { Id = uniqueId.Id } }
        );
        var payment = payments.FirstOrDefault();
        if (payment == null)
        {
            throw new NotFoundException();
        }

        return payment;
    }

    /// <summary>
    /// Update one Payment
    /// </summary>
    public async Task UpdatePayment(PaymentWhereUniqueInput uniqueId, PaymentUpdateInput updateDto)
    {
        var payment = updateDto.ToModel(uniqueId);

        if (updateDto.Rental != null)
        {
            payment.Rental = await _context
                .Rentals.Where(rental => updateDto.Rental == rental.Id)
                .FirstOrDefaultAsync();
        }

        _context.Entry(payment).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Payments.Any(e => e.Id == payment.Id))
            {
                throw new NotFoundException();
            }
            else
            {
                throw;
            }
        }
    }

    /// <summary>
    /// Get a rental record for Payment
    /// </summary>
    public async Task<Rental> GetRental(PaymentWhereUniqueInput uniqueId)
    {
        var payment = await _context
            .Payments.Where(payment => payment.Id == uniqueId.Id)
            .Include(payment => payment.Rental)
            .FirstOrDefaultAsync();
        if (payment == null)
        {
            throw new NotFoundException();
        }
        return payment.Rental.ToDto();
    }
}
