using RentalService.APIs.Dtos;
using RentalService.Infrastructure.Models;

namespace RentalService.APIs.Extensions;

public static class BikesExtensions
{
    public static Bike ToDto(this BikeDbModel model)
    {
        return new Bike
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            Location = model.Location,
            Model = model.Model,
            Rentals = model.Rentals?.Select(x => x.Id).ToList(),
            Status = model.Status,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static BikeDbModel ToModel(this BikeUpdateInput updateDto, BikeWhereUniqueInput uniqueId)
    {
        var bike = new BikeDbModel
        {
            Id = uniqueId.Id,
            Location = updateDto.Location,
            Model = updateDto.Model,
            Status = updateDto.Status
        };

        if (updateDto.CreatedAt != null)
        {
            bike.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            bike.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return bike;
    }
}
