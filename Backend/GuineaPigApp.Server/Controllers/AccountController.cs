﻿using GuineaPigApp.Server.Interfaces;
using GuineaPigApp_Server.Models.Add;
using Microsoft.AspNetCore.Authorization;
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

        [HttpPost("register/step1/validate")]
        public ActionResult ValidateRegisterStepOne([FromBody] RegisterStepOneDto dto)
        {
            _accountService.ValidateRegisterStepOne(dto);
            return Ok();
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody] LoginUserDto loginUserDto)
        {
            string token = _accountService.GenerateJWT(loginUserDto);

            return Ok(new { message = token });
        }

        [Authorize]
        [HttpPost("change-password")]
        public ActionResult ChangePassword([FromBody] ChangePasswordDto dto)
        {
            _accountService.ChangePassword(dto);
            return Ok(new { message = "Twoje hasło zostało zmienione!" });
        }

        [Authorize]
        [HttpDelete]
        public ActionResult RemoveAccount([FromQuery] string email)
        {
            _accountService.RemoveAccount(email);
            return Ok();
        }
    }
}
