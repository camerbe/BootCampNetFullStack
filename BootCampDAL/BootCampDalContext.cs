using BootCampDAL.Data.Models;
using BootCampNetFullStack.BootCampDAL.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BootCampDAL
{
    public class BootCampDalContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public BootCampDalContext(DbContextOptions<BootCampDalContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Patient>()
                .HasOne(p => p.User)
                .WithOne()
                .HasForeignKey<Patient>(p=>p.Id);
        }
        public DbSet<Patient> Patients { get; set; }
    }
}
