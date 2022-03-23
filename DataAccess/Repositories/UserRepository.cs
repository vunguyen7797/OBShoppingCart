using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OBShoppingCart.Data;
using OBShoppingCart.DataAccess.Dtos;
using OBShoppingCart.DataAccess.Repositories.IRepositories;
using OBShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OBShoppingCart.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _db;

        public UserRepository(DataContext db)
        {
            _db = db;
        }

        public void CreateUser(ApplicationUser user)
        {
            _db.Add(user);
        }

        public async Task<ApplicationUser> GetUserByUsernameAsync(string username)
        {
            return await _db.Users.SingleOrDefaultAsync(u => u.Username == username);
        }

        public async Task<bool> UserExists(string username)
        {
            return await _db.Users.AnyAsync(u => u.Username.ToLower() == username.ToLower());  
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _db.SaveChangesAsync() > 0;
        }
    }
}
