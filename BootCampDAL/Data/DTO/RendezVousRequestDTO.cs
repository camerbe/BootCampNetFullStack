using BootCampDAL.Data.Models;
using BootCampNetFullStack.BootCampDAL.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BootCampNetFullStack.Controllers
{
    public class RendezVousRequestDTO
    {
        public Guid Id { get; set; }
        public TimeSpan Debut { get; set; }
        public TimeSpan Fin { get; set; }
        public DateTime DateRdv { get; set; }
        public Guid PatientId { get; set; }
        public Guid MedecinId { get; set; }
        [Required]
        public string Statut { get; set; } = Status.Scheduled;
    }
}