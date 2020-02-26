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
        public IEnumerable<Movie> GetMovies(string title)
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
                .ToList();

            return movies;

            //var movies = context.Movies
            //    .Where(m =>
            //        string.IsNullOrEmpty(title)
            //        || m.Title.ToLower().Contains(title)
            //        || m.Year.ToString().Equals(title))
            //    .Include(m => m.MovieGenres)
            //        .ThenInclude(mg => mg)
            //    .Select(m => new MovieDto
            //    {
            //        Id = m.MovieId,
            //        Title = m.Title,
            //        Year = m.Year,
            //        Genres = m.MovieGenres.Select(g => g.Genre.Name).ToList()
            //    })
            //    .OrderBy(m => m.Title)
            //    .ToList();
        }

        // Map Movies entity to Movie model
        //public static List<Movie> Map(List<Movies> movies)
        //{
        //    // Map movies from db to api outfacing movie type
        //    var moviesDto = movies.Select(m => new
        //    {
        //        Id = m.MovieId,
        //        Rating = m.AvgRating,
        //        NumberOfVotes = m.NumberOfVotes,
        //        Title = m.Title,
        //        OriginalTitle = m.OriginalTitle,
        //        Year = m.Year,
        //        RunTime = m.RunTimeMinutes,
        //        IsAdult = m.IsAdult,
        //        Genres = m.MovieGenres.Select(g => g.Genre.Name).ToList()
        //    });

        //    /* Missing properties
        //    LongPlot = m.plot
        //    PosterUrl = m.poster
        //    Director = m.dire
        //    ReleaseDate = m.rele*/
        //    //}
        //}
    }
}

