using AutoMapper;
using GuineaPigApp.Server.Database.Entities;
using GuineaPigApp.Server.Exceptions;
using GuineaPigApp.Server.Interfaces;
using GuineaPigApp.Server.Models;

namespace GuineaPigApp.Server.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public GetUserDto GetUser(string email)
        {
            User? user = _userRepository.GetUser(email);

            if (user == null)
            {
                throw new BadRequestException("Nie znaleziono użytkownika o podanym adresie e-mail!");
            }

            var userDto = _mapper.Map<GetUserDto>(user);

            return userDto;
        }

        public void UpdateUser(string email, UpdateUserDto dto)
        {
            User? user = _userRepository.GetUser(email);

            if (user == null)
            {
                throw new BadRequestException("Nie znaleziono użytkownika o podanym adresie e-mail!");
            }

            _mapper.Map(dto, user);

            _userRepository.SaveChanges();
        }
    }
}
