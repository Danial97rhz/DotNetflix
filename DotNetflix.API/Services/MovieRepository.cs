using DotNetflix.API.Context;
using DotNetflix.API.Entities;
using DotNetflix.API.HelperMethods;
using DotNetflix.API.Models;
using DotNetflix.API.ModelsDto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetflix.API.Services
{
    public class MovieRepository : IMovieRepository
    {
        private readonly DotNetflixDbContext context;

        // Context provided via dependency injection. 
        public MovieRepository(DotNetflixDbContext context)
        {
            // Throw exception if context is null.
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /* Get all movies containing search term.
        If search term is left empty all movies are returned*/
        public IEnumerable<Movie> GetMovies(string title)
        {
            if (!string.IsNullOrEmpty(title))
            {
                title = title.ToLower();
            }

            // Filter movies on search term
            var movies = context.Movies
                .Where(m =>
                    string.IsNullOrEmpty(title)
                    || m.Title.ToLower().Contains(title)
                    || m.Year.ToString().Equals(title));
                //.Select(m => m);

            // Map to movie dto (data transfer object) and return
            return Map.ToMovieDto(movies)
                .OrderBy(m => m.Title)
                .ToList();
        }


        public Movie GetMovie(string movieId)
        {
            // Select movie on id
            var movie = context.Movies
                .Where(m => m.MovieId == movieId);

            // Map to movie dto (data transfer object) and return
            return Map.ToMovieDto(movie)
                .FirstOrDefault();
        }            
    }
}
