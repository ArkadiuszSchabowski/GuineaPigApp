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

            var guineaPigWeight = new GuineaPigWeight();

            guineaPigWeight.Weight = newGuineaPig.Weight;
            guineaPigWeight.GuineaPig = newGuineaPig;
            guineaPigWeight.Date = DateOnly.FromDateTime(DateTime.Now);

            _guineaPigRepository.AddGuineaPigWeight(guineaPigWeight);
        }

        public GuineaPigDto GetGuineaPig(string email, string name)
        {
            User? user = _userRepository.GetUser(email);

            if (user == null)
            {
                throw new BadRequestException("Taki użytkownik nie istnieje!");
            }

            GuineaPig? guineaPig = _guineaPigRepository.GetGuineaPig(user.Id, name);

            if (guineaPig == null)
            {
                throw new BadRequestException("Nie posiadasz świnki morskiej o takim imieniu!");
            }

            var guineaPigDto = _mapper.Map<GuineaPigDto>(guineaPig);

            return guineaPigDto;
        }

        public List<GuineaPigDto> GetGuineaPigs(string email)
        {
            User? user = _userRepository.GetUser(email);

            if (user == null)
            {
                throw new BadRequestException("Taki użytkownik nie istnieje!");
            }

            List<GuineaPig> guineaPigs = _guineaPigRepository.GetGuineaPigs(user.Id);

            var guineaPigsDto = _mapper.Map<List<GuineaPigDto>>(guineaPigs);

            return guineaPigsDto;
        }

        public List<GuineaPigWeightsDto> GetWeights(string email, string guineaPigName)
        {
            User? user = _userRepository.GetUser(email);

            if (user == null)
            {
                throw new BadRequestException("Taki użytkownik nie istnieje!");
            }

            var guineaPig = _guineaPigRepository.GetGuineaPig(user.Id, guineaPigName);

            if (guineaPig == null)
            {
                throw new BadRequestException("Nie posiadasz świnki morskiej o takim imieniu!");
            }

            List<GuineaPigWeight> weights = _guineaPigRepository.GetWeights(guineaPig);
            var weightsDto = _mapper.Map<List<GuineaPigWeightsDto>>(weights);
            return weightsDto;
        }

        public void RemoveGuineaPig(string email, string guineaPigName)
        {
            User? user = _userRepository.GetUser(email);

            if (user == null)
            {
                throw new BadRequestException("Taki użytkownik nie istnieje!");
            }

            var guineaPig = _guineaPigRepository.GetGuineaPig(user.Id, guineaPigName);

            if (guineaPig == null)
            {
                throw new BadRequestException("Nie posiadasz świnki morskiej o takim imieniu!");
            }

            _guineaPigRepository.RemoveGuineaPig(guineaPig);
        }

        public void UpdateWeight(string email, string guineaPigName, GuineaPigWeightDto dto)
        {
            User? user = _userRepository.GetUser(email);

            if (user == null)
            {
                throw new BadRequestException("Taki użytkownik nie istnieje!");
            }

            GuineaPig? guineaPig = _guineaPigRepository.GetGuineaPig(user.Id, guineaPigName);

            if (guineaPig == null)
            {
                throw new BadRequestException("Nie posiadasz świnki morskiej o takim imieniu!");
            }

            _mapper.Map(dto, guineaPig);

            var guineaPigWeight = new GuineaPigWeight();

            guineaPigWeight.Weight = guineaPig.Weight;
            guineaPigWeight.GuineaPig = guineaPig;
            guineaPigWeight.Date = DateOnly.FromDateTime(DateTime.Now);

            _guineaPigRepository.AddGuineaPigWeight(guineaPigWeight);

            _userRepository.SaveChanges();
        }
    }
}
