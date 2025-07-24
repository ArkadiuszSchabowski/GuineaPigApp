using AutoMapper;
using GuineaPigApp.Server.Database.Entities;
using GuineaPigApp.Server.Interfaces;
using GuineaPigApp_Server.Models.Add;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GuineaPigApp.Server.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserValidator _userValidator;
        private readonly ILoginValidator _loginValidator;
        private readonly IPasswordHasher<User> _hasher;
        private readonly IMapper _mapper;
        private readonly AuthenticationSettings _authenticationSettings;

        public AccountService(IUserRepository userRepository, IUserValidator userValidator, ILoginValidator loginValidator, IPasswordHasher<User> hasher, IMapper mapper, AuthenticationSettings authenticationSettings)
        {
            _userRepository = userRepository;
            _userValidator = userValidator;
            _loginValidator = loginValidator;
            _hasher = hasher;
            _mapper = mapper;
            _authenticationSettings = authenticationSettings;
        }

        public void ChangePassword(ChangePasswordDto dto)
        {
            var user = _userRepository.GetUser(dto.Email);

            _userValidator.ThrowIfUserIsNull(user);

            var result = _hasher.VerifyHashedPassword(user!, user!.PasswordHash, dto.CurrentPassword);

            _loginValidator.ThrowIfInvalidPassword(result);

            user.PasswordHash = _hasher.HashPassword(user, dto.NewPassword);

            _userRepository.UpdateUser(user);
        }

        public string GenerateJWT(LoginUserDto loginUserDto)
        {
            var user = _userRepository.GetUser(loginUserDto.Email);

            _loginValidator.ThrowIfInvalidLogin(user);

            var password = _hasher.VerifyHashedPassword(user!, user!.PasswordHash, loginUserDto.Password);

            _loginValidator.ThrowIfInvalidLogin(password);

            _userValidator.ThrowIfUserRoleIsNull(user.Role);

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role!.Name.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiresDays = DateTime.Now.AddDays(_authenticationSettings.ExpireDays);

            var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer, _authenticationSettings.JwtIssuer,
                claims,
                expires: expiresDays,
                signingCredentials: cred);

            var tokenHandler = new JwtSecurityTokenHandler();

            return tokenHandler.WriteToken(token);
        }

        public void RegisterUser(RegisterUserDto dto)
        {
            var user = _userRepository.GetUser(dto.Email);

            _userValidator.ThrowIfUserExist(user);

            var registerUser = _mapper.Map<User>(dto);

            registerUser.PasswordHash = _hasher.HashPassword(registerUser, dto.Password);
            registerUser.RoleId = 1;

            _userRepository.AddUser(registerUser);
        }

        public void RemoveAccount(string email)
        {
            var user = _userRepository.GetUser(email);

            _userValidator.ThrowIfUserIsNull(user);

            _userValidator.ThrowIfRemovingDefaultAccount(user!.Email);

            _userRepository.RemoveUser(user);
        }

        public void ValidateRegisterStepOne(RegisterStepOneDto dto)
        {
            var user = _userRepository.GetUser(dto.Email);

            _userValidator.ThrowIfUserExist(user);
        }
    }
}

