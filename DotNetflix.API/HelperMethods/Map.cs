using DotNetflix.API.Entities;
using DotNetflix.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetflix.API.HelperMethods
{
    public class Map
    {
        public static IQueryable<Movie> ToMovieDto(IQueryable<Movies> moviesIn)
        {
            // Include genres and movie details
            var moviesOut =
            moviesIn.Include(m => m.MovieGenres)
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
                // Get Generes
                Genres = m.MovieGenres.Select(mg => mg.Genre.Name),
                // Get data from MoviesDetails
                Actors = new List<string> { m.Details.Actors },
                LongPlot = m.Details.LongPlot,
                PosterUrl = m.Details.PosterUrl,
                Director = m.Details.Director,
                ReleaseDate = m.Details.ReleaseDate
            });

            return moviesOut;
        }
    }
}
