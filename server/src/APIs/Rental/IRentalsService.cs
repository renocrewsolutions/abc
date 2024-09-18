using RentalService.APIs.Common;
using RentalService.APIs.Dtos;

namespace RentalService.APIs;

public interface IRentalsService
{
    /// <summary>
    /// Create one Rental
    /// </summary>
    public Task<Rental> CreateRental(RentalCreateInput rental);

    /// <summary>
    /// Delete one Rental
    /// </summary>
    public Task DeleteRental(RentalWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Rentals
    /// </summary>
    public Task<List<Rental>> Rentals(RentalFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Rental records
    /// </summary>
    public Task<MetadataDto> RentalsMeta(RentalFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Rental
    /// </summary>
    public Task<Rental> Rental(RentalWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Rental
    /// </summary>
    public Task UpdateRental(RentalWhereUniqueInput uniqueId, RentalUpdateInput updateDto);

    /// <summary>
    /// Get a Bike record for Rental
    /// </summary>
    public Task<Bike> GetBike(RentalWhereUniqueInput uniqueId);

    /// <summary>
    /// Connect multiple Payments records to Rental
    /// </summary>
    public Task ConnectPayments(
        RentalWhereUniqueInput uniqueId,
        PaymentWhereUniqueInput[] paymentsId
    );

    /// <summary>
    /// Disconnect multiple Payments records from Rental
    /// </summary>
    public Task DisconnectPayments(
        RentalWhereUniqueInput uniqueId,
        PaymentWhereUniqueInput[] paymentsId
    );

    /// <summary>
    /// Find multiple Payments records for Rental
    /// </summary>
    public Task<List<Payment>> FindPayments(
        RentalWhereUniqueInput uniqueId,
        PaymentFindManyArgs PaymentFindManyArgs
    );

    /// <summary>
    /// Update multiple Payments records for Rental
    /// </summary>
    public Task UpdatePayments(
        RentalWhereUniqueInput uniqueId,
        PaymentWhereUniqueInput[] paymentsId
    );

    /// <summary>
    /// Get a User record for Rental
    /// </summary>
    public Task<User> GetUser(RentalWhereUniqueInput uniqueId);
}
