using BootCampDAL.Data.Models;
using BootCampNetFullStack.BootCampDAL.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace BootCampDAL
{

    public class BootCampDalContext : IdentityDbContext<User, IdentityRole<Guid>, Guid, IdentityUserClaim<Guid>, IdentityUserRole<Guid>, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
    {
        public BootCampDalContext(DbContextOptions<BootCampDalContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new RoleConfiguration());
            // RoleSeeder.SeedRoleAsync(builder); // Removed this line as it causes CS1503

            builder.Entity<RendezVous>(rdv =>
            {
                rdv.ToTable(t => t.HasCheckConstraint("CK_RendezVous_Statut", "Statut IN ('Scheduled','Confirmed','Cancelled','Completed')"));
            });

            //builder.Entity<Medecin>()
            //    .HasOne(d => d.User)
            //    .WithOne()
            //    .HasForeignKey<Medecin>(d => d.Id);

            //builder.Entity<Medecin>()
            //    .HasOne(s => s.Specialite)
            //    .WithOne()
            //    .HasForeignKey<Medecin>(s => s.Id);

            builder.Entity<RendezVous>()
                .HasOne(r => r.Patient)
                .WithMany(p => p.RendezVous)
                .HasForeignKey(r => r.PatientId)
                .OnDelete(DeleteBehavior.NoAction)
                .HasPrincipalKey(p => p.Id);

            builder.Entity<RendezVous>()
                .HasOne(r => r.Medecin)
                .WithMany(m => m.RendezVous)
                .HasForeignKey(r => r.MedecinId)
                .OnDelete(DeleteBehavior.NoAction) // Set foreign key to null on delete
                .HasPrincipalKey(m => m.Id);

            builder.Entity<IdentityUserRole<Guid>>();
            builder.Entity<IdentityUserClaim<Guid>>();
            builder.Entity<IdentityUserLogin<Guid>>();
            builder.Entity<IdentityUserToken<Guid>>();
            builder.Entity<IdentityRoleClaim<Guid>>();

        }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Medecin> Medecins { get; set; }
        public DbSet<Specialite> Specialites { get; set; }
        public DbSet<CrenauxHoraire> CrenauxHoraires { get; set; }
        public DbSet<RendezVous> RendezVous { get; set; }
    }
}
