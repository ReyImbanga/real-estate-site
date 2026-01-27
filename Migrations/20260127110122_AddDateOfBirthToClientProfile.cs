using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstateWeb.Migrations
{
    /// <inheritdoc />
    public partial class AddDateOfBirthToClientProfile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ClientProfiles_EmailAddress",
                table: "ClientProfiles");

            migrationBuilder.DropIndex(
                name: "IX_ClientProfiles_PhoneNumber",
                table: "ClientProfiles");

            migrationBuilder.DropColumn(
                name: "EmailAddress",
                table: "ClientProfiles");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "ClientProfiles");

            migrationBuilder.AddColumn<DateOnly>(
                name: "DateOfBirth",
                table: "ClientProfiles",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "ClientProfiles");

            migrationBuilder.AddColumn<string>(
                name: "EmailAddress",
                table: "ClientProfiles",
                type: "character varying(320)",
                maxLength: 320,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "ClientProfiles",
                type: "character varying(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_ClientProfiles_EmailAddress",
                table: "ClientProfiles",
                column: "EmailAddress",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClientProfiles_PhoneNumber",
                table: "ClientProfiles",
                column: "PhoneNumber",
                unique: true);
        }
    }
}
