using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BootCampNetFullStack.Migrations
{
    /// <inheritdoc />
    public partial class CreateCrenauHoraireMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CrenauxHoraire",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Debut = table.Column<TimeSpan>(type: "time", nullable: false),
                    Fin = table.Column<TimeSpan>(type: "time", nullable: false),
                    MedecinId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CrenauxHoraire", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CrenauxHoraire_Medecins_MedecinId",
                        column: x => x.MedecinId,
                        principalTable: "Medecins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CrenauxHoraire_MedecinId",
                table: "CrenauxHoraire",
                column: "MedecinId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CrenauxHoraire");
        }
    }
}
