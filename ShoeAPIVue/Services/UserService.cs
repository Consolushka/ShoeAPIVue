using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using ShoeAPIVue.Entities;
using ShoeAPIVue.Models;

namespace ShoeAPIVue.Services
{
    public class UserService: IUserService
    {
        private readonly IEfRepository<User> _userRepository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public UserService(IEfRepository<User> user, IConfiguration configuration, IMapper mapper)
        {
            _userRepository = user;
            _configuration = configuration;
            _mapper = mapper;
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest request)
        {
            var user = _userRepository.GetAll()
                .FirstOrDefault(u => u.Email == request.Email && u.Password == request.Password);

            if (user == null)
            {
                return null;
            }

            // var token = _configuration.GenerateJwtToken(user);

            return new AuthenticateResponse(user,"asdasd"/*, token*/);
        }

        public async Task<AuthenticateResponse> Register(UserModel userModel)
        {
            var user = _mapper.Map<User>(userModel);

            var addedUser = await _userRepository.Add(user);
            
            var response = Authenticate(new AuthenticateRequest
            {
                Email = user.Email,
                Password = user.Password
            });

            return response;
        }

        public User GetById(long id)
        {
            return _userRepository.GetById(id);
        }
    }
}