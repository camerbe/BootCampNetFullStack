using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BootCampNetFullStack.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSpecialiteMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Medecins_SpecialiteId",
                table: "Medecins",
                column: "SpecialiteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Medecins_Specialites_SpecialiteId",
                table: "Medecins",
                column: "SpecialiteId",
                principalTable: "Specialites",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medecins_Specialites_SpecialiteId",
                table: "Medecins");

            migrationBuilder.DropIndex(
                name: "IX_Medecins_SpecialiteId",
                table: "Medecins");
        }
    }
}
