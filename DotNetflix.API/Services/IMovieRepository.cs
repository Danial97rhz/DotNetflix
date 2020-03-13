using DotNetflix.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetflix.API.Services
{
    public interface IMovieRepository
    {
        IQueryable<Movies> GetMovies(string title);
        IQueryable<Movies> GetAdultMovies(bool isAdult);
        Movies GetMovie(string movieId, bool includeDetails = true);
        Task<MoviesDetails> GetMovieDetails(string movieId);
        IQueryable<Movies> GetMoviesByGenre(int genreId);

        Task<bool> SaveChangesAsync();
        void Add<T>(T entity) where T : class;

        IQueryable<CarouselDatas> GetCarouselData();
    }
}
