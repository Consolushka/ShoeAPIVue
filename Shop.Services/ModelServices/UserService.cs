using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Shop.Data.Models;
using Shop.Data.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Shop.Repositories.Contracts;
using Shop.Services.Contracts;

namespace Shop.Services.ModelServices
{
    internal class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IBasketRepository basketRepository,IConfiguration configuration, IMapper mapper)
        {
            _userRepository = userRepository;
            _basketRepository = basketRepository;
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
            
            var token = generateJwtToken(matched);

            return new UserResponse(matched, token);
        }

        public async Task<User> Register(UserVM userVm)
        {
            userVm.Password = Encryption.Encode(userVm.Password);
            var user = _mapper.Map<User>(userVm);
            var thisUser = await _userRepository.GetByUsernameOrEmail(user);
            if (thisUser != null)
            {
                throw new Exception("Same User with email or username already exists");
            }
            user.IsActive = false;
            user.IsAdmin = false;
            user.ConfirmString = Guid.NewGuid();
            var addedUser = await _userRepository.Add(user);
            await _basketRepository.Add(new Basket()
            {
                UserId = addedUser.Id
            });

            return addedUser;
        }

        public async Task ConfirmUser(Guid key)
        {
            var user = await _userRepository.GetByKey(key);
            if (user == null)
            {
                throw new Exception("Cannot find user with this Confirm Key");
            }
            
            await _userRepository.ConfirmUser(user);
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
            user.IsActive = false;
            user.ConfirmString = Guid.NewGuid();
            // user.Password = _configuration.Encode(user.Password);
            return await _userRepository.Update(user);
        }

        public async Task ChangePassword(string password, long id)
        {
            var user = await _userRepository.GetById(id);
            user.Password = Encryption.Encode(password);
            user.IsActive = false;
            user.ConfirmString = Guid.NewGuid();
            await _userRepository.Update(user);
        }

        public async Task<User> AbortUpdation(Guid key)
        {
            var user = await _userRepository.GetByKey(key);
            user.ConfirmString = Guid.NewGuid();
            user.IsActive = false;
            return await _userRepository.Update(user);
        }
        
        private string generateJwtToken(User user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Secret"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}