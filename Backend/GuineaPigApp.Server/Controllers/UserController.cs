using GuineaPigApp.Server.Interfaces;
using GuineaPigApp.Server.Models;
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
        [HttpPatch]
        public ActionResult UpdateUser([FromQuery] string email, [FromBody] UpdateUserDto dto)
        {
            _userService.UpdateUser(email, dto);
            return Ok();
        }
    }
}
