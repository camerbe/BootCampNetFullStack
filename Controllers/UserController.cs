using BootCampDAL.Data.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BootCampNetFullStack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
                return Ok(user);
            }
            catch (Exception ex) { 
                return StatusCode(500, ex.Message);
            }
        }
    }
}
