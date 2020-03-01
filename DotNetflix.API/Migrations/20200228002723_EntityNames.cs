using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DotNetflix.API.Migrations
{
    public partial class EntityNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LongPlot",
                table: "MoviesDetails",
                newName: "Plot");

            migrationBuilder.RenameColumn(
                name: "PosterUrl",
                table: "MoviesDetails",
                newName: "Poster");

            migrationBuilder.RenameColumn(
                name: "ReleaseDate",
                table: "MoviesDetails",
                newName: "Released");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Plot",
                table: "MoviesDetails",
                newName: "LongPlot");

            migrationBuilder.RenameColumn(
                name: "Poster",
                table: "MoviesDetails",
                newName: "PosterUrl");

            migrationBuilder.RenameColumn(
                name: "Released",
                table: "MoviesDetails",
                newName: "ReleaseDate");
        }
    }
}
