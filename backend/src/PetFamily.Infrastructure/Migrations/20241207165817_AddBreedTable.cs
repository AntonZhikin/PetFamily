using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetFamily.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddBreedTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Breeds",
                table: "species");

            migrationBuilder.RenameColumn(
                name: "SocialNetworks",
                table: "volunteers",
                newName: "SocialNetworkList");

            migrationBuilder.RenameColumn(
                name: "AssistanceDetails",
                table: "volunteers",
                newName: "AssistanceDetailList");

            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "species",
                type: "character varying(2000)",
                maxLength: 2000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "breeds",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    species_id = table.Column<Guid>(type: "uuid", nullable: true),
                    name = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_breeds", x => x.id);
                    table.ForeignKey(
                        name: "fk_breeds_species_species_id",
                        column: x => x.species_id,
                        principalTable: "species",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "ix_breeds_species_id",
                table: "breeds",
                column: "species_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "breeds");

            migrationBuilder.DropColumn(
                name: "name",
                table: "species");

            migrationBuilder.RenameColumn(
                name: "SocialNetworkList",
                table: "volunteers",
                newName: "SocialNetworks");

            migrationBuilder.RenameColumn(
                name: "AssistanceDetailList",
                table: "volunteers",
                newName: "AssistanceDetails");

            migrationBuilder.AddColumn<string>(
                name: "Breeds",
                table: "species",
                type: "jsonb",
                nullable: false,
                defaultValue: "");
        }
    }
}
