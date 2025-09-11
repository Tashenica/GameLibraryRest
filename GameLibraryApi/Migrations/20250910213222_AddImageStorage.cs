using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameLibraryApi.Migrations
{
    /// <inheritdoc />
    public partial class AddImageStorage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageContentType",
                table: "GameInformations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "ImageData",
                table: "GameInformations",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageFileName",
                table: "GameInformations",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageContentType",
                table: "GameInformations");

            migrationBuilder.DropColumn(
                name: "ImageData",
                table: "GameInformations");

            migrationBuilder.DropColumn(
                name: "ImageFileName",
                table: "GameInformations");
        }
    }
}
