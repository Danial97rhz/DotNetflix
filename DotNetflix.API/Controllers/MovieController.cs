using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DotNetflix.API.Models;
using DotNetflix.API.ModelsDto;
using DotNetflix.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace DotNetflix.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class MovieController : Controller
    {
        private readonly IMovieRepository movieRepository;
        private readonly IMapper mapper;

        public MovieController(IMovieRepository movieRepository,
            IMapper mapper)
        {
            this.movieRepository = movieRepository ?? 
                throw new ArgumentNullException(nameof(movieRepository));
            this.mapper = mapper ?? 
                throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet("{title}")]
        public ActionResult<IEnumerable<MovieDto>> GetMovies(string title)
        {
            var movies = movieRepository.GetMovies(title);
            //var mapResult = mapper.Map<IEnumerable<MovieDto>>(moviesFromRepo);



            if (movies == null)
            {
                return NotFound("Movie not found");
            }
            return Ok(movies);
        }

        [HttpGet("{movieId}")]
        public ActionResult<MovieDto> GetMovie(string movieId)
        {
            var movie = movieRepository.GetMovie(movieId);
            if (movie == null)
            {
                return NotFound("Movie not found");
            }

            //if (movie.PosterUrl == null)
            //{
            //    GetMovieDetailsFrom OMDBAPI
            //}
            return Ok(movie);
        }


        [HttpGet("{genreId}")]
        public ActionResult<MovieDto> GetMoviesByGenre(int genreId)
        {
            var movies = movieRepository.GetMoviesByGenre(genreId);
            if (movies == null)
            {
                return BadRequest("Movies not found");
            }

            return Ok(movies);
        }
    }
}
