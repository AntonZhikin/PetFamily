using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetFamily.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "species",
                columns: table => new
                {
                    species_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_species", x => x.species_id);
                });

            migrationBuilder.CreateTable(
                name: "volunteers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    experienceyears = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    secondName = table.Column<string>(type: "text", nullable: false),
                    surname = table.Column<string>(type: "text", nullable: false),
                    phonenumber = table.Column<string>(type: "text", nullable: false),
                    AssistanceDetailList = table.Column<string>(type: "jsonb", nullable: false),
                    SocialNetworkList = table.Column<string>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_volunteers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "breed",
                columns: table => new
                {
                    breed_id = table.Column<Guid>(type: "uuid", nullable: false),
                    species_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_breed", x => x.breed_id);
                    table.ForeignKey(
                        name: "fk_breed_species_species_id",
                        column: x => x.species_id,
                        principalTable: "species",
                        principalColumn: "species_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "pets",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    date_of_birth = table.Column<DateTime>(type: "timestamp with time zone", maxLength: 2000, nullable: false),
                    is_vaccine = table.Column<bool>(type: "boolean", maxLength: 2000, nullable: false),
                    help_status = table.Column<int>(type: "integer", maxLength: 2000, nullable: false),
                    date_create = table.Column<DateTime>(type: "timestamp with time zone", maxLength: 2000, nullable: false),
                    photos = table.Column<string>(type: "jsonb", nullable: false),
                    volunteer_id = table.Column<Guid>(type: "uuid", nullable: true),
                    address_city = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    address_street = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    color = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    height = table.Column<float>(type: "real", maxLength: 2000, nullable: false),
                    isNeutered = table.Column<bool>(type: "boolean", maxLength: 2000, nullable: false),
                    name = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    petHealthInfo = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    phone_number = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    Position = table.Column<int>(type: "integer", nullable: false),
                    breed_id = table.Column<Guid>(type: "uuid", nullable: false),
                    species_id = table.Column<Guid>(type: "uuid", nullable: false),
                    weight = table.Column<float>(type: "real", maxLength: 2000, nullable: false),
                    requisite = table.Column<string>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_pets", x => x.id);
                    table.ForeignKey(
                        name: "fk_pets_volunteers_volunteer_id",
                        column: x => x.volunteer_id,
                        principalTable: "volunteers",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "ix_breed_species_id",
                table: "breed",
                column: "species_id");

            migrationBuilder.CreateIndex(
                name: "ix_pets_volunteer_id",
                table: "pets",
                column: "volunteer_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "breed");

            migrationBuilder.DropTable(
                name: "pets");

            migrationBuilder.DropTable(
                name: "species");

            migrationBuilder.DropTable(
                name: "volunteers");
        }
    }
}
