using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetFamily.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SpeciesBreed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "petphotos");

            migrationBuilder.DropTable(
                name: "requisiteforhelp");

            migrationBuilder.DropTable(
                name: "requisites");

            migrationBuilder.DropTable(
                name: "socialmedia");

            migrationBuilder.DropColumn(
                name: "full_name",
                table: "voluunter");

            migrationBuilder.DropColumn(
                name: "address",
                table: "pets");

            migrationBuilder.DropColumn(
                name: "phone_number",
                table: "pets");

            migrationBuilder.RenameColumn(
                name: "species",
                table: "pets",
                newName: "petHealthInfo");

            migrationBuilder.RenameColumn(
                name: "pet_health_info",
                table: "pets",
                newName: "address_street");

            migrationBuilder.RenameColumn(
                name: "is_neutered",
                table: "pets",
                newName: "isNeutered");

            migrationBuilder.RenameColumn(
                name: "breed",
                table: "pets",
                newName: "address_city");

            migrationBuilder.AddColumn<string>(
                name: "ReqListDetails",
                table: "voluunter",
                type: "jsonb",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SocDetails",
                table: "voluunter",
                type: "jsonb",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "voluunter",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "secondName",
                table: "voluunter",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "surname",
                table: "voluunter",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "status",
                table: "pets",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<string>(
                name: "Details",
                table: "pets",
                type: "jsonb",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ReqDetails",
                table: "pets",
                type: "jsonb",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "species",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    Breeds = table.Column<string>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_species", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "species");

            migrationBuilder.DropColumn(
                name: "ReqListDetails",
                table: "voluunter");

            migrationBuilder.DropColumn(
                name: "SocDetails",
                table: "voluunter");

            migrationBuilder.DropColumn(
                name: "name",
                table: "voluunter");

            migrationBuilder.DropColumn(
                name: "secondName",
                table: "voluunter");

            migrationBuilder.DropColumn(
                name: "surname",
                table: "voluunter");

            migrationBuilder.DropColumn(
                name: "Details",
                table: "pets");

            migrationBuilder.DropColumn(
                name: "ReqDetails",
                table: "pets");

            migrationBuilder.RenameColumn(
                name: "petHealthInfo",
                table: "pets",
                newName: "species");

            migrationBuilder.RenameColumn(
                name: "isNeutered",
                table: "pets",
                newName: "is_neutered");

            migrationBuilder.RenameColumn(
                name: "address_street",
                table: "pets",
                newName: "pet_health_info");

            migrationBuilder.RenameColumn(
                name: "address_city",
                table: "pets",
                newName: "breed");

            migrationBuilder.AddColumn<string>(
                name: "full_name",
                table: "voluunter",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "status",
                table: "pets",
                type: "integer",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<string>(
                name: "address",
                table: "pets",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "phone_number",
                table: "pets",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "petphotos",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    is_main = table.Column<bool>(type: "boolean", maxLength: 100, nullable: false),
                    path = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
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
                name: "requisiteforhelp",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
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

            migrationBuilder.CreateTable(
                name: "socialmedia",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    path = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
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
                name: "ix_petphotos_pet_id",
                table: "petphotos",
                column: "pet_id");

            migrationBuilder.CreateIndex(
                name: "ix_requisiteforhelp_vol_id",
                table: "requisiteforhelp",
                column: "vol_id");

            migrationBuilder.CreateIndex(
                name: "ix_requisites_pet_id",
                table: "requisites",
                column: "pet_id");

            migrationBuilder.CreateIndex(
                name: "ix_socialmedia_vol_id",
                table: "socialmedia",
                column: "vol_id");
        }
    }
}
