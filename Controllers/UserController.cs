using BootCampDAL.Data.DTO;
using BootCampDAL.Service;
using BootCampDAL.Data.Models;
using BootCampDAL.Data.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using BootCampNetFullStack.BootCampDAL.Data.DTO;

namespace BootCampNetFullStack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        readonly ILogger<UserController> _logger;

        

        public UserController(IUnitOfWork unitOfWork, 
            UserManager<User> userManager,
            ILogger<UserController> logger)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _logger = logger;
            // _roleManager = roleManager;
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
        [ProducesResponseType(typeof(IEnumerable<User>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUsers() 
        {
            var users = _unitOfWork.User.GetAll();
            return Ok(users);
            
        }
        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        
        public async Task<IActionResult> GetUser(Guid id) 
        {
            var user = await _unitOfWork.User.Get(a=>a.Id==id);
            return user != null ? Ok(user) : NotFound();
            
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserDTO usrdto)
        {
            if (!ModelState.IsValid) { 
                return BadRequest();
            }
            var user = new User
            {
                Nom = usrdto.Nom.ToUpper(),
                Prenom = StringFormatter.ToTitleCase(usrdto.Prenom),
                CreatedAt = usrdto.CreatedAt,
                LastUpdatedAt = usrdto.LastUpdatedAt,
                IsActive = usrdto.IsActive,
                Tel = usrdto.Tel,
            };
            user.Email = usrdto.Email ?? usrdto.Prenom + usrdto.Nom;
            user.EmailConfirmed = usrdto.Email.IsNullOrEmpty() ? false : true;
            user.UserName = usrdto.Email ?? usrdto.Prenom + usrdto.Nom;
            var result = await _userManager.CreateAsync(user, usrdto.Password);
            if (!result.Succeeded) return BadRequest();

           var resultRole=await _userManager.AddToRoleAsync(user, usrdto.Role);
           if (!resultRole.Succeeded) return BadRequest();

            var responseDto = new UserResponseDTO
            {
                Id = user.Id,
                CreatedAt = user.CreatedAt,
                LastUpdatedAt = user.LastUpdatedAt,
                IsActive = user.IsActive,
                Tel = user.Tel,
                Email = user.Email,
                Nom = user.Nom,
                Prenom = user.Prenom
            };
            return Accepted(responseDto);



        }
        /// <summary>
        /// Deletes a user with the specified unique identifier.
        /// </summary>
        /// <param name="id">The GUID of the user to delete.</param>
        /// 
        /// <response code="200">The user was successfully deleted.</response>
        /// <response code="404">The user with the specified ID was not found.</response>
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteUser([FromRoute]Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null) return NotFound();
            await _userManager.DeleteAsync(user);
            return Ok();

        }
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateUser([FromRoute] Guid id, [FromBody] UserDTO userDTO)
        {
            var userToUpdate=await _userManager.FindByIdAsync(id.ToString());
            if (userToUpdate == null) return NotFound();
            //
            if (!string.Equals(userToUpdate.Nom, userDTO.Nom))
            {
                userToUpdate.Nom = userDTO.Nom;
            }

            if (!string.Equals(userToUpdate.Prenom, userDTO.Prenom))
            {
                userToUpdate.Prenom = userDTO.Prenom;
            }

            if (!string.Equals(userToUpdate.Email, userDTO.Email))
            {
                userToUpdate.Email = userDTO.Email;
                userToUpdate.UserName = userDTO.Email;
            }

            if (!string.Equals(userToUpdate.Tel, userDTO.Tel))
            {
                userToUpdate.Tel = userDTO.Tel;
            }

            if (!string.Equals(userToUpdate.IsActive, userDTO.IsActive))
            {
                userToUpdate.IsActive = userDTO.IsActive;
            }

            if (!string.Equals(userToUpdate.Tel, userDTO.Tel))
            {
                userToUpdate.Tel = userDTO.Tel;
            }

            userToUpdate.LastUpdatedAt = DateTime.Now;
            
            await _userManager.UpdateAsync(userToUpdate);
            return Ok(userToUpdate);

            
        }
        
        
    }
}
