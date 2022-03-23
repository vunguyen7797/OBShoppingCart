using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OBShoppingCart.DataAccess.Dtos;
using OBShoppingCart.Models;
using OBShoppingCart.Services.IServices;
using OBShoppingCart.Utility.LoggerService;
using System.Security.Cryptography;
using System.Text;

namespace OBShoppingCart.Controllers
{
    public class AccountsController : ApiBaseController
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;

        public AccountsController(IUserService userService, IMapper mapper, ILoggerManager logger, ITokenService tokenService)
        {
            _userService = userService;
            _mapper = mapper;
            _logger = logger;
            _tokenService = tokenService;
        }

        /// <summary>
        /// Register user account
        /// </summary>
        /// <param name="userRegisterDto">User register information</param>
        /// <returns>Username and authorized token</returns>
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(UserRegisterDto userRegisterDto)
        {
            if (await _userService.UserExists(userRegisterDto.Username)) return BadRequest("Username is taken.");

            using var hmac = new HMACSHA512();

            ApplicationUser user = new ApplicationUser
            {
                Username = userRegisterDto.Username.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(userRegisterDto.Password ?? String.Empty)),
                PasswordSalt = hmac.Key
            };

            _userService.CreateUser(user);

            await _userService.SaveChangesAsync();

            return new UserDto
            {
                Username = user.Username,
                Token = _tokenService.CreateToken(user)
            };
        }

        /// <summary>
        /// Login an user account
        /// </summary>
        /// <param name="userLoginDto">User login information</param>
        /// <returns>Username and authorized token</returns>
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(UserLoginDto userLoginDto)
        {
            var user = await _userService.GetUserByUsernameAsync(userLoginDto.Username);

            if (user == null) return Unauthorized("Invalid username");

            using var hmac = new HMACSHA512(user.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(userLoginDto.Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid password");
            }

            return new UserDto
            {
                Username = user.Username,
                Token = _tokenService.CreateToken(user)
            };
        }

    }
}
