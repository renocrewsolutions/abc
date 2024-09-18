using RentalService.APIs.Common;
using RentalService.APIs.Dtos;

namespace RentalService.APIs;

public interface IUsersService
{
    /// <summary>
    /// Create one User
    /// </summary>
    public Task<User> CreateUser(UserCreateInput user);

    /// <summary>
    /// Delete one User
    /// </summary>
    public Task DeleteUser(UserWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Users
    /// </summary>
    public Task<List<User>> Users(UserFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about User records
    /// </summary>
    public Task<MetadataDto> UsersMeta(UserFindManyArgs findManyArgs);

    /// <summary>
    /// Get one User
    /// </summary>
    public Task<User> User(UserWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one User
    /// </summary>
    public Task UpdateUser(UserWhereUniqueInput uniqueId, UserUpdateInput updateDto);

    /// <summary>
    /// Connect multiple Rentals records to User
    /// </summary>
    public Task ConnectRentals(UserWhereUniqueInput uniqueId, RentalWhereUniqueInput[] rentalsId);

    /// <summary>
    /// Disconnect multiple Rentals records from User
    /// </summary>
    public Task DisconnectRentals(
        UserWhereUniqueInput uniqueId,
        RentalWhereUniqueInput[] rentalsId
    );

    /// <summary>
    /// Find multiple Rentals records for User
    /// </summary>
    public Task<List<Rental>> FindRentals(
        UserWhereUniqueInput uniqueId,
        RentalFindManyArgs RentalFindManyArgs
    );

    /// <summary>
    /// Update multiple Rentals records for User
    /// </summary>
    public Task UpdateRentals(UserWhereUniqueInput uniqueId, RentalWhereUniqueInput[] rentalsId);
}
