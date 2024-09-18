using Microsoft.AspNetCore.Mvc;
using RentalService.APIs.Common;
using RentalService.Infrastructure.Models;

namespace RentalService.APIs.Dtos;

[BindProperties(SupportsGet = true)]
public class RentalFindManyArgs : FindManyInput<Rental, RentalWhereInput> { }
