using BootCampDAL.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace BootCampNetFullStack.BootCampDAL.Data.DTO
{
    public class RendezVousResponseDTO
    {
        public Guid Id { get; set; }
        public TimeSpan Debut { get; set; }
        public TimeSpan Fin { get; set; }
        public DateTime DateRdv { get; set; }
        public Guid PatientId { get; set; }
        public Guid MedecinId { get; set; }
        public MedecinResponseDTO Medecin { get; set; }
        public PatientDTO Patient { get; set; }
        public string Statut { get; set; } = "Scheduled";
    }
}
