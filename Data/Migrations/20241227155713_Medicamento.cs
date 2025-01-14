using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SGH2425_V3.Data.Migrations
{
    /// <inheritdoc />
    public partial class Medicamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TipoMedicamento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Forma_Farmaceutica = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoMedicamento", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Medicamento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome_medicamento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dosagem = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lote = table.Column<int>(type: "int", nullable: false),
                    Localicao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    stock = table.Column<int>(type: "int", nullable: false),
                    Stock_Min = table.Column<int>(type: "int", nullable: false),
                    Data_Producao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Data_Validade = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TipoMedicamentoId = table.Column<int>(type: "int", nullable: false),
                    RegistradoPorId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicamento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Medicamento_AspNetUsers_RegistradoPorId",
                        column: x => x.RegistradoPorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Medicamento_TipoMedicamento_TipoMedicamentoId",
                        column: x => x.TipoMedicamentoId,
                        principalTable: "TipoMedicamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Medicamento_RegistradoPorId",
                table: "Medicamento",
                column: "RegistradoPorId");

            migrationBuilder.CreateIndex(
                name: "IX_Medicamento_TipoMedicamentoId",
                table: "Medicamento",
                column: "TipoMedicamentoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Medicamento");

            migrationBuilder.DropTable(
                name: "TipoMedicamento");
        }
    }
}
