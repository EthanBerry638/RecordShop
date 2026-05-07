using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RecordShop.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Albums",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Artist = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Albums", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Albums",
                columns: new[] { "Id", "Artist", "Price", "Title" },
                values: new object[,]
                {
                    { 1, "Michael Jackson", 10.99m, "Thriller" },
                    { 2, "AC/DC", 9.49m, "Back In Black" },
                    { 3, "Pink Floyd", 15.00m, "The Dark Side of the Moon" },
                    { 4, "Nirvana", 5.99m, "Nevermind" },
                    { 5, "Fleetwood Mac", 12.50m, "Rumours" },
                    { 6, "The Beatles", 14.99m, "Abbey Road" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Albums");
        }
    }
}
