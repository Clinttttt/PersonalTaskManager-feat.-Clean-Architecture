using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PTMPersonalTaskManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class changes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "String",
                table: "taskProperties");

            migrationBuilder.RenameColumn(
                name: "StartData",
                table: "taskProperties",
                newName: "StartDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "taskProperties",
                newName: "StartData");

            migrationBuilder.AddColumn<string>(
                name: "String",
                table: "taskProperties",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
