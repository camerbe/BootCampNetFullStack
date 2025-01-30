using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BootCampNetFullStack.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("13e493de-1070-4d6d-a900-f080bd7d8324"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("3f4074b6-670b-4131-91eb-c35e0bdbfae8"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("76574bc6-100f-4aaa-8116-393cdee3f866"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("9ae71992-233c-4ead-8136-dc4d20f04678"));

            migrationBuilder.DropColumn(
                name: "Role",
                table: "AspNetUsers");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("13e493de-1070-4d6d-a900-f080bd7d8324"), null, "Patient", "PATIENT" },
                    { new Guid("3f4074b6-670b-4131-91eb-c35e0bdbfae8"), null, "Secretaire", "SECRETAIRE" },
                    { new Guid("76574bc6-100f-4aaa-8116-393cdee3f866"), null, "Medecin", "MEDECIN" },
                    { new Guid("9ae71992-233c-4ead-8136-dc4d20f04678"), null, "Admin", "ADMIN" }
                });
        }
    }
}
