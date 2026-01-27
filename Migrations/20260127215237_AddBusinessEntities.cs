using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace RealEstateWeb.Migrations
{
    /// <inheritdoc />
    public partial class AddBusinessEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContractStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Inspections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PropertyId = table.Column<int>(type: "integer", nullable: false),
                    ResponsibleOwnerId = table.Column<int>(type: "integer", nullable: false),
                    InspectionDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Notes = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inspections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Inspections_OwnerProfiles_ResponsibleOwnerId",
                        column: x => x.ResponsibleOwnerId,
                        principalTable: "OwnerProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Inspections_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OfferStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfferStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contracts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PropertyId = table.Column<int>(type: "integer", nullable: false),
                    ListingId = table.Column<int>(type: "integer", nullable: false),
                    ClientProfileId = table.Column<int>(type: "integer", nullable: false),
                    ResponsibleOwnerId = table.Column<int>(type: "integer", nullable: false),
                    ContractStatusId = table.Column<int>(type: "integer", nullable: false),
                    SignedDate = table.Column<DateOnly>(type: "date", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: true),
                    ContractDocumentPath = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contracts_ClientProfiles_ClientProfileId",
                        column: x => x.ClientProfileId,
                        principalTable: "ClientProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contracts_ContractStatuses_ContractStatusId",
                        column: x => x.ContractStatusId,
                        principalTable: "ContractStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contracts_Listings_ListingId",
                        column: x => x.ListingId,
                        principalTable: "Listings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contracts_OwnerProfiles_ResponsibleOwnerId",
                        column: x => x.ResponsibleOwnerId,
                        principalTable: "OwnerProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contracts_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientInspections",
                columns: table => new
                {
                    ClientProfileId = table.Column<int>(type: "integer", nullable: false),
                    InspectionId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientInspections", x => new { x.ClientProfileId, x.InspectionId });
                    table.ForeignKey(
                        name: "FK_ClientInspections_ClientProfiles_ClientProfileId",
                        column: x => x.ClientProfileId,
                        principalTable: "ClientProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientInspections_Inspections_InspectionId",
                        column: x => x.InspectionId,
                        principalTable: "Inspections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Offers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PropertyId = table.Column<int>(type: "integer", nullable: false),
                    ClientProfileId = table.Column<int>(type: "integer", nullable: false),
                    OfferStatusId = table.Column<int>(type: "integer", nullable: false),
                    OfferAmount = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Offers_ClientProfiles_ClientProfileId",
                        column: x => x.ClientProfileId,
                        principalTable: "ClientProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Offers_OfferStatuses_OfferStatusId",
                        column: x => x.OfferStatusId,
                        principalTable: "OfferStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Offers_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ownerships",
                columns: table => new
                {
                    PropertyId = table.Column<int>(type: "integer", nullable: false),
                    OwnerProfileId = table.Column<int>(type: "integer", nullable: false),
                    RoleTypeId = table.Column<int>(type: "integer", nullable: false),
                    SharePercent = table.Column<decimal>(type: "numeric", nullable: false),
                    IsPrimary = table.Column<bool>(type: "boolean", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ownerships", x => new { x.PropertyId, x.OwnerProfileId });
                    table.ForeignKey(
                        name: "FK_Ownerships_OwnerProfiles_OwnerProfileId",
                        column: x => x.OwnerProfileId,
                        principalTable: "OwnerProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ownerships_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ownerships_RoleTypes_RoleTypeId",
                        column: x => x.RoleTypeId,
                        principalTable: "RoleTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClientInspections_InspectionId",
                table: "ClientInspections",
                column: "InspectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_ClientProfileId",
                table: "Contracts",
                column: "ClientProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_ContractStatusId",
                table: "Contracts",
                column: "ContractStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_ListingId",
                table: "Contracts",
                column: "ListingId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_PropertyId",
                table: "Contracts",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_ResponsibleOwnerId",
                table: "Contracts",
                column: "ResponsibleOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Inspections_PropertyId",
                table: "Inspections",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_Inspections_ResponsibleOwnerId",
                table: "Inspections",
                column: "ResponsibleOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_ClientProfileId",
                table: "Offers",
                column: "ClientProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_OfferStatusId",
                table: "Offers",
                column: "OfferStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_PropertyId",
                table: "Offers",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_Ownerships_OwnerProfileId",
                table: "Ownerships",
                column: "OwnerProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Ownerships_RoleTypeId",
                table: "Ownerships",
                column: "RoleTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientInspections");

            migrationBuilder.DropTable(
                name: "Contracts");

            migrationBuilder.DropTable(
                name: "Offers");

            migrationBuilder.DropTable(
                name: "Ownerships");

            migrationBuilder.DropTable(
                name: "Inspections");

            migrationBuilder.DropTable(
                name: "ContractStatuses");

            migrationBuilder.DropTable(
                name: "OfferStatuses");

            migrationBuilder.DropTable(
                name: "RoleTypes");
        }
    }
}
