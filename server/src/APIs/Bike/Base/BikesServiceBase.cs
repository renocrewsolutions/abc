using Microsoft.EntityFrameworkCore;
using RentalService.APIs;
using RentalService.APIs.Common;
using RentalService.APIs.Dtos;
using RentalService.APIs.Errors;
using RentalService.APIs.Extensions;
using RentalService.Infrastructure;
using RentalService.Infrastructure.Models;

namespace RentalService.APIs;

public abstract class BikesServiceBase : IBikesService
{
    protected readonly RentalServiceDbContext _context;

    public BikesServiceBase(RentalServiceDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Bike
    /// </summary>
    public async Task<Bike> CreateBike(BikeCreateInput createDto)
    {
        var bike = new BikeDbModel
        {
            CreatedAt = createDto.CreatedAt,
            Location = createDto.Location,
            Model = createDto.Model,
            Status = createDto.Status,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            bike.Id = createDto.Id;
        }
        if (createDto.Rentals != null)
        {
            bike.Rentals = await _context
                .Rentals.Where(rental => createDto.Rentals.Select(t => t.Id).Contains(rental.Id))
                .ToListAsync();
        }

        _context.Bikes.Add(bike);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<BikeDbModel>(bike.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Bike
    /// </summary>
    public async Task DeleteBike(BikeWhereUniqueInput uniqueId)
    {
        var bike = await _context.Bikes.FindAsync(uniqueId.Id);
        if (bike == null)
        {
            throw new NotFoundException();
        }

        _context.Bikes.Remove(bike);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Bikes
    /// </summary>
    public async Task<List<Bike>> Bikes(BikeFindManyArgs findManyArgs)
    {
        var bikes = await _context
            .Bikes.Include(x => x.Rentals)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return bikes.ConvertAll(bike => bike.ToDto());
    }

    /// <summary>
    /// Meta data about Bike records
    /// </summary>
    public async Task<MetadataDto> BikesMeta(BikeFindManyArgs findManyArgs)
    {
        var count = await _context.Bikes.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Bike
    /// </summary>
    public async Task<Bike> Bike(BikeWhereUniqueInput uniqueId)
    {
        var bikes = await this.Bikes(
            new BikeFindManyArgs { Where = new BikeWhereInput { Id = uniqueId.Id } }
        );
        var bike = bikes.FirstOrDefault();
        if (bike == null)
        {
            throw new NotFoundException();
        }

        return bike;
    }

    /// <summary>
    /// Update one Bike
    /// </summary>
    public async Task UpdateBike(BikeWhereUniqueInput uniqueId, BikeUpdateInput updateDto)
    {
        var bike = updateDto.ToModel(uniqueId);

        if (updateDto.Rentals != null)
        {
            bike.Rentals = await _context
                .Rentals.Where(rental => updateDto.Rentals.Select(t => t).Contains(rental.Id))
                .ToListAsync();
        }

        _context.Entry(bike).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Bikes.Any(e => e.Id == bike.Id))
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
    /// Connect multiple Rentals records to Bike
    /// </summary>
    public async Task ConnectRentals(
        BikeWhereUniqueInput uniqueId,
        RentalWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Bikes.Include(x => x.Rentals)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Rentals.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();
        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        var childrenToConnect = children.Except(parent.Rentals);

        foreach (var child in childrenToConnect)
        {
            parent.Rentals.Add(child);
        }

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Disconnect multiple Rentals records from Bike
    /// </summary>
    public async Task DisconnectRentals(
        BikeWhereUniqueInput uniqueId,
        RentalWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Bikes.Include(x => x.Rentals)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Rentals.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();

        foreach (var child in children)
        {
            parent.Rentals?.Remove(child);
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find multiple Rentals records for Bike
    /// </summary>
    public async Task<List<Rental>> FindRentals(
        BikeWhereUniqueInput uniqueId,
        RentalFindManyArgs bikeFindManyArgs
    )
    {
        var rentals = await _context
            .Rentals.Where(m => m.BikeId == uniqueId.Id)
            .ApplyWhere(bikeFindManyArgs.Where)
            .ApplySkip(bikeFindManyArgs.Skip)
            .ApplyTake(bikeFindManyArgs.Take)
            .ApplyOrderBy(bikeFindManyArgs.SortBy)
            .ToListAsync();

        return rentals.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Update multiple Rentals records for Bike
    /// </summary>
    public async Task UpdateRentals(
        BikeWhereUniqueInput uniqueId,
        RentalWhereUniqueInput[] childrenIds
    )
    {
        var bike = await _context
            .Bikes.Include(t => t.Rentals)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (bike == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Rentals.Where(a => childrenIds.Select(x => x.Id).Contains(a.Id))
            .ToListAsync();

        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        bike.Rentals = children;
        await _context.SaveChangesAsync();
    }
}
