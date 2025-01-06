using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetFamily.Pets.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Pets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "PetFamily_Pets");

            migrationBuilder.CreateTable(
                name: "volunteers",
                schema: "PetFamily_Pets",
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
                    assistance_detail_list = table.Column<string>(type: "jsonb", nullable: false),
                    social_network_list = table.Column<string>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_volunteers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "pets",
                schema: "PetFamily_Pets",
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
                    is_neutered = table.Column<bool>(type: "boolean", maxLength: 2000, nullable: false),
                    name = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    petHealthInfo = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    phone_number = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    position = table.Column<int>(type: "integer", nullable: false),
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
                        principalSchema: "PetFamily_Pets",
                        principalTable: "volunteers",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "ix_pets_volunteer_id",
                schema: "PetFamily_Pets",
                table: "pets",
                column: "volunteer_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "pets",
                schema: "PetFamily_Pets");

            migrationBuilder.DropTable(
                name: "volunteers",
                schema: "PetFamily_Pets");
        }
    }
}
