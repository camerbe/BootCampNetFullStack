using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BootCampNetFullStack.Migrations
{
    /// <inheritdoc />
    public partial class AddConstraintToRDV : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedecinResponseDTO_AspNetUsers_UserId",
                table: "MedecinResponseDTO");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("0b128172-10fc-4e12-bb13-e100c5716ba3"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("1edeb8db-be10-42d1-af85-d90d3e0d3dce"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("60729606-5f65-4b75-9690-4822fc3f5e05"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("cfc93af6-26ba-45cd-b2fc-b4dc7cf9a04b"));

            migrationBuilder.CreateTable(
                name: "UserDTO",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prenom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Tel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDTO", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("081c6fc9-d07f-493b-8dd4-2f2a1acc8245"), "081c6fc9-d07f-493b-8dd4-2f2a1acc8245", "Admin", "ADMIN" },
                    { new Guid("57560b62-acbc-42a8-b6d4-f13945ab98df"), "57560b62-acbc-42a8-b6d4-f13945ab98df", "Patient", "PATIENT" },
                    { new Guid("afcdc060-b9fd-4395-bbc1-a9da843cb7b2"), "afcdc060-b9fd-4395-bbc1-a9da843cb7b2", "Secretaire", "SECRETAIRE" },
                    { new Guid("c134c18c-22cf-48c2-affa-0c16cbcf14dd"), "c134c18c-22cf-48c2-affa-0c16cbcf14dd", "Medecin", "MEDECIN" }
                });

            migrationBuilder.AddCheckConstraint(
                name: "CK_RendezVous_DateRdv",
                table: "RendezVous",
                sql: "DateRdv > SYSDATETIME()");

            migrationBuilder.AddForeignKey(
                name: "FK_MedecinResponseDTO_UserDTO_UserId",
                table: "MedecinResponseDTO",
                column: "UserId",
                principalTable: "UserDTO",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedecinResponseDTO_UserDTO_UserId",
                table: "MedecinResponseDTO");

            migrationBuilder.DropTable(
                name: "UserDTO");

            migrationBuilder.DropCheckConstraint(
                name: "CK_RendezVous_DateRdv",
                table: "RendezVous");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("081c6fc9-d07f-493b-8dd4-2f2a1acc8245"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("57560b62-acbc-42a8-b6d4-f13945ab98df"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("afcdc060-b9fd-4395-bbc1-a9da843cb7b2"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("c134c18c-22cf-48c2-affa-0c16cbcf14dd"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("0b128172-10fc-4e12-bb13-e100c5716ba3"), "0b128172-10fc-4e12-bb13-e100c5716ba3", "Secretaire", "SECRETAIRE" },
                    { new Guid("1edeb8db-be10-42d1-af85-d90d3e0d3dce"), "1edeb8db-be10-42d1-af85-d90d3e0d3dce", "Admin", "ADMIN" },
                    { new Guid("60729606-5f65-4b75-9690-4822fc3f5e05"), "60729606-5f65-4b75-9690-4822fc3f5e05", "Patient", "PATIENT" },
                    { new Guid("cfc93af6-26ba-45cd-b2fc-b4dc7cf9a04b"), "cfc93af6-26ba-45cd-b2fc-b4dc7cf9a04b", "Medecin", "MEDECIN" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_MedecinResponseDTO_AspNetUsers_UserId",
                table: "MedecinResponseDTO",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
