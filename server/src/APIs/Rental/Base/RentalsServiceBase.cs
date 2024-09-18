using Microsoft.EntityFrameworkCore;
using RentalService.APIs;
using RentalService.APIs.Common;
using RentalService.APIs.Dtos;
using RentalService.APIs.Errors;
using RentalService.APIs.Extensions;
using RentalService.Infrastructure;
using RentalService.Infrastructure.Models;

namespace RentalService.APIs;

public abstract class RentalsServiceBase : IRentalsService
{
    protected readonly RentalServiceDbContext _context;

    public RentalsServiceBase(RentalServiceDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Rental
    /// </summary>
    public async Task<Rental> CreateRental(RentalCreateInput createDto)
    {
        var rental = new RentalDbModel
        {
            CreatedAt = createDto.CreatedAt,
            EndTime = createDto.EndTime,
            Price = createDto.Price,
            StartTime = createDto.StartTime,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            rental.Id = createDto.Id;
        }
        if (createDto.Bike != null)
        {
            rental.Bike = await _context
                .Bikes.Where(bike => createDto.Bike.Id == bike.Id)
                .FirstOrDefaultAsync();
        }

        if (createDto.Payments != null)
        {
            rental.Payments = await _context
                .Payments.Where(payment =>
                    createDto.Payments.Select(t => t.Id).Contains(payment.Id)
                )
                .ToListAsync();
        }

        if (createDto.User != null)
        {
            rental.User = await _context
                .Users.Where(user => createDto.User.Id == user.Id)
                .FirstOrDefaultAsync();
        }

        _context.Rentals.Add(rental);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<RentalDbModel>(rental.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Rental
    /// </summary>
    public async Task DeleteRental(RentalWhereUniqueInput uniqueId)
    {
        var rental = await _context.Rentals.FindAsync(uniqueId.Id);
        if (rental == null)
        {
            throw new NotFoundException();
        }

        _context.Rentals.Remove(rental);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Rentals
    /// </summary>
    public async Task<List<Rental>> Rentals(RentalFindManyArgs findManyArgs)
    {
        var rentals = await _context
            .Rentals.Include(x => x.Bike)
            .Include(x => x.Payments)
            .Include(x => x.User)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return rentals.ConvertAll(rental => rental.ToDto());
    }

    /// <summary>
    /// Meta data about Rental records
    /// </summary>
    public async Task<MetadataDto> RentalsMeta(RentalFindManyArgs findManyArgs)
    {
        var count = await _context.Rentals.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Rental
    /// </summary>
    public async Task<Rental> Rental(RentalWhereUniqueInput uniqueId)
    {
        var rentals = await this.Rentals(
            new RentalFindManyArgs { Where = new RentalWhereInput { Id = uniqueId.Id } }
        );
        var rental = rentals.FirstOrDefault();
        if (rental == null)
        {
            throw new NotFoundException();
        }

        return rental;
    }

    /// <summary>
    /// Update one Rental
    /// </summary>
    public async Task UpdateRental(RentalWhereUniqueInput uniqueId, RentalUpdateInput updateDto)
    {
        var rental = updateDto.ToModel(uniqueId);

        if (updateDto.Bike != null)
        {
            rental.Bike = await _context
                .Bikes.Where(bike => updateDto.Bike == bike.Id)
                .FirstOrDefaultAsync();
        }

        if (updateDto.Payments != null)
        {
            rental.Payments = await _context
                .Payments.Where(payment => updateDto.Payments.Select(t => t).Contains(payment.Id))
                .ToListAsync();
        }

        if (updateDto.User != null)
        {
            rental.User = await _context
                .Users.Where(user => updateDto.User == user.Id)
                .FirstOrDefaultAsync();
        }

        _context.Entry(rental).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Rentals.Any(e => e.Id == rental.Id))
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
    /// Get a Bike record for Rental
    /// </summary>
    public async Task<Bike> GetBike(RentalWhereUniqueInput uniqueId)
    {
        var rental = await _context
            .Rentals.Where(rental => rental.Id == uniqueId.Id)
            .Include(rental => rental.Bike)
            .FirstOrDefaultAsync();
        if (rental == null)
        {
            throw new NotFoundException();
        }
        return rental.Bike.ToDto();
    }

    /// <summary>
    /// Connect multiple Payments records to Rental
    /// </summary>
    public async Task ConnectPayments(
        RentalWhereUniqueInput uniqueId,
        PaymentWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Rentals.Include(x => x.Payments)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Payments.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();
        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        var childrenToConnect = children.Except(parent.Payments);

        foreach (var child in childrenToConnect)
        {
            parent.Payments.Add(child);
        }

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Disconnect multiple Payments records from Rental
    /// </summary>
    public async Task DisconnectPayments(
        RentalWhereUniqueInput uniqueId,
        PaymentWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Rentals.Include(x => x.Payments)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Payments.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();

        foreach (var child in children)
        {
            parent.Payments?.Remove(child);
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find multiple Payments records for Rental
    /// </summary>
    public async Task<List<Payment>> FindPayments(
        RentalWhereUniqueInput uniqueId,
        PaymentFindManyArgs rentalFindManyArgs
    )
    {
        var payments = await _context
            .Payments.Where(m => m.RentalId == uniqueId.Id)
            .ApplyWhere(rentalFindManyArgs.Where)
            .ApplySkip(rentalFindManyArgs.Skip)
            .ApplyTake(rentalFindManyArgs.Take)
            .ApplyOrderBy(rentalFindManyArgs.SortBy)
            .ToListAsync();

        return payments.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Update multiple Payments records for Rental
    /// </summary>
    public async Task UpdatePayments(
        RentalWhereUniqueInput uniqueId,
        PaymentWhereUniqueInput[] childrenIds
    )
    {
        var rental = await _context
            .Rentals.Include(t => t.Payments)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (rental == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Payments.Where(a => childrenIds.Select(x => x.Id).Contains(a.Id))
            .ToListAsync();

        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        rental.Payments = children;
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Get a User record for Rental
    /// </summary>
    public async Task<User> GetUser(RentalWhereUniqueInput uniqueId)
    {
        var rental = await _context
            .Rentals.Where(rental => rental.Id == uniqueId.Id)
            .Include(rental => rental.User)
            .FirstOrDefaultAsync();
        if (rental == null)
        {
            throw new NotFoundException();
        }
        return rental.User.ToDto();
    }
}
