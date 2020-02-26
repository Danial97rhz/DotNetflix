using DotNetflix.API.Context;
using DotNetflix.API.Entities;
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
        public IEnumerable<MovieDto> GetMovies(string title)
        {
            if (!string.IsNullOrEmpty(title))
            {
                title = title.ToLower();
            }

            var movies = context.Movies
                // Filter on search term
                .Where(m =>
                    string.IsNullOrEmpty(title)
                    || m.Title.ToLower().Contains(title)
                    || m.Year.ToString().Equals(title))
                // Include genres and movie details
                .Include(m => m.MovieGenres)
                    .ThenInclude(mg => mg)
                .Include(m => m.Details)
                // Create new Movie object
                .Select(m => new Movie
                {
                    // Get data from Movies
                    Id = m.MovieId,
                    //Rating = float.Parse(m.AvgRating),    // String not in correct format
                    NumberOfVotes = m.NumberOfVotes,
                    Title = m.Title,
                    OriginalTitle = m.OriginalTitle,
                    Year = m.Year,
                    RunTime = m.RunTimeMinutes,
                    IsAdult = m.IsAdult,
                    // Get data from Genres
                    Genres = m.MovieGenres.Select(g => new Genre
                    {
                        Id = g.Genre.Id,
                        ImgPath = g.Genre.ImgPath,
                        Name = g.Genre.Name,
                    }),
                    Actors = new List<string> { m.Details.Actors },
                    // Get data from MoviesDetails
                    LongPlot = m.Details.LongPlot,
                    PosterUrl = m.Details.PosterUrl,
                    Director = m.Details.Director,
                    ReleaseDate = m.Details.ReleaseDate
                })
                .OrderBy(m => m.Title)
                .ToList();

            return movies;
        }      
    }
}
