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
        public async Task<IActionResult> AddUser([FromForm] UserAddDTO userData)
        {
            var result = await _userService.AddUser(userData);
            if (!result.Data)
            {
                return BadRequest(result.Message);
            }
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetUserPhrase(string username)
        {
            var result = await _userService.GetUserPhrase(username);
            if (result.Data is null)
            {
                return BadRequest(result.Message);
            }
            return Ok(result.Data);
        }
    }
}
