using OBShoppingCart.DataAccess.Dtos;
using OBShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OBShoppingCart.Services.IServices
{
    public interface IUserService
    {
        void CreateUser(ApplicationUser user);
        Task<ApplicationUser> GetUserByUsernameAsync(string username);
        Task<bool> UserExists(string username);
        Task<bool> SaveChangesAsync();
    }
}
