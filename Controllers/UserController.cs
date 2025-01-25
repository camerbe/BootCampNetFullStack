﻿using BootCampDAL.Data.DTO;
using BootCampDAL.Service;
using BootCampDAL.Data.Models;
using BootCampDAL.Data.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BootCampNetFullStack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;

        public UserController(IUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers() 
        {
            try
            {
                var users = _unitOfWork.User.GetAll();
                return Ok(users);
            }
            catch (Exception ex) { 
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetUser(Guid id) 
        {
            try
            {
                var user = await _unitOfWork.User.Get(a=>a.Id==id);
                return user != null ? Ok(user) : NotFound();
                
            }
            catch (Exception ex) { 
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserDTO usrdto)
        {
            if (!ModelState.IsValid) { 
                return BadRequest();
            }
            try
            {
                var user = new User
                {                  
                    
                    Nom=usrdto.Nom.ToUpper(),
                    Prenom = StringFormatter.ToTitleCase(usrdto.Prenom),
                     CreatedAt=usrdto.CreatedAt,
                    LastUpdatedAt=usrdto.LastUpdatedAt,
                    IsActive=usrdto.IsActive,
                    Tel=usrdto.Tel

                };
                user.Email = (usrdto.Email != null) ? usrdto.Email : usrdto.Prenom + usrdto.Nom;
                user.UserName = (usrdto.Email != null) ? usrdto.Email:usrdto.Prenom+usrdto.Nom ;
                var result = await _userManager.CreateAsync(user,usrdto.Password);
                if (!result.Succeeded) BadRequest();
                await _userManager.AddToRolesAsync(user,usrdto.Roles);
                return Accepted(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
                return Problem($"Problème survenu lors de l'enregistrement d'un User dans {nameof(CreateUser)}",statusCode:500);
            }
            
        }

        
    }
}
