using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Popcorn.Data.Migrations
{
    public partial class AddMovie : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    Trailer = table.Column<string>(nullable: false),
                    Genre = table.Column<string>(nullable: false),
                    Actors = table.Column<string>(nullable: false),
                    Director = table.Column<string>(nullable: false),
                    PlayTimeLength = table.Column<TimeSpan>(nullable: false),
                    MoviePoster = table.Column<string>(nullable: false),
                    ReleaseDate = table.Column<DateTime>(nullable: false),
                    HasBeenReleased = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Movies");
        }
    }
}
