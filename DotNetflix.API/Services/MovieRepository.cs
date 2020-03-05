using DotNetflix.API.Context;
using DotNetflix.API.Entities;
using DotNetflix.API.HelperMethods;
using DotNetflix.API.ModelsDto;
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

        /* Get all movies containing search term.
        If search term is left empty all movies are returned*/
        public IQueryable<Movies> GetMovies(string title)
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
                    || m.Year.ToString().Equals(title)).OrderByDescending(x=> x.AvgRating).ThenByDescending(x=>x.NumberOfVotes);
            //.Select(m => m);

            // Map to movie dto (data transfer object) and return
            return movies;
        }

        public IQueryable<Movies> GetMoviesByGenre(int genreId)
        {
            var movies = (from mg in context.MovieGenres
                          where mg.GenresId == genreId
                          select mg.Movie).Where(x=>x.NumberOfVotes>50000).OrderByDescending(x=> x.AvgRating).Take(10);

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



        public async Task<MoviesDetails> GetMovieDetails(string movieId)
        {
            var httpClient = new HttpClient();
            var omdbApiUrl = "http://www.omdbapi.com/?s=&apiKey=";
            var key = "dd0fa8bc";
            var uriBuilder = new UriBuilder(omdbApiUrl);
            var queryString = HttpUtility.ParseQueryString(uriBuilder.Query);
            queryString.Set("i", movieId);
            queryString.Set("apiKey", key);
            uriBuilder.Query = queryString.ToString();            
            var httpResponse = await httpClient.GetAsync(uriBuilder.Uri);
            
            var json = await httpResponse.Content.ReadAsStringAsync();
            var details = JsonConvert.DeserializeObject<MoviesDetails>(json);
            //if (details.Poster == null)
            //{
            //    details.Poster = "https://thefilmuniverse.com/wp-content/uploads/2019/09/Poster_Not_Available2.jpg";
            //}

            return details;            
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
    }
}
