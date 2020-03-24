using DotNetflix.API.Context;
using DotNetflix.API.Entities;
using Microsoft.EntityFrameworkCore;
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

        public List<WishlistMovies> GetWishList(int userId)
        {
            var wishlist = _context.Wishlist
                .Where(x => x.UserId == userId)
                .Include(m => m.Movie)
                .ToList();

            return wishlist;
        }
        public void AddToWishlist(WishlistMovies wishlistMovie)
        {
            _context.Wishlist.Add(wishlistMovie);
        }

        public void DeleteWishlistMovie(WishlistMovies wishlistMovie)
        {
            _context.Remove(wishlistMovie);
        }

        public WishlistMovies GetWishlistMovie(int id)
        {
            return  _context.Wishlist.Where(x => x.Id == id).Include(m => m.Movie).FirstOrDefault();
                
        }

        public async Task<bool> Save()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
        public List<RatedMovies> GetRecentReviews()
        {
            return _context.RatedMovies
                .Include(x => x.Movie)
                .OrderByDescending(y => y.RatingId)
                .Take(5).ToList();
        }
             
        public List<RatedMovies> GetRatedMovieList(int userId)
        {
            var ratedList = _context.RatedMovies
                .Where(x => x.UserId == userId)
                .Include(m => m.Movie)
                .ToList();

            return ratedList;
        }

        public void AddRatedMovie(RatedMovies ratedMovie)
        {
            _context.RatedMovies.Add(ratedMovie);
        }

        public void DeleteRatedMovie(RatedMovies ratedMovie)
        {
            _context.Remove(ratedMovie);
        }

        public RatedMovies GetRatedMovie(int id)
        {
            return _context.RatedMovies.Where(x => x.RatingId == id).Include(m => m.Movie).FirstOrDefault();
        }

        public void UpdateRatedMovie(RatedMovies ratedMovie)
        {
            //_context.RatedMovies.Update(ratedMovie);
            _context.Entry(ratedMovie).State = EntityState.Modified;
        }
    }
}
