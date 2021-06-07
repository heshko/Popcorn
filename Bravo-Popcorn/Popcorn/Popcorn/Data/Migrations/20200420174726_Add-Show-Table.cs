using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Popcorn.Data.Migrations
{
    public partial class AddShowTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Shows",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    date = table.Column<DateTime>(nullable: false),
                    movieId = table.Column<int>(nullable: false),
                    saloonId = table.Column<int>(nullable: false),
                    cinemaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shows", x => x.id);
                    table.ForeignKey(
                        name: "FK_Shows_Cinemas_cinemaId",
                        column: x => x.cinemaId,
                        principalTable: "Cinemas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Shows_Movies_movieId",
                        column: x => x.movieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Shows_Saloons_saloonId",
                        column: x => x.saloonId,
                        principalTable: "Saloons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction,
                        onUpdate: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Shows_cinemaId",
                table: "Shows",
                column: "cinemaId");

            migrationBuilder.CreateIndex(
                name: "IX_Shows_movieId",
                table: "Shows",
                column: "movieId");

            migrationBuilder.CreateIndex(
                name: "IX_Shows_saloonId",
                table: "Shows",
                column: "saloonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Shows");
        }
    }
}
