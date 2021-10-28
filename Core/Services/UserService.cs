using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Entities.Models;
using Entities.Support;
using Microsoft.Extensions.Configuration;
using Repository.Contracts;
using Core.Contracts;

namespace Core.Services
{
    public class UserService: IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IConfiguration configuration, IMapper mapper)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _mapper = mapper;
        }
        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            var user = _userRepository
                .GetAll()
                .FirstOrDefault(x => x.Email == model.Email && _configuration.Decode(x.Password) == model.Password);

            if (user == null)
            {
                // todo: need to add logger
                return null;
            }

            var token = _configuration.GenerateJwtToken(user);

            return new AuthenticateResponse(user, token);
        }

        public async Task<bool> Register(UserModel userModel)
        {
            userModel.Password = _configuration.Encode(userModel.Password);
            userModel.RoleId = 1;
            var user = _mapper.Map<User>(userModel);

            var addedUser = await _userRepository.Add(user);

            if (addedUser == 0)
            {
                return false;
            }
            
            return true;
        }
        public List<User> GetAll()
        {
            return _userRepository.GetAll();
        }

        public User GetById(long Id)
        {
            return _userRepository.GetById(Id);
        }
    }
}