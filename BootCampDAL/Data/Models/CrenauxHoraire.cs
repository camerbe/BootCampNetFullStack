using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootCampDAL.Data.Models
{
    public enum JourSemaine
    {
        Lundi=1,
        Mardi=2,
        Mercredi=3,
        Jeudi=4,
        Vendredi=5,
        Samedi=6,
        Dimanche=7
    }
    public class CrenauxHoraire
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Debut { get; set; }
        public TimeSpan Fin { get; set; }
        [ForeignKey(nameof(Medecin))]
        public Guid MedecinId { get; set; }
        public Medecin Medecin { get; set; }

    }
}
