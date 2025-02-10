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
            var MedecinResponseDTO = new MedecinResponseDTO
            {
                Inami = medecin.Inami,
                SpecialiteId = medecin.SpecialiteId,
                Specialite = await _unitOfWork.Specialite.Get(x => x.Id == medecin.SpecialiteId),
                User = new UserDTO
                {
                    Id = user.Id,
                    Nom = user.Nom,
                    Prenom = user.Prenom,
                    Email = user.Email,
                    CreatedAt = user.CreatedAt,
                    LastUpdatedAt = user.LastUpdatedAt,
                    IsActive = user.IsActive,
                    Tel = user.Tel,
                    Role = (await _userManager.GetRolesAsync(user)).FirstOrDefault()
                },
                FullName = $"{user.Prenom} {user.Nom}"
            };
            return Accepted(MedecinResponseDTO);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetMedecin(Guid id)
        {
            var userMedecin = await _userManager.Users.Include(x => x.Medecins).FirstOrDefaultAsync(x => x.Id == id);
            if (userMedecin == null) return NotFound();

            var medecin = userMedecin.Medecins.FirstOrDefault();
            if (medecin == null) return NotFound();

            var medecinResponseDTO = new MedecinResponseDTO
            {
                Inami = medecin.Inami,
                SpecialiteId = medecin.SpecialiteId,
                Specialite = await _unitOfWork.Specialite.Get(x => x.Id == medecin.SpecialiteId),
                User = new UserDTO
                {
                    Id = userMedecin.Id,
                    Nom = userMedecin.Nom,
                    Prenom = userMedecin.Prenom,
                    Email = userMedecin.Email,
                    CreatedAt = userMedecin.CreatedAt,
                    LastUpdatedAt = userMedecin.LastUpdatedAt,
                    IsActive = userMedecin.IsActive,
                    Tel = userMedecin.Tel,
                    Role = (await _userManager.GetRolesAsync(userMedecin)).FirstOrDefault()
                },
                FullName = $"{userMedecin.Prenom} {userMedecin.Nom}"
            };

            return Ok(medecinResponseDTO);
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
            //var medecins = (await _unitOfWork.Medecin.GetAll()).OrderBy(x => x.User.Nom);
            //return Accepted(medecins);
            var userMedecins = _userManager.Users.Include(x => x.Medecins).OrderBy(n => n.Nom);
            List<MedecinResponseDTO> listMedecin = new List<MedecinResponseDTO>();
            if (userMedecins != null)
            {
                foreach (var userMedecin in userMedecins)
                {
                    var medecin = userMedecin.Medecins.FirstOrDefault();
                    if (medecin != null)
                    {
                        var medecinResponseDTO = new MedecinResponseDTO
                        {
                            Inami = medecin.Inami,
                            SpecialiteId = medecin.SpecialiteId,
                            Specialite = await _unitOfWork.Specialite.Get(x => x.Id == medecin.SpecialiteId),
                            User = new UserDTO
                            {
                                Id = userMedecin.Id,
                                Nom = userMedecin.Nom,
                                Prenom = userMedecin.Prenom,
                                Email = userMedecin.Email,
                                CreatedAt = userMedecin.CreatedAt,
                                LastUpdatedAt = userMedecin.LastUpdatedAt,
                                IsActive = userMedecin.IsActive,
                                Tel = userMedecin.Tel,
                                Role = "Medecin"//(await _userManager.GetRolesAsync(userMedecin)).FirstOrDefault()
                            },
                            FullName = $"{userMedecin.Prenom} {userMedecin.Nom}"
                        };
                        listMedecin.Add(medecinResponseDTO);
                    }
                }
            }
            return Accepted(listMedecin);
        }
    }
}
