using DotNetflix.API.Context;
using DotNetflix.API.Models;
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

        // Get all movies from the database
        public IEnumerable<Movie> GetMovies()
        {
            return context.Movies.ToList<Movie>();
        }
    }
}
