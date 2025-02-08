using BootCampNetFullStack.BootCampDAL.Data.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootCampDAL.Data.Models
{
    public class Specialite
    {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]   
            public Guid Id { get; set; }
            public string Titre { get; set; }
            public string? Description { get; set; }
            public IEnumerable<MedecinResponseDTO> Medecins { get; set; }
    }
}
