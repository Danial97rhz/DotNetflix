using DotNetflix.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetflix.API.Services
{
    public interface IUserRepository
    {
        List<WishlistMovies> GetUserWishList(int userId);
    }
}
