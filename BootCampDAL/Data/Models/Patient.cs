using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using BootCampDAL.Data.Models;

namespace BootCampNetFullStack.BootCampDAL.Data.Models
{
    public class Patient
    {
        [Key, ForeignKey(nameof(User))]
        public Guid Id { get; set; }
        [Required]
        public DateTime Dob { get; set; }
        public string? Addresse { get; set; }
        public bool IsRegistered { get; set; } = true;
        public User User { get; set; }
        public IEnumerable<RendezVous> RendezVous { get; set; }
        
    }

}
