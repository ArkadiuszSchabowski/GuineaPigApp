using AutoMapper;
using GuineaPigApp.Server.Database;
using GuineaPigApp.Server.Database.Entities;
using GuineaPigApp.Server.Exceptions;
using GuineaPigApp.Server.Interfaces;
using GuineaPigApp.Server.Models;

namespace GuineaPigApp.Server.Services
{
    public class GuineaPigService : IGuineaPigService
    {
        private readonly IGuineaPigRepository _guineaPigRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;


        public GuineaPigService(IGuineaPigRepository guineaPigRepository, IUserRepository userRepository, IMapper mapper)
        {
            _guineaPigRepository = guineaPigRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public void AddGuineaPig(string email, GuineaPigDto dto)
        {
            User? user = _userRepository.GetUser(email);

            if (user == null)
            {
                throw new BadRequestException("Taki użytkownik nie istnieje!");
            }

            if(dto.Weight < 50 || dto.Weight > 3000)
            {
                throw new BadRequestException("Waga świnki musi mieścić się w przedziale 50 do 3000gram!");
            }

            var guineaPig = _guineaPigRepository.PigExists(user, dto.Name);

            if (guineaPig)
            {
                throw new ConflictException("Posiadasz już taką świnkę morską o takim imieniu!");
            }

            var newGuineaPig = _mapper.Map<GuineaPig>(dto);

            newGuineaPig.User = user;

            _guineaPigRepository.AddGuineaPig(newGuineaPig);
        }

        public GuineaPigDto GetGuineaPig(string email, string name)
        {
            throw new NotImplementedException();
        }

        public List<GuineaPigDto> GetGuineaPigs(string email)
        {
            throw new NotImplementedException();
        }

        public void RemoveGuineaPig(string email, string name)
        {
            throw new NotImplementedException();
        }
    }
}
