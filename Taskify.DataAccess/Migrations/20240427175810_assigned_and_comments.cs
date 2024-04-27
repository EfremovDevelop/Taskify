using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Taskify.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class assigned_and_comments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AssignedUserId",
                table: "Issue",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "IssueCommentEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Comment = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    IssueId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssueCommentEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IssueCommentEntity_Issue_IssueId",
                        column: x => x.IssueId,
                        principalTable: "Issue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IssueCommentEntity_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Issue_AssignedUserId",
                table: "Issue",
                column: "AssignedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_IssueCommentEntity_IssueId",
                table: "IssueCommentEntity",
                column: "IssueId");

            migrationBuilder.CreateIndex(
                name: "IX_IssueCommentEntity_UserId",
                table: "IssueCommentEntity",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Issue_User_AssignedUserId",
                table: "Issue",
                column: "AssignedUserId",
                principalTable: "User",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Issue_User_AssignedUserId",
                table: "Issue");

            migrationBuilder.DropTable(
                name: "IssueCommentEntity");

            migrationBuilder.DropIndex(
                name: "IX_Issue_AssignedUserId",
                table: "Issue");

            migrationBuilder.DropColumn(
                name: "AssignedUserId",
                table: "Issue");
        }
    }
}
