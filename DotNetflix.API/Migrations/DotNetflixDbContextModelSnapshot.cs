﻿// <auto-generated />
using System;
using DotNetflix.API.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DotNetflix.API.Migrations
{
    [DbContext(typeof(DotNetflixDbContext))]
    partial class DotNetflixDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DotNetflix.API.Entities.Genres", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ImgPath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("DotNetflix.API.Entities.MovieGenres", b =>
                {
                    b.Property<string>("MoviesId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("GenresId")
                        .HasColumnType("int");

                    b.HasKey("MoviesId", "GenresId");

                    b.HasIndex("GenresId");

                    b.ToTable("MovieGenres");
                });

            modelBuilder.Entity("DotNetflix.API.Entities.Movies", b =>
                {
                    b.Property<string>("MovieId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AvgRating")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsAdult")
                        .HasColumnType("bit");

                    b.Property<int?>("MoviesDetailsId")
                        .HasColumnType("int");

                    b.Property<int?>("NumberOfVotes")
                        .HasColumnType("int");

                    b.Property<string>("OriginalTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RunTimeMinutes")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Year")
                        .HasColumnType("int");

                    b.HasKey("MovieId");

                    b.HasIndex("MoviesDetailsId")
                        .IsUnique()
                        .HasFilter("[MoviesDetailsId] IS NOT NULL");

                    b.ToTable("Movies");
                });

            modelBuilder.Entity("DotNetflix.API.Entities.MoviesDetails", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Actors")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Director")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Plot")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Poster")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Released")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ShortPlot")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("MoviesDetails");
                });

            modelBuilder.Entity("DotNetflix.API.Entities.MovieGenres", b =>
                {
                    b.HasOne("DotNetflix.API.Entities.Genres", "Genre")
                        .WithMany("MovieGenres")
                        .HasForeignKey("GenresId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DotNetflix.API.Entities.Movies", "Movie")
                        .WithMany("MovieGenres")
                        .HasForeignKey("MoviesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DotNetflix.API.Entities.Movies", b =>
                {
                    b.HasOne("DotNetflix.API.Entities.MoviesDetails", "Details")
                        .WithOne("Movie")
                        .HasForeignKey("DotNetflix.API.Entities.Movies", "MoviesDetailsId");
                });
#pragma warning restore 612, 618
        }
    }
}
