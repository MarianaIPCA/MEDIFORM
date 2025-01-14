using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SGH2425_V3.Data.Migrations
{
    /// <inheritdoc />
    public partial class Utente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AlergiaUtente",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo_Alergia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Data_Ultima_Alergia = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlergiaUtente", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Utente",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NU = table.Column<int>(type: "int", nullable: false),
                    Genero = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Data_Nascimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Peso = table.Column<int>(type: "int", nullable: false),
                    Telemovel = table.Column<int>(type: "int", nullable: false),
                    AlergiaUtenteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utente", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Utente_AlergiaUtente_AlergiaUtenteId",
                        column: x => x.AlergiaUtenteId,
                        principalTable: "AlergiaUtente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Utente_AlergiaUtenteId",
                table: "Utente",
                column: "AlergiaUtenteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Utente");

            migrationBuilder.DropTable(
                name: "AlergiaUtente");
        }
    }
}
