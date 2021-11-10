using System;
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
    public class UserService : IUserService
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
                .FirstOrDefault(
                    x =>
                        (x.Email == model.Login || x.UserName == model.Login)
                        &&
                        _configuration.Decode(x.Password) == model.Password
                        &&
                        x.IsConfirmed);

            if (user == null)
            {
                // todo: need to add logger
                return null;
            }

            var token = _configuration.GenerateJwtToken(user);

            return new AuthenticateResponse(user, token);
        }

        public async Task<User> Register(UserModel userModel)
        {
            var user = _mapper.Map<User>(userModel);
            user.ConfirmString = Guid.NewGuid();
            var addedUser = await _userRepository.Add(user);

            return addedUser;
        }

        public async Task<bool> ConfirmUser(Guid key)
        {
            var user = await _userRepository.GetByKey(key);
            if (user != null)
            {
                _userRepository.ConfirmUser(user);
                return true;
            }

            return false;
        }

        public List<User> GetAll()
        {
            return _userRepository.GetAll();
        }

        public User GetById(long Id)
        {
            return _userRepository.GetById(Id);
        }

        public async Task<User> Update(User user)
        {
            user.ConfirmString = Guid.NewGuid();
            user.Password = _configuration.Encode(user.Password);
            user.IsConfirmed = false;
            return await _userRepository.Update(user);
        }

        public async Task<User> AbortUpdation(Guid key)
        {
            var user = await _userRepository.GetByKey(key);
            user.ConfirmString = Guid.NewGuid();
            user.IsConfirmed = false;
            return await _userRepository.Update(user);
        }
    }
}