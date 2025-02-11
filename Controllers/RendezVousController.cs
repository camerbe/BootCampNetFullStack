using BootCampDAL.Data.DTO;
using BootCampDAL.Data.Models;
using BootCampDAL.Data.Repository.IRepository;
using BootCampNetFullStack.BootCampDAL.Data.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BootCampNetFullStack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RendezVousController : ControllerBase
    {
        private readonly ILogger<RendezVousController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public RendezVousController(ILogger<RendezVousController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        [HttpPost]
        public async Task<IActionResult> CreateRendezVous([FromBody] RendezVousRequestDTO rendezVousRequestDTO)
        {
            if (!ModelState.IsValid) return BadRequest();
            var rendezVous = new RendezVous
            {
                DateRdv = rendezVousRequestDTO.DateRdv,
                Debut = rendezVousRequestDTO.Debut, // Fix the method call
                Fin = rendezVousRequestDTO.Fin, // Fix the method call
                PatientId = rendezVousRequestDTO.PatientId,
                MedecinId = rendezVousRequestDTO.MedecinId,
                Statut = rendezVousRequestDTO.Statut
            };
            await _unitOfWork.RendezVous.Add(rendezVous);
            await _unitOfWork.Save();
            var insertedRendezVous = await _unitOfWork.RendezVous.Get(r => r.Id == rendezVous.Id, r => r.Patient.Then, r => r.Medecin); // Fix the method call

            if (insertedRendezVous == null) return Accepted(rendezVous);
            RendezVousResponseDTO rdvResponseDTO = new RendezVousResponseDTO
            {
                Id = insertedRendezVous.Id,
                DateRdv = insertedRendezVous.DateRdv,
                Debut = insertedRendezVous.Debut,
                Fin = insertedRendezVous.Fin,
                PatientId = (Guid)insertedRendezVous.PatientId,
                MedecinId = (Guid)insertedRendezVous.MedecinId,
                Statut = insertedRendezVous.Statut,
                Patient = new PatientDTO
                {
                    Id = insertedRendezVous.Patient.Id,
                    Dob = insertedRendezVous.Patient.Dob,
                    Addresse = insertedRendezVous.Patient.Addresse,
                    Email = insertedRendezVous.Patient.User.Email,
                    Tel = insertedRendezVous.Patient.User.Tel,

                },
                Medecin = new MedecinResponseDTO
                {
                    Id = insertedRendezVous.Medecin.Id,
                    SpecialiteId = insertedRendezVous.Medecin.SpecialiteId,
                    Inami = insertedRendezVous.Medecin.Inami,
                    User = new UserDTO
                    {
                        Id = insertedRendezVous.Medecin.User.Id,
                        Nom = insertedRendezVous.Medecin.User.Nom,
                        Prenom = insertedRendezVous.Medecin.User.Prenom,
                        Email = insertedRendezVous.Medecin.User.Email,
                        Tel = insertedRendezVous.Medecin.User.Tel,

                    }
                }
            };
            return Ok(rdvResponseDTO);
        }
    }
}
