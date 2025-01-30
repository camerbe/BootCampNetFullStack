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
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }
            if (!await roleManager.RoleExistsAsync("Medecin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Medecin"));
            }
            if ( !await roleManager.RoleExistsAsync("Patient"))
            {
                await roleManager.CreateAsync(new IdentityRole("Patient"));
            }
       
            if( !await roleManager.RoleExistsAsync("Secrtaire"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }
        }
    }
}
