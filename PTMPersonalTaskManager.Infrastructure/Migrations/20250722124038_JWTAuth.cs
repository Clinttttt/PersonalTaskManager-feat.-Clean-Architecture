using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PTMPersonalTaskManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class JWTAuth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ExpiredRefreshToken",
                table: "taskProperties",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "taskProperties",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "taskProperties",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "String",
                table: "taskProperties",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "taskProperties",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpiredRefreshToken",
                table: "taskProperties");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "taskProperties");

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "taskProperties");

            migrationBuilder.DropColumn(
                name: "String",
                table: "taskProperties");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "taskProperties");
        }
    }
}
