﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BootCampNetFullStack.BootCampDAL.Data.Models;

namespace BootCampDAL.Data.Models
{
    public static class Status
    {
        public static string Scheduled { get; } = "Scheduled";
        public static string Confirmed { get; } = "Confirmed";
        public static string Cancelled { get; } = "Cancelled";
        public static string Completed { get; } = "Completed";

    }
    public class RendezVous
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public TimeSpan Debut { get; set; }
        public TimeSpan Fin { get; set; }
        public DateTime DateRdv { get; set; }
        public Guid PatientId { get; set; }
        public Guid MedecinId { get; set; }
        [ForeignKey("MedecinId")]
        public Medecin Medecin { get; set; }
        [ForeignKey("PatientId")]
        public Patient Patient { get; set; }
        [Required]
        public string Statut { get; set; } = Status.Scheduled;

    }
}
