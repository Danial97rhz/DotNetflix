using DotNetflix.API.Context;
using DotNetflix.API.Entities;
using DotNetflix.API.HelperMethods;
using DotNetflix.API.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

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
        public async Task<MoviesDetails> AttachDetailsToMovie(string movieId)
        {
            var movieEntity = GetMovie(movieId);
            var detailsEntity = await GetMovieDetails(movieId);
            detailsEntity.Movie = movieEntity;
            Add(detailsEntity);
            await SaveChangesAsync();
            return detailsEntity;
        }

        public IQueryable<Movies> GetMovies(string title)
        {
            if (!string.IsNullOrEmpty(title))
            {
                title = title.ToLower();
            }
            else
            {
                title = "";
            }

            // Filter movies on search term - AsNoTracking speeds up search but should only be used for readonly queries.

            //var movies = context.Movies
            //    .Where(m =>
            //        m.Title.ToLower().Contains(title)
            //        || m.Year.ToString().Equals(title))
            //    .OrderByDescending(m => m.AvgRating)
            //    .Take(30).AsNoTracking()
            //    ;

             

            var query = @"SELECT 
                            TS.Title, 
                            TS.[Year], 
                            TS.MovieId, 
                            TS.AvgRating, 
                            TS.IsAdult, 
                            TS.MoviesDetailsId, 
                            TS.NumberOfVotes, 
                            TS.OriginalTitle, 
                            TS.RunTimeMinutes 
                        FROM 
                            dbo.TrigramSearch({0}) 
                        AS TS";

            var movies = context.Movies
                .FromSqlRaw(query, "%" + title + "%")
                .OrderByDescending(m=>m.AvgRating);

            // Map to movie and return

            return movies;
        }

        //public double StringSimilarityScore(string title, int? year, string searchTerm)
        //{
        //    if (year.Equals(searchTerm))
        //    {
        //        return 0;
        //    }
        //    else if (title.Contains(searchTerm))
        //    {
        //        return (double)searchTerm.Length / (double)title.Length;
        //    }
        //    return 0;
        //}

        public IQueryable<Movies> GetAdultMovies(bool isAdult)
        {
            var movies = context.Movies
                .Where(x=> x.IsAdult == true);

            return movies;
        }

        public IQueryable<Movies> GetMoviesByGenre(int genreId)
        {
            var movies = (from mg in context.MovieGenres
                          where mg.GenresId == genreId
                          select mg.Movie);
            return movies;

        }


        public Movies GetMovie(string movieId, bool includeDetails = true)
        {
            var movie = context.Movies
                .Where(m => m.MovieId == movieId)
                .Include(m => m.Details)
                .Include(m => m.MovieGenres)
                .ThenInclude(mg => mg.Genre);
                

            return movie.FirstOrDefault();
        }


        static int UseCounter = 0;
        public async Task<MoviesDetails> GetMovieDetails(string movieId)
        {
            UseCounter++;

            var httpClient = new HttpClient();
            var omdbApiUrl = "http://www.omdbapi.com/?s=&apiKey=";

            //key3: d21d9cf7
            string key; if (UseCounter %2 == 0) key = "63709ce8"; else key = "dd0fa8bc";
            
            var uriBuilder = new UriBuilder(omdbApiUrl);
            var queryString = HttpUtility.ParseQueryString(uriBuilder.Query);
            queryString.Set("i", movieId);
            queryString.Set("apiKey", key);
            uriBuilder.Query = queryString.ToString();            
            var httpResponse = await httpClient.GetAsync(uriBuilder.Uri);
            
            var json = await httpResponse.Content.ReadAsStringAsync();
            if (json != "{\"Response\":\"False\",\"Error\":\"Error getting data.\"}")
            {
                var details = JsonConvert.DeserializeObject<MoviesDetails>(json);
                if (details.Poster == null || !details.Poster.StartsWith("http"))
                {
                    details.Poster = "https://thefilmuniverse.com/wp-content/uploads/2019/09/Poster_Not_Available2.jpg";
                }

                return details;
            }
            else
            {
                var details = new MoviesDetails
                {
                    ShortPlot = "(Unknown)",
                    Plot = "(Unknown)",
                    Actors = "(Unknown)",
                    Director = "(Unknown)",
                    Released = "(Unknown)",
                    Poster = "https://thefilmuniverse.com/wp-content/uploads/2019/09/Poster_Not_Available2.jpg",
                    Country = "(Unknown)"
                };

                return details;
            }        
        }
        public IQueryable<RatedMovies> GetAllReviews()
        {
            return context.RatedMovies
                .Include(x => x.Movie)
                .OrderByDescending(y => y.RatingId);
        }

        public async Task<bool> SaveChangesAsync()
        {
            // Only return success if at least one row was changed
            return (await context.SaveChangesAsync()) > 0;
        }
        public void Add<T>(T entity) where T : class
        {
            context.Add(entity);
        }

        public IQueryable<CarouselDatas> GetCarouselData()
        {
            var carouselData =
                from c in context.CarouselDatas select c;

            return carouselData;
        }
    }
}
