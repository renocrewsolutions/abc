using Microsoft.AspNetCore.Mvc;
using RentalService.APIs.Common;
using RentalService.Infrastructure.Models;

namespace RentalService.APIs.Dtos;

[BindProperties(SupportsGet = true)]
public class UserFindManyArgs : FindManyInput<User, UserWhereInput> { }
