using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BootCampNetFullStack.Migrations
{
    /// <inheritdoc />
    public partial class UpdPatientDTO : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
