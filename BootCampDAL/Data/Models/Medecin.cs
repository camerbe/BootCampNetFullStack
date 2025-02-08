using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootCampDAL.Data.Models
{
    public class Medecin
    {
        [Key, ForeignKey(nameof(User))]
        public Guid Id { get; set; }
        public string? Inami  { get; set; }
        [ForeignKey(nameof(Specialite))]
        public Guid SpecialiteId { get; set; }
        
        public Specialite Specialite { get; set; }
        //public CrenauxHoraire CrenauxHoraire { get; set; }
        public User User { get; set; }
        public IEnumerable<RendezVous>? RendezVous { get; set; }
    }
}
