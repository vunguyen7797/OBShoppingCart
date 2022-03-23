using OBShoppingCart.DataAccess.Dtos;
using OBShoppingCart.DataAccess.Repositories.IRepositories;
using OBShoppingCart.Models;
using OBShoppingCart.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OBShoppingCart.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void CreateUser(ApplicationUser user) => _userRepository.CreateUser(user);

        public async Task<ApplicationUser> GetUserByUsernameAsync(string username) => await _userRepository.GetUserByUsernameAsync(username); 

        public async Task<bool> SaveChangesAsync() => await _userRepository.SaveChangesAsync();

        public async Task<bool> UserExists(string username) => await _userRepository.UserExists(username);
    }
}
