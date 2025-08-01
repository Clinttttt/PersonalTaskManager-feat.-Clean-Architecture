using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PTMPersonalTaskManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class User : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_taskProperties_profile_ProfilesId",
                table: "taskProperties");

            migrationBuilder.DropIndex(
                name: "IX_taskProperties_ProfilesId",
                table: "taskProperties");

            migrationBuilder.DropColumn(
                name: "ExpiredRefreshToken",
                table: "taskProperties");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "taskProperties");

            migrationBuilder.DropColumn(
                name: "ProfilesId",
                table: "taskProperties");

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "taskProperties");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "taskProperties");

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpiredRefreshToken = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user");

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
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "ProfilesId",
                table: "taskProperties",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "taskProperties",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "taskProperties",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_taskProperties_ProfilesId",
                table: "taskProperties",
                column: "ProfilesId");

            migrationBuilder.AddForeignKey(
                name: "FK_taskProperties_profile_ProfilesId",
                table: "taskProperties",
                column: "ProfilesId",
                principalTable: "profile",
                principalColumn: "Id");
        }
    }
}
