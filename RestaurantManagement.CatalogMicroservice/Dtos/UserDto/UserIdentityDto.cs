using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantManagement.CatalogMicroservice.Dtos.UserDto
{
    public class UserIdentityDto
    {
        public string? Name { get; set; }
        public string? Token { get; set; }
    }
}