using BootCampDAL.Data.Models;
using BootCampDAL.Data.Repository.IRepository;
using BootCampDAL.Service;
using BootCampNetFullStack.BootCampDAL.Data.DTO;
using BootCampNetFullStack.BootCampDAL.Data.Models;
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
                Nom=patientDTO.Nom,
                Prenom=StringFormatter.ToTitleCase(patientDTO.Prenom),
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
    }
}
