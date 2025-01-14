using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ApiControleFinanceiro.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InstituicaoFinanceiras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodigoInstituicao = table.Column<int>(type: "int", nullable: false),
                    NomeInstituicao = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstituicaoFinanceiras", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MeioDePagamentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeioDePagamentos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoDeContas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoConta = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoDeContas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoMovimentacoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MovimentaSaldo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoMovimentacoes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Saldo = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    UltimaAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InstituicaoId = table.Column<int>(type: "int", nullable: false),
                    TipoContaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contas_InstituicaoFinanceiras_InstituicaoId",
                        column: x => x.InstituicaoId,
                        principalTable: "InstituicaoFinanceiras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contas_TipoDeContas_TipoContaId",
                        column: x => x.TipoContaId,
                        principalTable: "TipoDeContas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Movimentacoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    DataPagamento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataMovimentacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TipoMovimentoId = table.Column<int>(type: "int", nullable: false),
                    MeioPagamentoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movimentacoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Movimentacoes_MeioDePagamentos_MeioPagamentoId",
                        column: x => x.MeioPagamentoId,
                        principalTable: "MeioDePagamentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Movimentacoes_TipoMovimentacoes_TipoMovimentoId",
                        column: x => x.TipoMovimentoId,
                        principalTable: "TipoMovimentacoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "InstituicaoFinanceiras",
                columns: new[] { "Id", "CodigoInstituicao", "NomeInstituicao" },
                values: new object[,]
                {
                    { 1, 341, "Itaú" },
                    { 2, 104, "Caixa" },
                    { 3, 260, "Nubank" },
                    { 4, 77, "Inter" },
                    { 5, 0, "PeerBR" }
                });

            migrationBuilder.InsertData(
                table: "TipoDeContas",
                columns: new[] { "Id", "TipoConta" },
                values: new object[,]
                {
                    { 1, "Conta Corrente" },
                    { 2, "Poupança" },
                    { 3, "Investimento" }
                });

            migrationBuilder.InsertData(
                table: "TipoMovimentacoes",
                columns: new[] { "Id", "Descricao", "MovimentaSaldo" },
                values: new object[,]
                {
                    { 1, "Crédito", true },
                    { 2, "Débito", true },
                    { 3, "Transferência", false }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contas_InstituicaoId",
                table: "Contas",
                column: "InstituicaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Contas_TipoContaId",
                table: "Contas",
                column: "TipoContaId");

            migrationBuilder.CreateIndex(
                name: "IX_Movimentacoes_MeioPagamentoId",
                table: "Movimentacoes",
                column: "MeioPagamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Movimentacoes_TipoMovimentoId",
                table: "Movimentacoes",
                column: "TipoMovimentoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contas");

            migrationBuilder.DropTable(
                name: "Movimentacoes");

            migrationBuilder.DropTable(
                name: "InstituicaoFinanceiras");

            migrationBuilder.DropTable(
                name: "TipoDeContas");

            migrationBuilder.DropTable(
                name: "MeioDePagamentos");

            migrationBuilder.DropTable(
                name: "TipoMovimentacoes");
        }
    }
}
