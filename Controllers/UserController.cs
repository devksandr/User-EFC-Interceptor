using Microsoft.AspNetCore.Mvc;
using User_EFC_Interceptor.Models.DTO;
using User_EFC_Interceptor.Services;

namespace User_EFC_Interceptor.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        public readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public IActionResult AddUser([FromForm] UserAddDTO userData)
        {
            var result = _userService.AddUser(userData);
            if (!result.Data)
            {
                return BadRequest(result.Message);
            }
            return Ok();
        }

        [HttpGet]
        public IActionResult GetUserPhrase(string username)
        {
            var result = _userService.GetUserPhrase(username);
            if (result.Data is null)
            {
                return BadRequest(result.Message);
            }
            return Ok(result.Data);
        }
    }
}
