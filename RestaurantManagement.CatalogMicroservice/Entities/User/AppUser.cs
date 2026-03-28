using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.Identity.MongoDbCore.Models;

namespace RestaurantManagement.CatalogMicroservice.Entities.User
{
    public class AppUser : MongoIdentityUser<Guid>
    {
        public string? Name { get; set; }
    }
}