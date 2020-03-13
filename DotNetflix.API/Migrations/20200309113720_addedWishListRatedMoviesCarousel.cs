using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DotNetflix.API.Migrations
{
    public partial class addedWishListRatedMoviesCarousel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarouselDatas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    ImgPath = table.Column<string>(nullable: true),
                    Text = table.Column<string>(nullable: true),
                    DateAdded = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarouselDatas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RatedMovies",
                columns: table => new
                {
                    RatingId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MovieId = table.Column<string>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    UserRating = table.Column<int>(nullable: false),
                    ReviewText = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RatedMovies", x => x.RatingId);
                    table.ForeignKey(
                        name: "FK_RatedMovies_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "MovieId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Wishlist",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MovieId = table.Column<string>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    DateAdded = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wishlist", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wishlist_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "MovieId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RatedMovies_MovieId",
                table: "RatedMovies",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_Wishlist_MovieId",
                table: "Wishlist",
                column: "MovieId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarouselDatas");

            migrationBuilder.DropTable(
                name: "RatedMovies");

            migrationBuilder.DropTable(
                name: "Wishlist");
        }
    }
}
