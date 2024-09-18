using RentalService.APIs.Dtos;
using RentalService.Infrastructure.Models;

namespace RentalService.APIs.Extensions;

public static class RentalsExtensions
{
    public static Rental ToDto(this RentalDbModel model)
    {
        return new Rental
        {
            Bike = model.BikeId,
            CreatedAt = model.CreatedAt,
            EndTime = model.EndTime,
            Id = model.Id,
            Payments = model.Payments?.Select(x => x.Id).ToList(),
            Price = model.Price,
            StartTime = model.StartTime,
            UpdatedAt = model.UpdatedAt,
            User = model.UserId,
        };
    }

    public static RentalDbModel ToModel(
        this RentalUpdateInput updateDto,
        RentalWhereUniqueInput uniqueId
    )
    {
        var rental = new RentalDbModel
        {
            Id = uniqueId.Id,
            EndTime = updateDto.EndTime,
            Price = updateDto.Price,
            StartTime = updateDto.StartTime
        };

        if (updateDto.Bike != null)
        {
            rental.BikeId = updateDto.Bike;
        }
        if (updateDto.CreatedAt != null)
        {
            rental.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            rental.UpdatedAt = updateDto.UpdatedAt.Value;
        }
        if (updateDto.User != null)
        {
            rental.UserId = updateDto.User;
        }

        return rental;
    }
}
