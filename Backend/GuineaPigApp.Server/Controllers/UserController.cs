using GuineaPigApp.Server.Interfaces;
using GuineaPigApp_Server.Models.Add;
using GuineaPigApp_Server.Models.Get;
using Microsoft.AspNetCore.Mvc;

namespace GuineaPigApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        public GetUserDto GetUser([FromQuery] string email)
        {
            GetUserDto user = _userService.GetUser(email);
            return user;
        }
        [HttpGet("users")]
        public List<GetUserDto> GetUsers()
        {
            var users = _userService.GetUsers();
            return users;
        }
        [HttpPatch]
        public ActionResult UpdateUser([FromQuery] string email, [FromBody] UpdateUserDto dto)
        {
            _userService.UpdateUser(email, dto);
            return Ok();
        }
    }
}
