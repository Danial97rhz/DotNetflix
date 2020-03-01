using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using DotNetflix.API.Models;
using DotNetflix.API.Entities;
using DotNetflix.API.ModelsDto;
using DotNetflix.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
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
        public ActionResult<IEnumerable<MovieDto>> GetMovies(string title)
        {
            var movies = movieRepository.GetMovies(title);

            var mappedMovies = Map.ToMovieDto(movies)
                .OrderBy(m => m.Title)
                .ToList();

            if (mappedMovies == null)
            {
                return NotFound("Movie not found");
            }
            return Ok(mappedMovies);
        }

        [HttpGet("{movieId}")]
        public async Task<ActionResult<MovieDto>> GetMovieAsync(string movieId)
        {

            var movie = movieRepository.GetMovie(movieId);
            if (movie == null)
            {
                return NotFound("Movie not found");
            }
            var movieModel = Map.ToMovieDtoFromObject(movie);
                      
            if (movieModel.PosterUrl == null)
            {
                var detailsEntity = await movieRepository.GetMovieDetails(movieId);
                var movieEntity = movie;

                detailsEntity.Movie = movieEntity;

                movieRepository.Add(detailsEntity);
                _ = movieRepository.SaveChangesAsync();

                movieModel = Map.ToMovieDtoFromObject(movie, detailsEntity);
            }
            

            return Ok(movieModel);
        }


        [HttpGet("{genreId}")]
        public ActionResult<MovieDto> GetMoviesByGenre(int genreId)
        {
            var movies = movieRepository.GetMoviesByGenre(genreId);

            var mappedmovies = Map.ToMovieDto(movies).ToList();

            if (mappedmovies == null)
            {
                return BadRequest("Movies not found");
            }

            return Ok(mappedmovies);
        }
    }
}
