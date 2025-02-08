using BootCampDAL.Data.DTO;
using BootCampDAL.Data.Models;
using BootCampDAL.Data.Repository.IRepository;
using BootCampNetFullStack.BootCampDAL.Data.DTO;
using BootCampNetFullStack.BootCampDAL.Data.Models;
using BootCampNetFullStack.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            var patientUser = await _unitOfWork.Patient.Get(x => x.Id == id,u =>u.User);
            if (patientUser == null) return NotFound();
            var patientResponseDTO = new PatientResponseDTO
            {
                Addresse = patientUser.Addresse,
                Dob = patientUser.Dob,
                IsRegistered = patientUser.IsRegistered,
                User = new UserDTO
                {
                    Id = patientUser.User.Id,
                    Nom = patientUser.User.Nom,
                    Prenom = patientUser.User.Prenom,
                    Email = patientUser.User.Email,
                    CreatedAt = patientUser.User.CreatedAt,
                    LastUpdatedAt = patientUser.User.LastUpdatedAt,
                    IsActive = patientUser.User.IsActive,
                    Tel = patientUser.User.Tel,
                    Role = (await _userManager.GetRolesAsync(patientUser.User)).FirstOrDefault()
                },
                FullName = $"{patientUser.User.Prenom} {patientUser.User.Nom}"
            };
            ////var userPatient = await _userManager.Users.Include(x => x.Patients).FirstOrDefaultAsync(k => k.Id == id);

            //if (userPatient == null) return NotFound();
            //var patientResponseDTO = new PatientResponseDTO();
            //foreach (var patient in userPatient.Patients)
            //{
            //    patientResponseDTO.Addresse = patient.Addresse;
            //    patientResponseDTO.Dob = patient.Dob;
            //    patientResponseDTO.IsRegistered = patient.IsRegistered;
            //    patientResponseDTO.User = new UserDTO
            //    {
            //        Id = userPatient.Id,
            //        Nom = userPatient.Nom,
            //        Prenom = userPatient.Prenom,
            //        Email = userPatient.Email,
            //        CreatedAt = userPatient.CreatedAt,
            //        LastUpdatedAt = userPatient.LastUpdatedAt,
            //        IsActive = userPatient.IsActive,
            //        Tel = userPatient.Tel,
            //        Role = (await _userManager.GetRolesAsync(userPatient)).FirstOrDefault()
            //    };
            //    patientResponseDTO.FullName = $"{userPatient.Prenom} {userPatient.Nom}";
            //}

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
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Patient>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPatients()
        {
            var patients = (await _unitOfWork.Patient.GetAll()).OrderBy(p => p.User.Nom);
            return Ok(patients);
        }
    }
}
