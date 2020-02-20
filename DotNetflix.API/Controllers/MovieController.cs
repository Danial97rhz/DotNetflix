﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DotNetflix.API.ModelsDto;
using DotNetflix.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace DotNetflix.API.Controllers
{
    [ApiController]
    [Route("api/movies")]
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

        [HttpGet]
        public ActionResult<IEnumerable<MovieDto>> GetMovies(
            [FromQuery] string title)
        {
            var moviesFromRepo = movieRepository.GetMovies(title);
            return Ok(mapper.Map<IEnumerable<MovieDto>>(moviesFromRepo));
        }
    }
}
