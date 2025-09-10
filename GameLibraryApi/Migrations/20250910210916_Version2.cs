using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameLibraryApi.Migrations
{
    /// <inheritdoc />
    public partial class Version2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AgeRestriction",
                table: "GameInformations");

            migrationBuilder.DropColumn(
                name: "GameType",
                table: "GameInformations");

            migrationBuilder.DropColumn(
                name: "Genre",
                table: "GameInformations");

            migrationBuilder.AddColumn<int>(
                name: "AgeRestrictionId",
                table: "GameInformations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GameTypeId",
                table: "GameInformations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GenreId",
                table: "GameInformations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AgeRestrictions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgeRestrictions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GameTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameInformations_AgeRestrictionId",
                table: "GameInformations",
                column: "AgeRestrictionId");

            migrationBuilder.CreateIndex(
                name: "IX_GameInformations_GameTypeId",
                table: "GameInformations",
                column: "GameTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_GameInformations_GenreId",
                table: "GameInformations",
                column: "GenreId");

            migrationBuilder.AddForeignKey(
                name: "FK_GameInformations_AgeRestrictions_AgeRestrictionId",
                table: "GameInformations",
                column: "AgeRestrictionId",
                principalTable: "AgeRestrictions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GameInformations_GameTypes_GameTypeId",
                table: "GameInformations",
                column: "GameTypeId",
                principalTable: "GameTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GameInformations_Genres_GenreId",
                table: "GameInformations",
                column: "GenreId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameInformations_AgeRestrictions_AgeRestrictionId",
                table: "GameInformations");

            migrationBuilder.DropForeignKey(
                name: "FK_GameInformations_GameTypes_GameTypeId",
                table: "GameInformations");

            migrationBuilder.DropForeignKey(
                name: "FK_GameInformations_Genres_GenreId",
                table: "GameInformations");

            migrationBuilder.DropTable(
                name: "AgeRestrictions");

            migrationBuilder.DropTable(
                name: "GameTypes");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropIndex(
                name: "IX_GameInformations_AgeRestrictionId",
                table: "GameInformations");

            migrationBuilder.DropIndex(
                name: "IX_GameInformations_GameTypeId",
                table: "GameInformations");

            migrationBuilder.DropIndex(
                name: "IX_GameInformations_GenreId",
                table: "GameInformations");

            migrationBuilder.DropColumn(
                name: "AgeRestrictionId",
                table: "GameInformations");

            migrationBuilder.DropColumn(
                name: "GameTypeId",
                table: "GameInformations");

            migrationBuilder.DropColumn(
                name: "GenreId",
                table: "GameInformations");

            migrationBuilder.AddColumn<string>(
                name: "AgeRestriction",
                table: "GameInformations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GameType",
                table: "GameInformations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Genre",
                table: "GameInformations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
