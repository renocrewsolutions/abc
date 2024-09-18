using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RentalService.Infrastructure.Models;

namespace RentalService.Infrastructure;

public class RentalServiceDbContext : IdentityDbContext<IdentityUser>
{
    public RentalServiceDbContext(DbContextOptions<RentalServiceDbContext> options)
        : base(options) { }

    public DbSet<RentalDbModel> Rentals { get; set; }

    public DbSet<BikeDbModel> Bikes { get; set; }

    public DbSet<PaymentDbModel> Payments { get; set; }

    public DbSet<UserDbModel> Users { get; set; }
}
