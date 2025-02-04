using BootCampDAL.Data.DTO;
using BootCampDAL.Data.Models;
using BootCampDAL.Data.Repository.IRepository;
using BootCampNetFullStack.BootCampDAL.Data.DTO;
using BootCampNetFullStack.BootCampDAL.Data.Models;
using BootCampNetFullStack.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;



namespace BootCampNetFullStack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _unitOfWork ;
        private readonly ILogger<PatientController> _logger;

        public PatientController(
            UserManager<User> userManager, 
            IUnitOfWork unitOfWork, 
            ILogger<PatientController> logger
            )
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        [HttpPost]
        public async Task<IActionResult> CreatePatient([FromBody] PatientDTO patientDTO)
        {
            if (!ModelState.IsValid) {
                return BadRequest();
            }
            var user = new User
            {
                Nom=patientDTO.Nom.ToUpperCase(),
                Prenom=patientDTO.Prenom.Capitalize(),
                Email=patientDTO.Email,
                CreatedAt = patientDTO.CreatedAt,
                LastUpdatedAt = patientDTO.LastUpdatedAt,
                IsActive = patientDTO.IsActive,
                Tel = patientDTO.Tel,

            };
            user.Email = patientDTO.Email ?? patientDTO.Prenom + patientDTO.Nom;
            user.EmailConfirmed = patientDTO.Email.IsNullOrEmpty() ? false : true;
            user.UserName = patientDTO.Email ?? patientDTO.Prenom + patientDTO.Nom;
            var result = await _userManager.CreateAsync(user, patientDTO.Password);
            if (!result.Succeeded) return BadRequest();

            var resultRole = await _userManager.AddToRoleAsync(user, patientDTO.Role);
            if (!resultRole.Succeeded) return BadRequest();

            var patient = new Patient
            {
                Id=user.Id,
                Dob = patientDTO.Dob,
                Addresse = patientDTO.Addresse,
                IsRegistered = patientDTO.IsRegistered
                
            };
            await _unitOfWork.Patient.Add(patient);
            await _unitOfWork.Save();
            return Accepted(patient);

        }
        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPatient(Guid id)
        {
            var patient = await _unitOfWork.Patient.Get(x => x.Id == id);
            if (patient == null) return NotFound();
            var user = await _userManager.FindByIdAsync(id.ToString());
            patient.User = user;
            var patientResponseDTO = new PatientResponseDTO
            {
                Addresse = patient.Addresse,
                Dob = patient.Dob,
                IsRegistered = patient.IsRegistered,
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
                }
            };
            return Ok(patientResponseDTO);
        }
        /// <summary>
        /// Deletes a user with the specified unique identifier.
        /// </summary>
        /// <param name="id">The GUID of the user to delete.</param>
        /// 
        /// <response code="200">The user was successfully deleted.</response>
        /// <response code="404">The user with the specified ID was not found.</response>
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeletePatient([FromRoute] Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null) return NotFound();
            await _userManager.DeleteAsync(user);
            return Ok();

        }
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdatePatient([FromRoute] Guid id, [FromBody] PatientDTO patientDTO)
        {
            var patientToUpdate = await _unitOfWork.Patient.Get(x => x.Id == id);
            if (patientToUpdate == null) return NotFound();

            if (!string.Equals(patientToUpdate.Addresse, patientDTO.Addresse))
            {
                patientToUpdate.Addresse = patientDTO.Addresse;
            }

            if (!string.Equals(patientToUpdate.Dob, patientDTO.Dob))
            {
                patientToUpdate.Dob = patientDTO.Dob;
            }

            if (!string.Equals(patientToUpdate.IsRegistered, patientDTO.IsRegistered))
            {
                patientToUpdate.IsRegistered = patientDTO.IsRegistered;
            }

            await _unitOfWork.Patient.Update(patientToUpdate);
            await _unitOfWork.Save();

            return Ok(patientToUpdate);
        }
    }
}
