using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using WebApplication.Data.Models;
using WebApplication.Data.ViewModels;
using WebApplication.Middleware;
using WebApplication.Repository.Contracts;
using WebApplication.Services.Contracts;

namespace WebApplication.Services.ModelServices
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
            var users = await _userRepository.GetAll(); 
            var user = users.FirstOrDefault(
                    x =>
                        (x.Email == vm.Email || x.UserName == vm.UserName)
                        &&
                        _configuration.Decode(x.Password) == vm.Password
                        &&
                        x.IsActive);

            if (user == null)
            {
                return null;
            }

            var token = _configuration.GenerateJwtToken(user);

            return new UserResponse(user, token);
        }

        public async Task<User> Register(UserVM userVm)
        {
            userVm.Password = _configuration.Encode(userVm.Password);
            var user = _mapper.Map<User>(userVm);
            user.IsActive = false;
            user.IsAdmin = false;
            user.ConfirmString = Guid.NewGuid();
            var users = await _userRepository.GetAll();
            var thisUser = users.FirstOrDefault(u =>
                u.Email == user.Email
                ||
                u.UserName == user.UserName);
            if (thisUser != null)
            {
                return null;
            }
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
            user.Password = _configuration.Encode(user.Password);
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