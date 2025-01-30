using BootCampNetFullStack.BootCampDAL.Data.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootCampDAL.Data.Models
{
    
    public class User :IdentityUser<Guid>
    {
        
        [Required]
        public string Nom { get; set; }
        [Required]
        public string Prenom { get; set; }
        
        public string? Email { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? LastUpdatedAt { get; set; }
        public bool IsActive { get; set; } = false;
        public string? Tel { get; set; }
        public IList<Patient>? Patients { get; set; }
        public IList<Medecin>? Medecins { get; set; }
    }
}
