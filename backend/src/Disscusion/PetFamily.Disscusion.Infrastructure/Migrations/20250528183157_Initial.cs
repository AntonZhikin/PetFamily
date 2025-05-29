using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetFamily.Disscusion.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "discussions");

            migrationBuilder.CreateTable(
                name: "discussion",
                schema: "discussions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    request_id = table.Column<Guid>(type: "uuid", nullable: false),
                    status = table.Column<string>(type: "text", nullable: false),
                    applicant_user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    reviewing_user_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_discussion", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "message",
                schema: "discussions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    sender_id = table.Column<Guid>(type: "uuid", nullable: false),
                    message_content = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    discussion_id = table.Column<Guid>(type: "uuid", nullable: false),
                    is_edited = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_message", x => x.id);
                    table.ForeignKey(
                        name: "fk_message_discussions_discussion_id",
                        column: x => x.discussion_id,
                        principalSchema: "discussions",
                        principalTable: "discussion",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_message_discussion_id",
                schema: "discussions",
                table: "message",
                column: "discussion_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "message",
                schema: "discussions");

            migrationBuilder.DropTable(
                name: "discussion",
                schema: "discussions");
        }
    }
}
