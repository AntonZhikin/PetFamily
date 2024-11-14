using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetFamily.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NewPC : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "count_pet_found_home",
                table: "voluunter");

            migrationBuilder.DropColumn(
                name: "count_pet_healing",
                table: "voluunter");

            migrationBuilder.DropColumn(
                name: "count_pet_in_home",
                table: "voluunter");

            migrationBuilder.DropColumn(
                name: "descriptions",
                table: "voluunter");

            migrationBuilder.DropColumn(
                name: "experience_years",
                table: "voluunter");

            migrationBuilder.DropColumn(
                name: "phone_number",
                table: "voluunter");

            migrationBuilder.DropColumn(
                name: "status",
                table: "pets");

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "voluunter",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "experienceyears",
                table: "voluunter",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "phonenumber",
                table: "voluunter",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "help_status",
                table: "pets",
                type: "integer",
                maxLength: 100,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "volunteer_id",
                table: "pets",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_pets_volunteer_id",
                table: "pets",
                column: "volunteer_id");

            migrationBuilder.AddForeignKey(
                name: "fk_pets_voluunter_volunteer_id",
                table: "pets",
                column: "volunteer_id",
                principalTable: "voluunter",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_pets_voluunter_volunteer_id",
                table: "pets");

            migrationBuilder.DropIndex(
                name: "ix_pets_volunteer_id",
                table: "pets");

            migrationBuilder.DropColumn(
                name: "description",
                table: "voluunter");

            migrationBuilder.DropColumn(
                name: "experienceyears",
                table: "voluunter");

            migrationBuilder.DropColumn(
                name: "phonenumber",
                table: "voluunter");

            migrationBuilder.DropColumn(
                name: "help_status",
                table: "pets");

            migrationBuilder.DropColumn(
                name: "volunteer_id",
                table: "pets");

            migrationBuilder.AddColumn<int>(
                name: "count_pet_found_home",
                table: "voluunter",
                type: "integer",
                maxLength: 100,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "count_pet_healing",
                table: "voluunter",
                type: "integer",
                maxLength: 100,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "count_pet_in_home",
                table: "voluunter",
                type: "integer",
                maxLength: 100,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "descriptions",
                table: "voluunter",
                type: "character varying(2000)",
                maxLength: 2000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "experience_years",
                table: "voluunter",
                type: "integer",
                maxLength: 100,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "phone_number",
                table: "voluunter",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "status",
                table: "pets",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}
