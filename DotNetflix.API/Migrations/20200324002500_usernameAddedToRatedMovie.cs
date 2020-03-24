using Microsoft.EntityFrameworkCore.Migrations;

namespace DotNetflix.API.Migrations
{
    public partial class usernameAddedToRatedMovie : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "RatedMovies",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "RatedMovies");
        }
    }
}
