using AutoMapper;
using GuineaPigApp.Server.Database.Entities;
using GuineaPigApp.Server.Interfaces;
using GuineaPigApp_Server.Models.Add;
using GuineaPigApp_Server.Models.Get;

namespace GuineaPigApp.Server.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserValidator _userValidator;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IUserValidator userValidator, IMapper mapper)
        {
            _userRepository = userRepository;
            _userValidator = userValidator;
            _mapper = mapper;
        }
        public GetUserDto GetUser(string email)
        {
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
            User? user = _userRepository.GetUser(email);

            _userValidator.ThrowIfUserIsNull(user);

            _mapper.Map(dto, user);

            _userRepository.SaveChanges();
        }
    }
}
