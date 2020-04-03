using DotNetflix.API.Entities;
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
        public DbSet<Genres> Genres { get; set; }
        public DbSet<Movies> Movies { get; set; }
        public DbSet<MovieGenres> MovieGenres { get; set; }
        public DbSet<MoviesDetails> MoviesDetails { get; set; }
        public DbSet<WishlistMovies> Wishlist { get; set; }
        public DbSet<RatedMovies> RatedMovies { get; set; }
        public DbSet<CarouselDatas> CarouselDatas { get; set; }

        //public DbSet<Role> Roles { get; set; }
        //public DbSet<User> Users { get; set; }
        //public DbSet<UserMovies> UserMovies { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WishlistMovies>(entity =>
            {
                entity.HasKey(m => m.Id);

            });

            modelBuilder.Entity<RatedMovies>(entity =>
            {
                entity.HasKey(m => m.RatingId);
                
            });
            modelBuilder.Entity<MovieGenres>(entity =>
            { 
                entity.HasKey(mg => new { mg.MoviesId, mg.GenresId });

                entity.HasOne(mg => mg.Genre)
                .WithMany(g => g.MovieGenres)
                .HasForeignKey(mg => mg.GenresId);

                entity.HasOne(mg => mg.Movie)
                .WithMany(m => m.MovieGenres)
                .HasForeignKey(mg => mg.MoviesId);

            });

            modelBuilder.Entity<Movies>( entity =>
            {
                entity.HasKey(m => m.MovieId);
              
                entity.HasOne(m => m.Details)
                .WithOne(d => d.Movie);
            });
        }
    }
}
