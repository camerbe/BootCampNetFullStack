using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootCampDAL.Data.Models
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole<Guid>>
    {
        public void Configure(EntityTypeBuilder<IdentityRole<Guid>> builder)
        {
            //RoleSeeder.SeedRoleAsync(builder.);
            //var medecinGuid = Guid.NewGuid();
            //var patientGuid = Guid.NewGuid();
            //var adminGuid = Guid.NewGuid();
            //var secretaireGuid = Guid.NewGuid();

            //builder.HasData(
            //    new IdentityRole<Guid>
            //    {
            //        Id = medecinGuid,
            //        Name = "Medecin",
            //        NormalizedName = "Medecin".ToUpper(),
            //        ConcurrencyStamp = medecinGuid.ToString(),
            //    },
            //    new IdentityRole<Guid>
            //    {
            //        Id = Guid.NewGuid(),
            //        Name = "Patient",
            //        NormalizedName = "PATIENT",
            //        ConcurrencyStamp = patientGuid.ToString()
            //    },
            //    new IdentityRole<Guid>
            //    {
            //        Id = adminGuid,
            //        Name = "Admin",
            //        NormalizedName = "ADMIN",
            //        ConcurrencyStamp = adminGuid.ToString()
            //    },
            //    new IdentityRole<Guid>
            //    {
            //        Id = secretaireGuid,
            //        Name = "Secretaire",
            //        NormalizedName = "SECRETAIRE",
            //        ConcurrencyStamp = secretaireGuid.ToString()
            //    }
            //);
        }
    }
}
