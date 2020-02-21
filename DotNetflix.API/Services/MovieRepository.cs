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

        /* Get all movies containing search term either as title or as production year.
        If search term is left empty all movies are returned*/
        public IEnumerable<Movie> GetMovies(string searchTerm)
        {
            if (!string.IsNullOrEmpty(searchTerm))
            {
                searchTerm = searchTerm.ToLower();
            }
            return context.Movies
                .Where(m => string.IsNullOrEmpty(searchTerm) || m.Title.ToLower().Contains(searchTerm)
                || m.Year.ToString().Equals(searchTerm))
                .OrderBy(m => m.Title)
                .Select(m => m);

        }
    }
}
