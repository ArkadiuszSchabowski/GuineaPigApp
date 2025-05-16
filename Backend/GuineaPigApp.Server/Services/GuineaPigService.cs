using AutoMapper;
using GuineaPigApp.Server.Database.Entities;
using GuineaPigApp.Server.Interfaces;
using GuineaPigApp.Server.Models;

namespace GuineaPigApp.Server.Services
{
    public class GuineaPigService : IGuineaPigService
    {
        private readonly IGuineaPigRepository _guineaPigRepository;
        private readonly IGuineaPigValidator _guineaPigValidator;
        private readonly IUserRepository _userRepository;
        private readonly IUserValidator _userValidator;
        private readonly IMapper _mapper;


        public GuineaPigService(IGuineaPigRepository guineaPigRepository, IGuineaPigValidator guineaPigValidator, IUserRepository userRepository, IUserValidator userValidator, IMapper mapper)
        {
            _guineaPigRepository = guineaPigRepository;
            _guineaPigValidator = guineaPigValidator;
            _userRepository = userRepository;
            _userValidator = userValidator;
            _mapper = mapper;
        }
        public void Add(string email, GuineaPigDto dto)
        {
            User? user = _userRepository.GetUser(email);

            _userValidator.ThrowIfUserIsNull(user);

            _guineaPigValidator.ThrowIfWeightIsNotCorrect(dto.Weight);

            bool isGuineaPig = _guineaPigRepository.PigExists(user!, dto.Name);

            _guineaPigValidator.ThrowIfUserGuineaPigExists(isGuineaPig);

            GuineaPig guineaPig = _mapper.Map<GuineaPig>(dto);

            guineaPig.User = user!;

            _guineaPigRepository.Add(guineaPig);

            var guineaPigWeight = new GuineaPigWeight();

            guineaPigWeight.Weight = guineaPig.Weight;
            guineaPigWeight.GuineaPig = guineaPig;
            guineaPigWeight.Date = DateOnly.FromDateTime(DateTime.Now);

            _guineaPigRepository.AddGuineaPigWeight(guineaPigWeight);
        }

        public GuineaPigDto GetGuineaPig(string email, string name)
        {
            User? user = _userRepository.GetUser(email);

            _userValidator.ThrowIfUserIsNull(user);

            GuineaPig? guineaPig = _guineaPigRepository.GetGuineaPig(user!.Id, name);

            _guineaPigValidator.ThrowIfGuineaPigIsNull(guineaPig);

            var guineaPigDto = _mapper.Map<GuineaPigDto>(guineaPig);

            return guineaPigDto;
        }

        public List<GuineaPigDto> GetGuineaPigs(string email)
        {
            User? user = _userRepository.GetUser(email);

            _userValidator.ThrowIfUserIsNull(user);

            List<GuineaPig> guineaPigs = _guineaPigRepository.GetGuineaPigs(user!.Id);

            var guineaPigsDto = _mapper.Map<List<GuineaPigDto>>(guineaPigs);

            return guineaPigsDto;
        }

        public List<GuineaPigWeightsDto> GetWeights(string email, string guineaPigName)
        {
            User? user = _userRepository.GetUser(email);

            _userValidator.ThrowIfUserIsNull(user);

            var guineaPig = _guineaPigRepository.GetGuineaPig(user!.Id, guineaPigName);

            _guineaPigValidator.ThrowIfGuineaPigIsNull(guineaPig);

            List<GuineaPigWeight> weights = _guineaPigRepository.GetWeights(guineaPig!);

            var weightsDto = _mapper.Map<List<GuineaPigWeightsDto>>(weights);

            return weightsDto;
        }

        public void RemoveGuineaPig(string email, string guineaPigName)
        {
            User? user = _userRepository.GetUser(email);

            _userValidator.ThrowIfUserIsNull(user);

            var guineaPig = _guineaPigRepository.GetGuineaPig(user!.Id, guineaPigName);

            _guineaPigValidator.ThrowIfGuineaPigIsNull(guineaPig);

            _guineaPigRepository.RemoveGuineaPig(guineaPig!);
        }

        public void UpdateWeight(string email, string guineaPigName, GuineaPigWeightDto dto)
        {
            User? user = _userRepository.GetUser(email);

            _userValidator.ThrowIfUserIsNull(user);

            GuineaPig? guineaPig = _guineaPigRepository.GetGuineaPig(user!.Id, guineaPigName);

            _guineaPigValidator.ThrowIfGuineaPigIsNull(guineaPig);

            _mapper.Map(dto, guineaPig);

            var guineaPigWeight = new GuineaPigWeight();

            guineaPigWeight.Weight = guineaPig!.Weight;
            guineaPigWeight.GuineaPig = guineaPig;
            guineaPigWeight.Date = DateOnly.FromDateTime(DateTime.Now);

            _guineaPigRepository.AddGuineaPigWeight(guineaPigWeight);

            _userRepository.SaveChanges();
        }
    }
}
