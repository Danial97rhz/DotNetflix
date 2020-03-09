using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DotNetflix.API.Context;
using DotNetflix.API.Entities;
using DotNetflix.API.Services;

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
            return _repository.GetUserWishList(userId);
        }

        // GET: api/User/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<WishlistMovies>> GetWishlistMovies(int id)
        //{
        //    var wishlistMovies = await _context.Wishlist.FindAsync(id);

        //    if (wishlistMovies == null)
        //    {
        //        return NotFound();
        //    }

        //    return wishlistMovies;
        //}

        // PUT: api/User/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutWishlistMovies(int id, WishlistMovies wishlistMovies)
        //{
        //    if (id != wishlistMovies.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(wishlistMovies).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!WishlistMoviesExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/User
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        //[HttpPost]
        //public async Task<ActionResult<WishlistMovies>> PostWishlistMovies(WishlistMovies wishlistMovies)
        //{
        //    _context.Wishlist.Add(wishlistMovies);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetWishlistMovies", new { id = wishlistMovies.Id }, wishlistMovies);
        //}

        //// DELETE: api/User/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<WishlistMovies>> DeleteWishlistMovies(int id)
        //{
        //    var wishlistMovies = await _context.Wishlist.FindAsync(id);
        //    if (wishlistMovies == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Wishlist.Remove(wishlistMovies);
        //    await _context.SaveChangesAsync();

        //    return wishlistMovies;
        //}

        //private bool WishlistMoviesExists(int id)
        //{
        //    return _context.Wishlist.Any(e => e.Id == id);
        //}
    }
}
