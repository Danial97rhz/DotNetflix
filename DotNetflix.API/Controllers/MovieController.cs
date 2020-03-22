using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetflix.API.Models;
using DotNetflix.API.Services;
using Microsoft.AspNetCore.Mvc;
using DotNetflix.API.HelperMethods;

namespace DotNetflix.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class MovieController : Controller
    {
        private readonly IMovieRepository movieRepository;
        //private readonly IMapper mapper; --> Not USED?

        public MovieController(IMovieRepository movieRepository)
        {
            this.movieRepository = movieRepository ?? 
                throw new ArgumentNullException(nameof(movieRepository));
            
            //NOT USED?
            //this.mapper = mapper ?? 
            //    throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet("{title}")]
        public ActionResult<IEnumerable<Movie>> GetMovies(string title)
        {
            var movies = movieRepository.GetMovies(title);

            var mappedMovies = Map.ToMovie(movies)
                .ToList();

            if (mappedMovies == null)
            {
                return NotFound("Movie not found");
            }
            return Ok(mappedMovies);
        }

        [HttpGet]
        public ActionResult<SearchResult> GetPaginatedMovies(Search search)
        {
            var movies = movieRepository.GetMovies(search.Title);
            int count = movies.Count();

            SearchResult sr = new SearchResult();

            sr.Movies = Map.ToMovie(movies.Skip((search.CurrentPage - 1) * search.PageSize).Take(search.PageSize))
                .ToList();
            sr.Count = count;
            sr.Title = search.Title;

            if (sr.Movies == null)
            {
                return NotFound("Movie not found");
            }
            return Ok(sr);
        }

        [HttpGet("{movieId}")]
        public async Task<ActionResult<Movie>> GetMovieAsync(string movieId)
        {

            var movie = movieRepository.GetMovie(movieId);
            if (movie == null)
            {
                return NotFound("Movie not found");
            }
            var movieModel = Map.ToMovieFromObject(movie);
                      
            if (movieModel.PosterUrl == null 
                || !movieModel.PosterUrl.StartsWith("http"))
            {
                var detailsEntity = await movieRepository.GetMovieDetails(movieId);
                var movieEntity = movie;

                detailsEntity.Movie = movieEntity;

                movieRepository.Add(detailsEntity);
                _ = movieRepository.SaveChangesAsync();

                movieModel = Map.ToMovieFromObject(movie, detailsEntity);
            }
            

            return Ok(movieModel);
        }


        [HttpGet("{genreId}")]
        public ActionResult<Movie> GetMoviesByGenre(int genreId)
        {
            var movies = movieRepository.GetMoviesByGenre(genreId);

            var mappedmovies = Map.ToMovie(movies).ToList();

            if (mappedmovies == null)
            {
                return NotFound("No movies of selected genre could be found.");
            }

            return Ok(mappedmovies);
        }

        [HttpGet("{isAdult}")]
        public ActionResult<IEnumerable<Movie>> GetAdultMovies(bool isAdult)
        {
            var movies = movieRepository.GetAdultMovies(isAdult);

            var mappedMovies = Map.ToMovie(movies).ToList();

            if (mappedMovies == null)
            {
                return NotFound("No movies of selected genre could be found.");
            }
            return Ok(mappedMovies);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Carousel>> GetCarouselData()
        {
            var data = movieRepository.GetCarouselData();
            if (data == null)
            {
                return NotFound("Data not found");
            }
            return Ok(data);
        }
    }
}
