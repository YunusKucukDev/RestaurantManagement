using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using RestaurantManagement.CatalogMicroservice.Entities.User;

namespace RestaurantManagement.CatalogMicroservice.Data
{
    public static class SeedDatabase
    {
        public static async Task Initialize(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateAsyncScope();
            var serviceProvider = scope.ServiceProvider;

            var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<AppRole>>();

            // ROLLERİ OLUŞTUR
            string[] roleNames = { "Admin", "Customer" };
            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    // Burası önemli: Name set edilince Identity otomatik Normalizer çalıştırır
                    await roleManager.CreateAsync(new AppRole { Name = roleName });
                }
            }

            // KULLANICILARI OLUŞTUR
            if (userManager.Users.Count() == 0)
            {
                var adminUser = new AppUser { Name = "İlhan Elibol", UserName = "ilhanelibol35", Email = "ilhanelibol35@gmail.com" };
                var result = await userManager.CreateAsync(adminUser, "Aa12345.");

                if (result.Succeeded)
                {
                    // Rollerin varlığından emin olduktan sonra ata
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                    await userManager.AddToRoleAsync(adminUser, "Customer");
                }
            }
        }
    }
}