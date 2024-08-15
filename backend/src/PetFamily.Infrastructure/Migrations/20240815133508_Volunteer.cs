using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetFamily.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Volunteer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "vol_id",
                table: "pets",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "voluunter",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    full_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    descriptions = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    experience_years = table.Column<int>(type: "integer", maxLength: 100, nullable: false),
                    count_pet_in_home = table.Column<int>(type: "integer", maxLength: 100, nullable: false),
                    count_pet_found_home = table.Column<int>(type: "integer", maxLength: 100, nullable: false),
                    count_pet_healing = table.Column<int>(type: "integer", maxLength: 100, nullable: false),
                    phone_number = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_voluunter", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "requisiteforhelp",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    vol_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_requisiteforhelp", x => x.id);
                    table.ForeignKey(
                        name: "fk_requisiteforhelp_voluunter_vol_id",
                        column: x => x.vol_id,
                        principalTable: "voluunter",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "socialmedia",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    path = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    vol_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_socialmedia", x => x.id);
                    table.ForeignKey(
                        name: "fk_socialmedia_voluunter_vol_id",
                        column: x => x.vol_id,
                        principalTable: "voluunter",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "ix_pets_vol_id",
                table: "pets",
                column: "vol_id");

            migrationBuilder.CreateIndex(
                name: "ix_requisiteforhelp_vol_id",
                table: "requisiteforhelp",
                column: "vol_id");

            migrationBuilder.CreateIndex(
                name: "ix_socialmedia_vol_id",
                table: "socialmedia",
                column: "vol_id");

            migrationBuilder.AddForeignKey(
                name: "fk_pets_voluunter_vol_id",
                table: "pets",
                column: "vol_id",
                principalTable: "voluunter",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_pets_voluunter_vol_id",
                table: "pets");

            migrationBuilder.DropTable(
                name: "requisiteforhelp");

            migrationBuilder.DropTable(
                name: "socialmedia");

            migrationBuilder.DropTable(
                name: "voluunter");

            migrationBuilder.DropIndex(
                name: "ix_pets_vol_id",
                table: "pets");

            migrationBuilder.DropColumn(
                name: "vol_id",
                table: "pets");
        }
    }
}
