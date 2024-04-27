using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Taskify.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class clear_entity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IssueCommentEntity_Issue_IssueId",
                table: "IssueCommentEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_IssueCommentEntity_User_UserId",
                table: "IssueCommentEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IssueCommentEntity",
                table: "IssueCommentEntity");

            migrationBuilder.RenameTable(
                name: "IssueCommentEntity",
                newName: "IssueComment");

            migrationBuilder.RenameIndex(
                name: "IX_IssueCommentEntity_UserId",
                table: "IssueComment",
                newName: "IX_IssueComment_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_IssueCommentEntity_IssueId",
                table: "IssueComment",
                newName: "IX_IssueComment_IssueId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IssueComment",
                table: "IssueComment",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IssueComment_Issue_IssueId",
                table: "IssueComment",
                column: "IssueId",
                principalTable: "Issue",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IssueComment_User_UserId",
                table: "IssueComment",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IssueComment_Issue_IssueId",
                table: "IssueComment");

            migrationBuilder.DropForeignKey(
                name: "FK_IssueComment_User_UserId",
                table: "IssueComment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IssueComment",
                table: "IssueComment");

            migrationBuilder.RenameTable(
                name: "IssueComment",
                newName: "IssueCommentEntity");

            migrationBuilder.RenameIndex(
                name: "IX_IssueComment_UserId",
                table: "IssueCommentEntity",
                newName: "IX_IssueCommentEntity_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_IssueComment_IssueId",
                table: "IssueCommentEntity",
                newName: "IX_IssueCommentEntity_IssueId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IssueCommentEntity",
                table: "IssueCommentEntity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IssueCommentEntity_Issue_IssueId",
                table: "IssueCommentEntity",
                column: "IssueId",
                principalTable: "Issue",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IssueCommentEntity_User_UserId",
                table: "IssueCommentEntity",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
