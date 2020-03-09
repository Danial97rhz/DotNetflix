using DotNetflix.API.Context;
using DotNetflix.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetflix.API.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly DotNetflixDbContext _context;

        public UserRepository(DotNetflixDbContext context)
        {
            _context = context;
        }

        public List<WishlistMovies> GetUserWishList(int userId)
        {
            var wishlist = _context.Wishlist
                .Where(x => x.UserId == userId).ToList();

            return wishlist;
        }
    }
}
