using BootCampDAL.Data.DTO;
using BootCampDAL.Data.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BootCampNetFullStack.BootCampDAL.Data.DTO
{
    public class MedecinRequestDTO : UserDTO
    {

        public string? Inami { get; set; }
        public Guid SpecialiteId { get; set; }
        
    }
}
