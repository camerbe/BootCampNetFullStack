using BootCampDAL.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using BootCampDAL.Data.DTO;

namespace BootCampNetFullStack.BootCampDAL.Data.DTO
{
    public class PatientDTO : UserDTO
    {
        
        [Required]
        public DateTime Dob { get; set; }
        public string? Addresse { get; set; }
        public bool IsRegistered { get; set; }
       
    }
}
