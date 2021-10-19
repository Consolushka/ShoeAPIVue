using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Entities.Models;
using Entities.Support;
using Microsoft.Extensions.Configuration;
using Middleware;
using Repository;
using Repository.EntityRepository;

namespace Core
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
                .FirstOrDefault(x => x.Email == model.Email && x.Password == model.Password);

            if (user == null)
            {
                // todo: need to add logger
                return null;
            }

            var token = _configuration.GenerateJwtToken(user);

            return new AuthenticateResponse(user, token);
        }

        public async Task<AuthenticateResponse> Register(UserModel userModel)
        {
            var user = _mapper.Map<User>(userModel);

            var addedUser = _userRepository.Add(user);

            var response = Authenticate(new AuthenticateRequest
            {
                Email = user.Email,
                Password = user.Password
            });
            
            return response;
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