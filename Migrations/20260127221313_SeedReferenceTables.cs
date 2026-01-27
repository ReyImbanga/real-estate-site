using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RealEstateWeb.Migrations
{
    /// <inheritdoc />
    public partial class SeedReferenceTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ContractStatuses",
                columns: new[] { "Id", "Code", "Description" },
                values: new object[,]
                {
                    { 1, "DRAFT", "Brouillon" },
                    { 2, "SIGNED", "Signé" },
                    { 3, "ACTIVE", "Actif" },
                    { 4, "COMPLETED", "Terminé" },
                    { 5, "CANCELLED", "Annulé" }
                });

            migrationBuilder.InsertData(
                table: "OfferStatuses",
                columns: new[] { "Id", "Code", "Description" },
                values: new object[,]
                {
                    { 1, "PENDING", "En attente" },
                    { 2, "ACCEPTED", "Acceptée" },
                    { 3, "REJECTED", "Refusée" },
                    { 4, "CANCELLED", "Annulée" }
                });

            migrationBuilder.InsertData(
                table: "RoleTypes",
                columns: new[] { "Id", "Description" },
                values: new object[,]
                {
                    { 1, "Propriétaire principal" },
                    { 2, "Co-propriétaire" },
                    { 3, "Agent immobilier" },
                    { 4, "Gestionnaire" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ContractStatuses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ContractStatuses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ContractStatuses",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ContractStatuses",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ContractStatuses",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "OfferStatuses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "OfferStatuses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "OfferStatuses",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "OfferStatuses",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "RoleTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "RoleTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "RoleTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "RoleTypes",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
