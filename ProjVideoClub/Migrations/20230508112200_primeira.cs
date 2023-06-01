using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjVideoClub.Migrations
{
    public partial class primeira : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TCategorias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeCat = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TCategorias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TUtilizadores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TUtilizadores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TFilmes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ano = table.Column<int>(type: "int", nullable: false),
                    CategoriaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TFilmes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TFilmes_TCategorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "TCategorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TRegistoAlugueres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FilmeId = table.Column<int>(type: "int", nullable: false),
                    UtilizadorId = table.Column<int>(type: "int", nullable: false),
                    DataInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataFim = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TRegistoAlugueres", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TRegistoAlugueres_TFilmes_FilmeId",
                        column: x => x.FilmeId,
                        principalTable: "TFilmes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TRegistoAlugueres_TUtilizadores_UtilizadorId",
                        column: x => x.UtilizadorId,
                        principalTable: "TUtilizadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TFilmes_CategoriaId",
                table: "TFilmes",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_TRegistoAlugueres_FilmeId",
                table: "TRegistoAlugueres",
                column: "FilmeId");

            migrationBuilder.CreateIndex(
                name: "IX_TRegistoAlugueres_UtilizadorId",
                table: "TRegistoAlugueres",
                column: "UtilizadorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TRegistoAlugueres");

            migrationBuilder.DropTable(
                name: "TFilmes");

            migrationBuilder.DropTable(
                name: "TUtilizadores");

            migrationBuilder.DropTable(
                name: "TCategorias");
        }
    }
}
