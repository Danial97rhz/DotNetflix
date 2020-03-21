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
        public static IQueryable<Movie> ToMovie(IQueryable<Movies> moviesIn)
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
                LongPlot = m.Details.Plot,
                PosterUrl = m.Details.Poster,
                Director = m.Details.Director,
                ReleaseDate = m.Details.Released,
                Country = m.Details.Country
            });

            return moviesOut;
        }
        public static Movie ToMovieFromObject(Movies moviesIn)
        {
            // Include genres and movie details
            Movie moviesOut = new Movie();

            moviesOut.Id = moviesIn.MovieId;
                //Rating = float.Parse(m.AvgRating),    // String not in correct format
                moviesOut.NumberOfVotes = moviesIn.NumberOfVotes;
                moviesOut.Title = moviesIn.Title;
                moviesOut.OriginalTitle = moviesIn.OriginalTitle;
                moviesOut.Year = moviesIn.Year;
                moviesOut.RunTime = moviesIn.RunTimeMinutes;
                moviesOut.IsAdult = moviesIn.IsAdult;
            if (moviesIn.Details != null)
            {
                moviesOut.Genres = moviesIn.MovieGenres.Select(mg => mg.Genre.Name);
                if (moviesIn.Details.Actors != null)        moviesOut.Actors = moviesIn.Details.Actors.Split(',').ToList();
                moviesOut.LongPlot = moviesIn.Details.Plot;
                moviesOut.PosterUrl = moviesIn.Details.Poster;
                moviesOut.Director = moviesIn.Details.Director;
                moviesOut.ReleaseDate = moviesIn.Details.Released;
                moviesOut.Country = moviesIn.Details.Country;
            }
            return moviesOut;
        }
        public static Movie ToMovieFromObject(Movies moviesIn, MoviesDetails moviesDetails)
        {
            // Include genres and movie details
            Movie moviesOut =
            new Movie
            {
                Id = moviesIn.MovieId,
                //Rating = float.Parse(m.AvgRating),    // String not in correct format
                NumberOfVotes = moviesIn.NumberOfVotes,
                Title = moviesIn.Title,
                OriginalTitle = moviesIn.OriginalTitle,
                Year = moviesIn.Year,
                RunTime = moviesIn.RunTimeMinutes,
                IsAdult = moviesIn.IsAdult,
                // Get Generes
                Genres = moviesIn.MovieGenres.Select(mg => mg.Genre.Name),
                // Get data from MoviesDetails
                Actors = moviesDetails.Actors.Split(',').ToList(),
                LongPlot = moviesDetails.Plot,
                PosterUrl = moviesDetails.Poster,
                Director = moviesDetails.Director,
                ReleaseDate = moviesDetails.Released,
                Country = moviesDetails.Country
            };           
            return moviesOut;
        }

        public static List<WishList> ToWishlistFromObjectList(IEnumerable<WishlistMovies> wishlistEntity)
        {
            var wishlistList = new List<WishList>();

            foreach (var entity in wishlistEntity)
            {

            WishList wishlistModel =
            new WishList
            {
                Id = entity.Id,
                UserId = entity.UserId,
                DateAdded = entity.DateAdded,
                MovieId = entity.MovieId,
                Title = entity.Movie.Title,
                Year = entity.Movie.Year,
                Rating = entity.Movie.AvgRating
            };
                wishlistList.Add(wishlistModel);
            }

            return wishlistList;
        }
    }
}
