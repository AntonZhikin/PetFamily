using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetFamily.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class More : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReqListDetails",
                table: "voluunter");

            migrationBuilder.DropColumn(
                name: "SocDetails",
                table: "voluunter");

            migrationBuilder.AddColumn<string>(
                name: "AssistanceDetails",
                table: "voluunter",
                type: "jsonb",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SocialNetworks",
                table: "voluunter",
                type: "jsonb",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssistanceDetails",
                table: "voluunter");

            migrationBuilder.DropColumn(
                name: "SocialNetworks",
                table: "voluunter");

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
        }
    }
}
