﻿using BootCampDAL.Data.DTO;
using BootCampDAL.Data.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BootCampNetFullStack.BootCampDAL.Data.DTO
{
    public class MedecinResponseDTO
    {
        public Guid Id { get; set; }
        public string? Inami { get; set; }
        public Guid? SpecialiteId { get; set; }
        public Specialite Specialite { get; set; }
        public UserDTO User { get; set; }
        public string FullName { get; set; }

        
    }
}
