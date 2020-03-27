using DotNetflix.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetflix.API.Services
{
    public interface IUserRepository
    {
        List<WishlistMovies> GetWishList(int userId);
        void AddToWishlist(WishlistMovies wishlistMovie);
        void DeleteWishlistMovie(WishlistMovies wishlistMovie);
        List<int> GetUsersWithWishlist();
        WishlistMovies GetWishlistMovie(int id);
        List<RatedMovies> GetRecentReviews();
        List<RatedMovies> GetRatedMovieList(int userId);
        void AddRatedMovie(RatedMovies ratedMovie);
        void DeleteRatedMovie(RatedMovies ratedMovie);
        RatedMovies GetRatedMovie(int id);
        void UpdateRatedMovie(RatedMovies ratedMovie);

        Task<bool> Save();
    }
}
