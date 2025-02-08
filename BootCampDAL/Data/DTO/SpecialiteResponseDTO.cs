using BootCampDAL.Data.Models;

namespace BootCampNetFullStack.BootCampDAL.Data.DTO
{
    public class SpecialiteResponseDTO
    {
        public Guid Id { get; set; }
        public string Titre { get; set; }
        public string? Description { get; set; }
        public IEnumerable<Medecin> Medecins { get; set; }
    }
}
