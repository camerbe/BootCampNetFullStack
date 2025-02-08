using BootCampDAL.Data.DTO;
using System.ComponentModel.DataAnnotations;

namespace BootCampNetFullStack.BootCampDAL.Data.DTO
{
    public class PatientResponseDTO
    {
        public DateTime Dob { get; set; }
        public string? Addresse { get; set; }
        public bool IsRegistered { get; set; }
        public UserDTO User { get; set; }
        public string FullName { get; set; }
    }
}
