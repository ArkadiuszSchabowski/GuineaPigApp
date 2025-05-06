using AutoMapper;
using GuineaPigApp.Server.Database.Entities;
using GuineaPigApp.Server.Interfaces;
using GuineaPigApp.Server.Models;

namespace GuineaPigApp.Server.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IEmailValidator _emailValidator;
        private readonly IUserValidator _userValidator;

        public UserService(IUserRepository userRepository, IMapper mapper, IUserValidator userValidator, IEmailValidator emailValidator)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _emailValidator = emailValidator;
            _userValidator = userValidator;
        }
        public GetUserDto GetUser(string email)
        {
            _emailValidator.ValidateEmailFormat(email);

            User? user = _userRepository.GetUser(email);

            _userValidator.ThrowIfUserIsNull(user);

            var userDto = _mapper.Map<GetUserDto>(user);

            return userDto;
        }

        public List<GetUserDto> GetUsers()
        {
            List<User> users = _userRepository.GetUsers();

            var usersDto = _mapper.Map<List<GetUserDto>>(users);

            return usersDto;
        }

        public void UpdateUser(string email, UpdateUserDto dto)
        {
            _emailValidator.ValidateEmailFormat(email);
            
            User? user = _userRepository.GetUser(email);

            _userValidator.ThrowIfUserIsNull(user);

            _mapper.Map(dto, user);

            _userRepository.SaveChanges();
        }
    }
}
