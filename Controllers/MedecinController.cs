using BootCampDAL.Data.DTO;
using BootCampDAL.Data.Models;
using BootCampDAL.Data.Repository.IRepository;
using BootCampNetFullStack.BootCampDAL.Data.DTO;
using BootCampNetFullStack.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BootCampNetFullStack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedecinController : ControllerBase
    {
        private readonly ILogger<MedecinController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        public MedecinController(
            ILogger<MedecinController> logger,
            IUnitOfWork unitOfWork,
            UserManager<User> userManager
            )
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }
        [HttpPost]
        public async Task<IActionResult> CreateMedecin([FromBody] MedecinRequestDTO medecinRequestDTO)
        {
            if (!ModelState.IsValid) return BadRequest();
            var user = new User
            {
                Nom = medecinRequestDTO.Nom.ToUpperCase(),
                Prenom = medecinRequestDTO.Prenom.Capitalize(),
                Email = medecinRequestDTO.Email,
                CreatedAt = medecinRequestDTO.CreatedAt,
                LastUpdatedAt = medecinRequestDTO.LastUpdatedAt,
                IsActive = medecinRequestDTO.IsActive,
                Tel = medecinRequestDTO.Tel,
                
            };
            user.Email = medecinRequestDTO.Email ?? medecinRequestDTO.Prenom + medecinRequestDTO.Nom;
            user.EmailConfirmed = medecinRequestDTO.Email.IsNullOrEmpty() ? false : true;
            user.UserName = medecinRequestDTO.Email ?? medecinRequestDTO.Prenom + medecinRequestDTO.Nom;
            var result = await _userManager.CreateAsync(user, medecinRequestDTO.Password);

            if (!result.Succeeded) return BadRequest();
            var resultRole = await _userManager.AddToRoleAsync(user, medecinRequestDTO.Role);

            if (!resultRole.Succeeded) return BadRequest();
            var medecin = new Medecin
            {
                Id = user.Id,
                SpecialiteId = medecinRequestDTO.SpecialiteId,
                Inami = medecinRequestDTO.Inami,
                 
            };
            await _unitOfWork.Medecin.Add(medecin);
            await _unitOfWork.Save();
            return Accepted(medecin);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetMedecin(Guid id)
        {
            var medecinUser = await _unitOfWork.Medecin.Get(x => x.Id == id,u =>u.User);
            if (medecinUser == null) return NotFound();
            var medecinResponseDTO = new MedecinResponseDTO
            {
                Inami = medecinUser.Inami,
                SpecialiteId = medecinUser.SpecialiteId,
                Specialite = await _unitOfWork.Specialite.Get(x => x.Id == medecinUser.SpecialiteId),
                User = new UserDTO
                {
                    Id = medecinUser.Id,
                    Nom = medecinUser.User.Nom,
                    Prenom = medecinUser.User.Prenom,
                    Email = medecinUser.User.Email,
                    CreatedAt = medecinUser.User.CreatedAt,
                    LastUpdatedAt = medecinUser.User.LastUpdatedAt,
                    IsActive = medecinUser.User.IsActive,
                    Tel = medecinUser.User.Tel,
                    Role = (await _userManager.GetRolesAsync(medecinUser.User)).FirstOrDefault()
                },
                FullName = $"{medecinUser.User.Prenom} {medecinUser.User.Nom}"
            };
            //var userMedecin = await _userManager.Users.Include(x => x.Medecins).FirstOrDefaultAsync(x => x.Id == id);

            //if (userMedecin == null) return NotFound();
            //var medecinResponseDTO = new MedecinResponseDTO();
            //foreach (var toubib in userMedecin.Medecins)
            //{

            //    medecinResponseDTO.Inami = toubib.Inami;
            //    medecinResponseDTO.SpecialiteId = toubib.SpecialiteId;
            //    medecinResponseDTO.Specialite = await _unitOfWork.Specialite.Get(x => x.Id == toubib.SpecialiteId);
            //    medecinResponseDTO.User = new UserDTO
            //    {
            //          Id = userMedecin.Id,
            //          Nom = userMedecin.Nom,
            //          Prenom = userMedecin.Prenom,
            //          Email = userMedecin.Email,
            //          CreatedAt = userMedecin.CreatedAt,
            //          LastUpdatedAt = userMedecin.LastUpdatedAt,
            //          IsActive = userMedecin.IsActive,
            //          Tel = userMedecin.Tel,
            //          Role = (await _userManager.GetRolesAsync(userMedecin)).FirstOrDefault()
            //    };
            //    medecinResponseDTO.FullName = $"{userMedecin.Prenom} {userMedecin.Nom}";

            //}

            return Accepted(medecinResponseDTO);
        }
        /// <summary>
        /// Deletes a user with the specified unique identifier.
        /// </summary>
        /// <param name="id">The GUID of the user to delete.</param>
        /// 
        /// <response code="200">The user was successfully deleted.</response>
        /// <response code="404">The user with the specified ID was not found.</response>
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteMedecin([FromRoute] Guid id)
        {
            var medecin = await _userManager.FindByIdAsync(id.ToString());
            if (medecin == null) return NotFound();
            await _userManager.DeleteAsync(medecin);
            return Ok();
        }
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateMedecin([FromRoute] Guid id, [FromBody] MedecinRequestDTO medecinRequestDTO)
        {
            var medecin = await _unitOfWork.Medecin.Get(x => x.Id == id);
            if (medecin == null) return NotFound();
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null) return NotFound();
            user.Nom = medecinRequestDTO.Nom.ToUpperCase();
            user.Prenom = medecinRequestDTO.Prenom.Capitalize();
            user.Email = medecinRequestDTO.Email;
            user.CreatedAt = medecinRequestDTO.CreatedAt;
            user.LastUpdatedAt = medecinRequestDTO.LastUpdatedAt;
            user.IsActive = medecinRequestDTO.IsActive;
            user.Tel = medecinRequestDTO.Tel;
            user.Email = medecinRequestDTO.Email ?? medecinRequestDTO.Prenom + medecinRequestDTO.Nom;
            user.EmailConfirmed = medecinRequestDTO.Email.IsNullOrEmpty() ? false : true;
            user.UserName = medecinRequestDTO.Email ?? medecinRequestDTO.Prenom + medecinRequestDTO.Nom;
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded) return BadRequest();
            medecin.SpecialiteId = medecinRequestDTO.SpecialiteId;
            medecin.Inami = medecinRequestDTO.Inami;
            await _unitOfWork.Medecin.Update(medecin);
            await _unitOfWork.Save();
            return Ok(medecin);
        }
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Medecin>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMedecins()
        {
            var medecins = (await _unitOfWork.Medecin.GetAll()).OrderBy(x=>x.User.Nom);
            return Accepted(medecins);
        }
    }
}
