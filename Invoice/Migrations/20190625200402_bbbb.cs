using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Invoice.Migrations
{
    public partial class bbbb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesItems_Nir_NirModelId",
                table: "SalesItems");

            migrationBuilder.DropIndex(
                name: "IX_SalesItems_NirModelId",
                table: "SalesItems");

            migrationBuilder.DropColumn(
                name: "MeasureUnit",
                table: "SalesItems");

            migrationBuilder.DropColumn(
                name: "NirModelId",
                table: "SalesItems");

            migrationBuilder.DropColumn(
                name: "QuantityReceived",
                table: "SalesItems");

            migrationBuilder.CreateTable(
                name: "NirItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Amount = table.Column<double>(nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelete = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    NirId = table.Column<int>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    QuantityReceived = table.Column<int>(nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NirItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NirItems_Nir_NirId",
                        column: x => x.NirId,
                        principalTable: "Nir",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NirItems_NirId",
                table: "NirItems",
                column: "NirId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NirItems");

            migrationBuilder.AddColumn<int>(
                name: "MeasureUnit",
                table: "SalesItems",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NirModelId",
                table: "SalesItems",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "QuantityReceived",
                table: "SalesItems",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SalesItems_NirModelId",
                table: "SalesItems",
                column: "NirModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesItems_Nir_NirModelId",
                table: "SalesItems",
                column: "NirModelId",
                principalTable: "Nir",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
