using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BootCampNetFullStack.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCrenauHoraireMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CrenauxHoraire_Medecins_MedecinId",
                table: "CrenauxHoraire");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CrenauxHoraire",
                table: "CrenauxHoraire");

            migrationBuilder.DropIndex(
                name: "IX_CrenauxHoraire_MedecinId",
                table: "CrenauxHoraire");

            migrationBuilder.RenameTable(
                name: "CrenauxHoraire",
                newName: "CrenauxHoraires");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CrenauxHoraires",
                table: "CrenauxHoraires",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_CrenauxHoraires_MedecinId",
                table: "CrenauxHoraires",
                column: "MedecinId");

            migrationBuilder.AddForeignKey(
                name: "FK_CrenauxHoraires_Medecins_MedecinId",
                table: "CrenauxHoraires",
                column: "MedecinId",
                principalTable: "Medecins",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CrenauxHoraires_Medecins_MedecinId",
                table: "CrenauxHoraires");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CrenauxHoraires",
                table: "CrenauxHoraires");

            migrationBuilder.DropIndex(
                name: "IX_CrenauxHoraires_MedecinId",
                table: "CrenauxHoraires");

            migrationBuilder.RenameTable(
                name: "CrenauxHoraires",
                newName: "CrenauxHoraire");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CrenauxHoraire",
                table: "CrenauxHoraire",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_CrenauxHoraire_MedecinId",
                table: "CrenauxHoraire",
                column: "MedecinId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CrenauxHoraire_Medecins_MedecinId",
                table: "CrenauxHoraire",
                column: "MedecinId",
                principalTable: "Medecins",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
