using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Shop.Data.Models;
using Shop.Data.ViewModels;
using Microsoft.Extensions.Configuration;
using Shop.Repositories.Contracts;
using Shop.Services.Contracts;

namespace Shop.Services.ModelServices
{
    internal class UserService : IUserService
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

        public async Task<UserResponse> Authenticate(UserVM vm)
        {
            var user = _mapper.Map<User>(vm);
            user.Password = Encryption.Encode(vm.Password);

            var matched = await _userRepository.GetByUserNameOrEmailAndPassword(user);
            
            if (matched == null)
            {
                throw new Exception("Cannot find User with this login parameters, or your account is unconfirmed");
            }
            
            var token = _configuration.GenerateJwtToken(user);

            return new UserResponse(user, token);
        }

        public async Task<User> Register(UserVM userVm)
        {
            userVm.Password = Encryption.Encode(userVm.Password);
            var user = _mapper.Map<User>(userVm);
            var thisUser = _userRepository.GetByUsernameOrEmail(user);
            if (thisUser != null)
            {
                throw new Exception("Same User with email or username already exists");
            }
            user.IsActive = false;
            user.IsAdmin = false;
            user.ConfirmString = Guid.NewGuid();
            var addedUser = await _userRepository.Add(user);

            return addedUser;
        }

        public async Task ConfirmUser(Guid key)
        {
            var user = await _userRepository.GetByKey(key);
            if (user != null)
            {
                _userRepository.ConfirmUser(user);
            }

            throw new Exception("Cannot find user with this Confirm Key");
        }

        public async Task<List<User>> GetAll()
        {
            return await _userRepository.GetAll();
        }

        public User GetById(long Id)
        {
            return _userRepository.GetById(Id).Result;
        }

        public async Task<User> Update(UserVM userVm, long id)
        {
            var user = await _userRepository.GetById(id);
            user.FillFromVM(userVm);
            user.ConfirmString = Guid.NewGuid();
            // user.Password = _configuration.Encode(user.Password);
            return await _userRepository.Update(user);
        }

        public async Task<User> AbortUpdation(Guid key)
        {
            var user = await _userRepository.GetByKey(key);
            user.ConfirmString = Guid.NewGuid();
            user.IsActive = false;
            return await _userRepository.Update(user);
        }
    }
}