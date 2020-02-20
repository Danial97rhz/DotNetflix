using DotNetflix.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetflix.API.Context
{
    public class DotNetflixDbContext : DbContext
    {
        /* Constructor gets DbContext options via 
        dependecy injections from the startup file */
        public DotNetflixDbContext(DbContextOptions<DotNetflixDbContext> options)
            : base(options)
        {

        }

        public DbSet<Genre> Genres { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MovieGenres> MovieGenres { get; set; }
        public DbSet<Poster> Posters { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<StoryLine> StoryLines { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserMovies> UserMovies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MovieGenres>().HasKey(mg => new { mg.MovieId, mg.GenreId });
            modelBuilder.Entity<UserMovies>().HasKey(um => new { um.MovieId, um.UserId });
        }
    }
}
