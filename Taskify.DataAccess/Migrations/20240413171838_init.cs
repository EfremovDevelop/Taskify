using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Taskify.DataAccess.Migrations;

/// <inheritdoc />
public partial class init : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Permission",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                Name = table.Column<string>(type: "text", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Permission", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Project",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                Description = table.Column<string>(type: "text", nullable: true),
                CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Project", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Role",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                Name = table.Column<string>(type: "text", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Role", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "StatusIssue",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                Name = table.Column<string>(type: "text", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_StatusIssue", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "User",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                Name = table.Column<string>(type: "text", nullable: false),
                Email = table.Column<string>(type: "text", nullable: false),
                Password = table.Column<string>(type: "text", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_User", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "RolePermission",
            columns: table => new
            {
                RoleId = table.Column<int>(type: "integer", nullable: false),
                PermissionId = table.Column<int>(type: "integer", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_RolePermission", x => new { x.RoleId, x.PermissionId });
                table.ForeignKey(
                    name: "FK_RolePermission_Permission_PermissionId",
                    column: x => x.PermissionId,
                    principalTable: "Permission",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_RolePermission_Role_RoleId",
                    column: x => x.RoleId,
                    principalTable: "Role",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Issue",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                Description = table.Column<string>(type: "text", nullable: false),
                TimeSpent = table.Column<float>(type: "real", nullable: false),
                ProjectId = table.Column<Guid>(type: "uuid", nullable: false),
                StatusId = table.Column<int>(type: "integer", nullable: false),
                CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                RefId = table.Column<int>(type: "integer", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Issue", x => x.Id);
                table.ForeignKey(
                    name: "FK_Issue_Project_ProjectId",
                    column: x => x.ProjectId,
                    principalTable: "Project",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_Issue_StatusIssue_StatusId",
                    column: x => x.StatusId,
                    principalTable: "StatusIssue",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "ProjectUser",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                UserId = table.Column<Guid>(type: "uuid", nullable: false),
                ProjectId = table.Column<Guid>(type: "uuid", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ProjectUser", x => x.Id);
                table.ForeignKey(
                    name: "FK_ProjectUser_Project_ProjectId",
                    column: x => x.ProjectId,
                    principalTable: "Project",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_ProjectUser_User_UserId",
                    column: x => x.UserId,
                    principalTable: "User",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "ProjectUserRole",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                ProjectUserId = table.Column<Guid>(type: "uuid", nullable: false),
                RoleId = table.Column<int>(type: "integer", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ProjectUserRole", x => x.Id);
                table.ForeignKey(
                    name: "FK_ProjectUserRole_ProjectUser_ProjectUserId",
                    column: x => x.ProjectUserId,
                    principalTable: "ProjectUser",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_ProjectUserRole_Role_RoleId",
                    column: x => x.RoleId,
                    principalTable: "Role",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.InsertData(
            table: "Permission",
            columns: new[] { "Id", "Name" },
            values: new object[,]
            {
                { 1, "Create" },
                { 2, "Read" },
                { 3, "Update" },
                { 4, "Delete" }
            });

        migrationBuilder.InsertData(
            table: "Role",
            columns: new[] { "Id", "Name" },
            values: new object[,]
            {
                { 1, "Admin" },
                { 2, "User" }
            });

        migrationBuilder.InsertData(
            table: "StatusIssue",
            columns: new[] { "Id", "Name" },
            values: new object[,]
            {
                { 1, "New" },
                { 2, "Assigned" },
                { 3, "Review" },
                { 4, "Reopened" },
                { 5, "Closed" }
            });

        migrationBuilder.InsertData(
            table: "RolePermission",
            columns: new[] { "PermissionId", "RoleId" },
            values: new object[,]
            {
                { 1, 1 },
                { 2, 1 },
                { 3, 1 },
                { 4, 1 },
                { 2, 2 },
                { 3, 2 }
            });

        migrationBuilder.CreateIndex(
            name: "IX_Issue_ProjectId",
            table: "Issue",
            column: "ProjectId");

        migrationBuilder.CreateIndex(
            name: "IX_Issue_StatusId",
            table: "Issue",
            column: "StatusId");

        migrationBuilder.CreateIndex(
            name: "IX_ProjectUser_ProjectId",
            table: "ProjectUser",
            column: "ProjectId");

        migrationBuilder.CreateIndex(
            name: "IX_ProjectUser_UserId",
            table: "ProjectUser",
            column: "UserId");

        migrationBuilder.CreateIndex(
            name: "IX_ProjectUserRole_ProjectUserId",
            table: "ProjectUserRole",
            column: "ProjectUserId");

        migrationBuilder.CreateIndex(
            name: "IX_ProjectUserRole_RoleId",
            table: "ProjectUserRole",
            column: "RoleId");

        migrationBuilder.CreateIndex(
            name: "IX_RolePermission_PermissionId",
            table: "RolePermission",
            column: "PermissionId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Issue");

        migrationBuilder.DropTable(
            name: "ProjectUserRole");

        migrationBuilder.DropTable(
            name: "RolePermission");

        migrationBuilder.DropTable(
            name: "StatusIssue");

        migrationBuilder.DropTable(
            name: "ProjectUser");

        migrationBuilder.DropTable(
            name: "Permission");

        migrationBuilder.DropTable(
            name: "Role");

        migrationBuilder.DropTable(
            name: "Project");

        migrationBuilder.DropTable(
            name: "User");
    }
}
