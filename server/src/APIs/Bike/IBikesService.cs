using RentalService.APIs.Common;
using RentalService.APIs.Dtos;

namespace RentalService.APIs;

public interface IBikesService
{
    /// <summary>
    /// Create one Bike
    /// </summary>
    public Task<Bike> CreateBike(BikeCreateInput bike);

    /// <summary>
    /// Delete one Bike
    /// </summary>
    public Task DeleteBike(BikeWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Bikes
    /// </summary>
    public Task<List<Bike>> Bikes(BikeFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Bike records
    /// </summary>
    public Task<MetadataDto> BikesMeta(BikeFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Bike
    /// </summary>
    public Task<Bike> Bike(BikeWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Bike
    /// </summary>
    public Task UpdateBike(BikeWhereUniqueInput uniqueId, BikeUpdateInput updateDto);

    /// <summary>
    /// Connect multiple Rentals records to Bike
    /// </summary>
    public Task ConnectRentals(BikeWhereUniqueInput uniqueId, RentalWhereUniqueInput[] rentalsId);

    /// <summary>
    /// Disconnect multiple Rentals records from Bike
    /// </summary>
    public Task DisconnectRentals(
        BikeWhereUniqueInput uniqueId,
        RentalWhereUniqueInput[] rentalsId
    );

    /// <summary>
    /// Find multiple Rentals records for Bike
    /// </summary>
    public Task<List<Rental>> FindRentals(
        BikeWhereUniqueInput uniqueId,
        RentalFindManyArgs RentalFindManyArgs
    );

    /// <summary>
    /// Update multiple Rentals records for Bike
    /// </summary>
    public Task UpdateRentals(BikeWhereUniqueInput uniqueId, RentalWhereUniqueInput[] rentalsId);
}
