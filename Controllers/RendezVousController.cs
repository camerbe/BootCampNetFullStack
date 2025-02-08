using BootCampDAL.Data.Models;
using BootCampDAL.Data.Repository.IRepository;
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
                Debut = rendezVousRequestDTO.Debut,
                Fin = rendezVousRequestDTO.Debut,
                PatientId = rendezVousRequestDTO.PatientId,
                MedecinId = rendezVousRequestDTO.MedecinId,
                Statut = rendezVousRequestDTO.Statut
               
            };
            await _unitOfWork.RendezVous.Add(rendezVous);
            await _unitOfWork.Save();
            return Ok();
        }
    }
}
