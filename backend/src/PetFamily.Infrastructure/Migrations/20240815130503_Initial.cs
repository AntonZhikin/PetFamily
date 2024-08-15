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
                name: "pets",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    species = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    breed = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    color = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    pet_health_info = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    address = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    weight = table.Column<float>(type: "real", maxLength: 100, nullable: false),
                    height = table.Column<float>(type: "real", maxLength: 100, nullable: false),
                    phone_number = table.Column<string>(type: "text", nullable: false),
                    is_neutered = table.Column<bool>(type: "boolean", maxLength: 100, nullable: false),
                    date_of_birth = table.Column<DateOnly>(type: "date", maxLength: 100, nullable: false),
                    is_vaccine = table.Column<bool>(type: "boolean", maxLength: 100, nullable: false),
                    status = table.Column<int>(type: "integer", maxLength: 100, nullable: false),
                    date_create = table.Column<DateOnly>(type: "date", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_pets", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "petphotos",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    path = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    is_main = table.Column<bool>(type: "boolean", maxLength: 100, nullable: false),
                    pet_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_petphotos", x => x.id);
                    table.ForeignKey(
                        name: "fk_petphotos_pets_pet_id",
                        column: x => x.pet_id,
                        principalTable: "pets",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "requisites",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    title = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    pet_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_requisites", x => x.id);
                    table.ForeignKey(
                        name: "fk_requisites_pets_pet_id",
                        column: x => x.pet_id,
                        principalTable: "pets",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "ix_petphotos_pet_id",
                table: "petphotos",
                column: "pet_id");

            migrationBuilder.CreateIndex(
                name: "ix_requisites_pet_id",
                table: "requisites",
                column: "pet_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "petphotos");

            migrationBuilder.DropTable(
                name: "requisites");

            migrationBuilder.DropTable(
                name: "pets");
        }
    }
}
