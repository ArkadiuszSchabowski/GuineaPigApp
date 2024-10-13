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
        private readonly IUserValidator _userValidator;

        public UserService(IUserRepository userRepository, IMapper mapper, IUserValidator userValidator)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _userValidator = userValidator;
        }
        public GetUserDto GetUser(string email)
        {
            _userValidator.ValidateEmailFormat(email);

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
            _userValidator.ValidateEmailFormat(email);
            
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
