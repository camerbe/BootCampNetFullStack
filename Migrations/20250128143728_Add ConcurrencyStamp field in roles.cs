using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BootCampNetFullStack.Migrations
{
    /// <inheritdoc />
    public partial class AddConcurrencyStampfieldinroles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("17122a03-38ba-4548-9fcc-b10e45fb8f63"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("5b7a2d91-2250-4169-bea2-8bf0c2010042"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("936633a7-d2d8-49bc-9252-44f861caa6ce"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("e4501398-c6eb-4e5b-ae51-1f633d10aba6"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("04b71631-d442-4413-9974-11cd7b808439"), "42b6ed45-bcb2-4555-b21b-8da405d0496f", "Patient", "PATIENT" },
                    { new Guid("39867fb0-be52-4058-9d1c-2019c2ce9dda"), "39867fb0-be52-4058-9d1c-2019c2ce9dda", "Medecin", "MEDECIN" },
                    { new Guid("3f830736-30d6-4ea9-890b-1ad8389c6e66"), "3f830736-30d6-4ea9-890b-1ad8389c6e66", "Admin", "ADMIN" },
                    { new Guid("5f000235-4cb6-46d3-9c82-fcc37aae7ec0"), "5f000235-4cb6-46d3-9c82-fcc37aae7ec0", "Secretaire", "SECRETAIRE" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("04b71631-d442-4413-9974-11cd7b808439"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("39867fb0-be52-4058-9d1c-2019c2ce9dda"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("3f830736-30d6-4ea9-890b-1ad8389c6e66"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("5f000235-4cb6-46d3-9c82-fcc37aae7ec0"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("17122a03-38ba-4548-9fcc-b10e45fb8f63"), null, "Medecin", "MEDECIN" },
                    { new Guid("5b7a2d91-2250-4169-bea2-8bf0c2010042"), null, "Patient", "PATIENT" },
                    { new Guid("936633a7-d2d8-49bc-9252-44f861caa6ce"), null, "Secretaire", "SECRETAIRE" },
                    { new Guid("e4501398-c6eb-4e5b-ae51-1f633d10aba6"), null, "Admin", "ADMIN" }
                });
        }
    }
}
