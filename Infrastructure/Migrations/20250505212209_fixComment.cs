using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoviesAPI.Migrations
{
    /// <inheritdoc />
    public partial class fixComment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Movie_MovieId",
                table: "Comments");

            migrationBuilder.DropTable(
                name: "Movie");

            migrationBuilder.DropIndex(
                name: "IX_Comments_MovieId",
                table: "Comments");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Movie",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Overview = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PosterPath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movie", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_MovieId",
                table: "Comments",
                column: "MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Movie_MovieId",
                table: "Comments",
                column: "MovieId",
                principalTable: "Movie",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
