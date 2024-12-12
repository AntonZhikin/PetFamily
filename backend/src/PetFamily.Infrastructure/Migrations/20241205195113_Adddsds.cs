using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetFamily.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Adddsds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "petphotos",
                table: "pets",
                newName: "petPhotos");

            migrationBuilder.RenameColumn(
                name: "serial_number",
                table: "pets",
                newName: "Position");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "petPhotos",
                table: "pets",
                newName: "petphotos");

            migrationBuilder.RenameColumn(
                name: "Position",
                table: "pets",
                newName: "serial_number");
        }
    }
}
