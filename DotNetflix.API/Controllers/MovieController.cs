using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using DotNetflix.API.ModelsDto;
using DotNetflix.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;

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
        public async Task<ActionResult<MovieDto>> GetMovieAsync(string movieId)
        {
            var movie = movieRepository.GetMovie(movieId);
            if (movie == null)
            {
                return NotFound("Movie not found");
            }

            else 
            {
                    //Check if data is already saved in local databse
                if (movie.PosterUrl == null || movie.LongPlot == null)
                {
                        var httpClient = new HttpClient();
                        var omdbApiUrl = "http://www.omdbapi.com/?s=&apiKey=";
                        var key = "dd0fa8bc";
                        var uriBuilder = new UriBuilder(omdbApiUrl);
                        var queryString = HttpUtility.ParseQueryString(uriBuilder.Query);
                        queryString.Set("i", movieId);
                        queryString.Set("apiKey", key);
                        uriBuilder.Query = queryString.ToString();

                        HttpResponseMessage httpResponse = await httpClient.GetAsync(uriBuilder.Uri);
                        MovieDto response = new MovieDto();
                        var json = await httpResponse.Content.ReadAsStringAsync();
                        response = JsonConvert.DeserializeObject<MovieDto>(json);
                    if (response.PosterUrl is "N/A")
                    {
                        response.PosterUrl = "https://thefilmuniverse.com/wp-content/uploads/2019/09/Poster_Not_Available2.jpg";
                    }
                    if (response.ReleaseDate is "N/A")
                    {
                        response.ReleaseDate = "11 September 2001";
                    }
                        movie.PosterUrl = response.PosterUrl;
                        movie.LongPlot = response.LongPlot;
                        movie.Director = response.Director;
                        movie.ReleaseDate = Convert.ToDateTime( response.ReleaseDate);
                        movie.Actors = response.Actors.Split(',').ToList();

                    //TODO: Fix this MESS
                    using (SqlConnection con = new SqlConnection("Server=(localdb)\\MSSQLLocalDB;Database=DotNetflixDb;Trusted_Connection=True;"))
                    {
                        con.Open();
                        SqlCommand command = new SqlCommand("insert into [MoviesDetails] (PosterUrl, Actors, ReleaseDate, Director, LongPlot) VALUES (@PosterUrl, @Actors ,@ReleaseDate , @Director , @LongPlot)" +
                            "UPDATE Movies SET MoviesDetailsId = (select top 1 id from MoviesDetails order by id desc) WHERE MovieId=@movieId", con);
                        command.Parameters.AddWithValue("@movieId", movieId);
                        command.Parameters.AddWithValue("@PosterUrl", response.PosterUrl);
                        command.Parameters.AddWithValue("@Actors", response.Actors);
                        command.Parameters.AddWithValue("@ReleaseDate", response.ReleaseDate);
                        command.Parameters.AddWithValue("@Director", response.Director);
                        command.Parameters.AddWithValue("@LongPlot", response.LongPlot);
                        command.ExecuteNonQuery();
                        con.Close();
                    }

                }

                //if (movie.PosterUrl == null)
                //{
                //    GetMovieDetailsFrom OMDBAPI
                //}
                return Ok(movie);
            }

            
        }
    }
}
