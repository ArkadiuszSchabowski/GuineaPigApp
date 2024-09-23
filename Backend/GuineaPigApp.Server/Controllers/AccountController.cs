using AutoMapper;
using GuineaPigApp.Server.Interfaces;
using GuineaPigApp.Server.Models;
using Microsoft.AspNetCore.Mvc;

namespace GuineaPigApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        [HttpPost("register")]
        public ActionResult Register([FromBody] RegisterUserDto registerUserDto)
        {
            _accountService.RegisterUser(registerUserDto);

            return Ok(new { message = "Pomyślnie zarejestrowano użytkownika!" });
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody] LoginUserDto loginUserDto)
        {
            string token = _accountService.GenerateJWT(loginUserDto);

            return Ok(new { message = token });
        }
        [HttpPost("change-password")]
        public ActionResult ChangePassword([FromBody] ChangePasswordDto dto)
        {
            _accountService.ChangePassword(dto);
            return Ok(new { message = "Twoje hasło zostało zmienione!" });
        }
        [HttpDelete]
        public ActionResult DeleteAccount([FromQuery] string email)
        {
            _accountService.DeleteAccount(email);
            return Ok();
        }
    }
}
