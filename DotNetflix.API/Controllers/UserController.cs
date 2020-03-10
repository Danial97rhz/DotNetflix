using DotNetflix.API.Entities;
using DotNetflix.API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotNetflix.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repository;

        public UserController(IUserRepository repository)
        {
            _repository = repository;
        }

        // GET: api/User
        [HttpGet("{userId}")]
        public ActionResult<IEnumerable<WishlistMovies>> GetWishlist(int userId)
        {
            var wishlist = _repository.GetWishList(userId);

            if (wishlist == null)
            {
                return NotFound("Wishlist not found.");
            }

            return wishlist;
        }

        [HttpPost]
        public async Task<ActionResult<WishlistMovies>> PostWishlistMovie(WishlistMovies wishlistMovies)
        {
            _repository.AddToWishlist(wishlistMovies);

            if (!await _repository.Save())
            {
                return BadRequest("Add to wishlist has failed.");
            }

            return Ok();

            //make it work
            //return CreatedAtAction("GetUserWishlistMovie", new { id = wishlistMovies.Id }, wishlistMovies);
        }


        [HttpGet("{id}")]
        public ActionResult<WishlistMovies> GetWishlistMovie(int id)
        {
            var wishlistMovie = _repository.GetWishlistMovie(id);

            if (wishlistMovie == null)
            {
                return NotFound();
            }

            return wishlistMovie;
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<WishlistMovies>> DeleteWishlistMovie(int id)
        {
            var wishlistMovies = _repository.GetWishlistMovie(id);
            if (wishlistMovies == null)
            {
                return NotFound();
            }

            _repository.DeleteWishlistMovie(wishlistMovies);
            await _repository.Save();

            return wishlistMovies;
        }


        [HttpGet("{userId}")]
        public ActionResult<IEnumerable<RatedMovies>> GetRatedMovieList(int userId)
        {
            var RatedMovieList = _repository.GetRatedMovieList(userId);

            if (RatedMovieList == null)
            {
                return NotFound("Rate Movies not found.");
            }

            return RatedMovieList;
        }

        [HttpPost]
        public async Task<ActionResult<WishlistMovies>> PostRatedMovie(RatedMovies ratedMovies)
        {
            _repository.AddRatedMovie(ratedMovies);

            if (!await _repository.Save())
            {
                return BadRequest("Add to rated movies has failed.");
            }

            return Ok();

            //make it work
            //return CreatedAtAction("GetUserWishlistMovie", new { id = wishlistMovies.Id }, wishlistMovies);
        }

        [HttpGet("{id}")]
        public ActionResult<RatedMovies> GetRatedMovie(int id)
        {
            var ratedMovie = _repository.GetRatedMovie(id);

            if (ratedMovie == null)
            {
                return NotFound();
            }

            return ratedMovie;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<RatedMovies>> DeleteRatedMovie(int id)
        {
            var RatedMovie = _repository.GetRatedMovie(id);
            if (RatedMovie == null)
            {
                return NotFound();
            }

            _repository.DeleteRatedMovie(RatedMovie);
            await _repository.Save();

            return RatedMovie;
        }

       
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRatedMovie(int id, RatedMovies ratedMovie)
        {
            if (id != ratedMovie.RatingId)
            {
                return BadRequest();
            }

            _repository.UpdateRatedMovie(ratedMovie);

            if (!await _repository.Save())
            {
                return BadRequest();
            }
            
            return NoContent();
        }

        //private bool WishlistMoviesExists(int id)
        //{
        //    return _context.Wishlist.Any(e => e.Id == id);
        //}
    }
}
