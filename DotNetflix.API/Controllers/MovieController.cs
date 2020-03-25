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
        public async Task<ActionResult<SearchResult>> GetPaginatedMovies(Search search)
        {
            var movies = movieRepository.GetMovies(search.Title);

            SearchResult sr = new SearchResult()
            {
                Count = movies.Count(),
                Title = search.Title,
                CurrentPage = search.CurrentPage,
                PageSize = search.PageSize
            };

            sr.Movies = Map.ToMovie(movies.Skip((search.CurrentPage - 1) * search.PageSize).Take(search.PageSize))
                .ToList();

            for (int i = 0; i < sr.Movies.Count(); i++)
            {
           
                if (sr.Movies[i].PosterUrl == null || !sr.Movies[i].PosterUrl.StartsWith("http"))
                {
                    var detailsEntity = await movieRepository.GetMovieDetails(sr.Movies[i].Id);
                    var movieEntity = movieRepository.GetMovie(sr.Movies[i].Id);

                    detailsEntity.Movie = movieEntity;

                    movieRepository.Add(detailsEntity);
                    await movieRepository.SaveChangesAsync();

                    sr.Movies[i] = Map.ToMovieFromObject(movieEntity, detailsEntity);
                }
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
        public async Task<ActionResult<Movie>> GetMoviesByGenre(int genreId)
        {
            var movies = movieRepository.GetMoviesByGenre(genreId);

            var mappedmovies = Map.ToMovie(
                movies
                .Where(x => x.NumberOfVotes > 50000).
                OrderByDescending(x => x.AvgRating)
                .Take(16)
                ).ToList();

            for (int i = 0; i < mappedmovies.Count(); i++)
            {

                if (mappedmovies[i].PosterUrl == null || !mappedmovies[i].PosterUrl.StartsWith("http"))
                {
                    var detailsEntity = await movieRepository.GetMovieDetails(mappedmovies[i].Id);
                    var movieEntity = movieRepository.GetMovie(mappedmovies[i].Id);

                    detailsEntity.Movie = movieEntity;

                    movieRepository.Add(detailsEntity);
                    await movieRepository.SaveChangesAsync();

                    mappedmovies[i] = Map.ToMovieFromObject(movieEntity, detailsEntity);
                }
            }

            if (mappedmovies == null)
            {
                return NotFound("No movies of selected genre could be found.");
            }

            return Ok(mappedmovies);
        }

        [HttpGet("{isAdult}")]
        public async Task<ActionResult<IEnumerable<Movie>>> GetAdultMovies(bool isAdult)
        {
            var movies = movieRepository.GetAdultMovies(isAdult);

            var mappedMovies = Map.ToMovie(
                movies
                .Where(x => x.NumberOfVotes > 100)
                .OrderByDescending(x => x.AvgRating)
                .Take(16)
                ).ToList();

            if (mappedMovies == null)
            {
                return NotFound("No movies of selected genre could be found.");
            }
            for (int i = 0; i < mappedMovies.Count(); i++)
            {

                if (mappedMovies[i].PosterUrl == null || !mappedMovies[i].PosterUrl.StartsWith("http"))
                {
                    var detailsEntity = await movieRepository.GetMovieDetails(mappedMovies[i].Id);
                    var movieEntity = movieRepository.GetMovie(mappedMovies[i].Id);

                    detailsEntity.Movie = movieEntity;

                    movieRepository.Add(detailsEntity);
                    await movieRepository.SaveChangesAsync();

                    mappedMovies[i] = Map.ToMovieFromObject(movieEntity, detailsEntity);
                }
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
