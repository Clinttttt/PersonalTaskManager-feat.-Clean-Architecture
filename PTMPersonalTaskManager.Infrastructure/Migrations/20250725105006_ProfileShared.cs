using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PTMPersonalTaskManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ProfileShared : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProfilesId",
                table: "taskProperties",
                type: "uniqueidentifier",
                nullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_taskProperties_profile_ProfilesId",
                table: "taskProperties");

            migrationBuilder.DropIndex(
                name: "IX_taskProperties_ProfilesId",
                table: "taskProperties");

            migrationBuilder.DropColumn(
                name: "ProfilesId",
                table: "taskProperties");
        }
    }
}
