using BootCampDAL.Data.Models;
using BootCampNetFullStack.BootCampDAL.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BootCampNetFullStack.Controllers
{
    public class RendezVousRequestDTO
    {
        public Guid Id { get; set; }
        
        [Column(TypeName = "time")]
        public TimeSpan Fin { get; set; }
        public DateTime DateRdv { get; set; }
        public Guid PatientId { get; set; }
        public Guid MedecinId { get; set; }
        [Column(TypeName = "time")]
        public TimeSpan Debut { get; set; }
        [Required]
        public string Statut { get; set; } = "Scheduled";
    }
}