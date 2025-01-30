using BootCampDAL.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootCampDAL.Data.DTO
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        [Required]
        public string Nom { get; set; }
        [Required]
        public string Prenom { get; set; }
        public string Password { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? LastUpdatedAt { get; set; }
        public bool IsActive { get; set; } = false;
        public string Tel { get; set; }=string.Empty;
        public string Role { get; set; }
    }
}
