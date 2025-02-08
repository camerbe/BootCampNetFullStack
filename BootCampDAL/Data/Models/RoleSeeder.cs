using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootCampDAL.Data.Models
{
    public static class RoleSeeder
    {
        public static async Task SeedRoleAsync(RoleManager<IdentityRole> roleManager)
        {
            if( !await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin")
                {
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    Id = Guid.NewGuid().ToString(),
                    Name= "Admin"
                });
            }
            if (!await roleManager.RoleExistsAsync("Medecin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Medecin")
                {
                    NormalizedName = "MEDECIN",
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    Id = Guid.NewGuid().ToString(),
                    Name = "Medecin"
                });
            }
            if ( !await roleManager.RoleExistsAsync("Patient"))
            {
                await roleManager.CreateAsync(new IdentityRole("Patient") {
                    Name = "Patient",
                    NormalizedName = "PATIENT",
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    Id = Guid.NewGuid().ToString()
                });
            }
       
            if( !await roleManager.RoleExistsAsync("Secretaire"))
            {
                await roleManager.CreateAsync(new IdentityRole("Secretaire")
                {
                    Name = "Secretaire",
                    NormalizedName = "SECRETAIRE",
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    Id = Guid.NewGuid().ToString()
                });
            }
        }
    }
}
