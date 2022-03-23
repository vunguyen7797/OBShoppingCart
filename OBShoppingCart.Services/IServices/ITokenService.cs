using OBShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OBShoppingCart.Services.IServices
{
    public interface ITokenService
    {
        string CreateToken(ApplicationUser user);
    }
}
