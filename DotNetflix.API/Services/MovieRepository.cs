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

        /* Get all movies containing search term.
        If search term is left empty all movies are returned*/
        public IEnumerable<Movie> GetMovies(string title)
        {
            if (!string.IsNullOrEmpty(title))
            {
                title = title.ToLower();
            }

            return context.Movies
                .Where(m => string.IsNullOrEmpty(title) || m.Title.ToLower().Contains(title))
                .OrderBy(m => m.Title)
                .Select(m => m);
        }
    }
}
