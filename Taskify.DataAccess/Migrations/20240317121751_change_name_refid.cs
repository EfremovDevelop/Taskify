using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Taskify.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class change_name_refid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RefId",
                table: "Issue",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefId",
                table: "Issue");
        }
    }
}
