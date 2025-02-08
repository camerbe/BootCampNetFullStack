using BootCampDAL.Data.DTO;
using BootCampDAL.Data.Models;
using BootCampDAL.Data.Repository.IRepository;
using BootCampNetFullStack.BootCampDAL.Data.DTO;
using BootCampNetFullStack.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BootCampNetFullStack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecialiteController : Controller
    {
        private readonly ILogger<SpecialiteController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public SpecialiteController(ILogger<SpecialiteController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        [HttpPost]
        public async Task<IActionResult> CreateSpecialite([FromBody] SpecialiteRequestDTO specialiteRequestDTO)
        {
            if (!ModelState.IsValid) return BadRequest();
            var specialite = new Specialite
            {
                Titre = specialiteRequestDTO.Titre.Capitalize(),
                Description = specialiteRequestDTO.Description,

            };
            await _unitOfWork.Specialite.Add(specialite);
            await _unitOfWork.Save();
            var specialiteResponseDTO = new SpecialiteResponseDTO
            {
                Id = specialite.Id,
                Titre = specialite.Titre,
                Description = specialite.Description
            };
            return Ok(specialiteResponseDTO);
        }
        
        /// <summary>
        /// Récupère la liste de tous les utilisateurs.
        /// </summary>
        /// <returns>
        /// Une tâche représentant l'opération asynchrone, qui contient une réponse HTTP OK avec la liste des utilisateurs si l'opération réussit.
        /// </returns>
        /// <remarks>
        /// Cette méthode est une action HTTP GET qui récupère tous les utilisateurs à partir du dépôt d'unité de travail.
        /// </remarks>
        /// <seealso cref="M:YourNamespace.IUnitOfWork.User.GetAll"/>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Specialite>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSpecialites()
        {
            var specialites = (await _unitOfWork.Specialite.GetAll()).OrderBy(s => s.Titre);
            return Ok(specialites);
        }
        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetSpecialite(Guid id)
        {
            var specialite = await _unitOfWork.Specialite.Get(a => a.Id == id);
            return specialite != null ? Ok(specialite) : NotFound();

        }
        /// <summary>
        /// Deletes a user with the specified unique identifier.
        /// </summary>
        /// <param name="id">The GUID of the user to delete.</param>
        /// 
        /// <response code="200">The user was successfully deleted.</response>
        /// <response code="404">The user with the specified ID was not found.</response>
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteSpecialite([FromRoute] Guid id)
        {
            var user = await _unitOfWork.Specialite.Get(x => x.Id == id);
            if (user == null) return NotFound();
            await _unitOfWork.Specialite.Remove(id);
            await _unitOfWork.Save(); 
            return Ok();

        }
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateSpecialite([FromRoute] Guid id, [FromBody] SpecialiteRequestDTO specialiteDTO)
        {
            var specialiteToUpdate = await _unitOfWork.Specialite.Get(x =>x.Id==id);
            if (specialiteToUpdate == null) return NotFound();
            //
            if (!string.Equals(specialiteToUpdate.Titre, specialiteDTO.Titre))
            {
                specialiteToUpdate.Titre = specialiteDTO.Titre.Capitalize();
            }

            if (!string.Equals(specialiteToUpdate.Description, specialiteDTO.Description))
            {
                specialiteToUpdate.Description = specialiteDTO.Description;
            }
            
            await _unitOfWork.Specialite.Update(specialiteToUpdate);
            await _unitOfWork.Save();
            return Ok(specialiteToUpdate);


        }
    }
}
