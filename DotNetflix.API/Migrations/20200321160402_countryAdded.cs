using Microsoft.EntityFrameworkCore.Migrations;

namespace DotNetflix.API.Migrations
{
    public partial class countryAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "MoviesDetails",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Country",
                table: "MoviesDetails");
        }
    }
}
