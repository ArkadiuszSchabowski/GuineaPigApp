using AutoMapper;
using GuineaPigApp.Server.Database.Entities;
using GuineaPigApp.Server.Exceptions;
using GuineaPigApp.Server.Interfaces;
using GuineaPigApp.Server.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GuineaPigApp.Server.Services
{
    public class AccountService : IAccountService
    {
        private readonly IPasswordHasher<User> _hasher;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly AuthenticationSettings _authenticationSettings;

        public AccountService(IPasswordHasher<User> hasher, IMapper mapper, AuthenticationSettings authenticationSettings, IUserRepository userRepository)
        {
            _hasher = hasher;
            _mapper = mapper;
            _authenticationSettings = authenticationSettings;
            _userRepository = userRepository;
        }

        public void ChangePassword(ChangePasswordDto dto)
        {
            var user = _userRepository.GetUser(dto.Email);

            if (user == null)
            {
                throw new NotFoundException("Taki użytkownik nie istnieje!");
            }

            var result = _hasher.VerifyHashedPassword(user, user.PasswordHash, dto.CurrentPassword);

            if (result == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Wprowadzono niepoprawne hasło!");
            }

            if (dto.NewPassword != dto.RepeatNewPassword)
            {
                throw new BadRequestException("Wprowadzone hasła nie są zgodne!");
            }

            user.PasswordHash = _hasher.HashPassword(user, dto.NewPassword);

            _userRepository.UpdateUser(user);
        }

        public string GenerateJWT(LoginUserDto loginUserDto)
        {
            var user = _userRepository.GetUser(loginUserDto.Email);

            if (user == null)
            {
                throw new BadRequestException("Błędne dane logowania");
            }

            var password = _hasher.VerifyHashedPassword(user, user.PasswordHash, loginUserDto.Password);

            if (password != PasswordVerificationResult.Success)
            {
                throw new BadRequestException("Błędne dane logowania");
            }

            if(user.Role == null) {
                throw new Exception("Użytkownik nie ma przypisanej roli!");
            }
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.Name.ToString())
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

            if (user != null)
            {
                throw new ConflictException("Taki użytkownik istnieje już w bazie danych");
            }

            if (dto.Password != dto.RepeatPassword)
            {
                throw new ConflictException("Wprowadzone hasła są róźne");
            }

            var newUser = _mapper.Map<User>(dto);

            newUser.PasswordHash = _hasher.HashPassword(newUser, dto.Password);
            newUser.RoleId = 1;

            _userRepository.AddUser(newUser);
        }

        public void DeleteAccount(string email)
        {
            var user = _userRepository.GetUser(email);

            if (user == null)
            {
                throw new BadRequestException("Taki użytkownik nie istnieje!");
            }
                _userRepository.RemoveUser(user);
        }
    }
}

