using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Invoice.Migrations
{
    public partial class nirs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "Nir",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Discount = table.Column<double>(nullable: true),
                    GrandTotal = table.Column<double>(nullable: false),
                    IsDelete = table.Column<bool>(nullable: false),
                    NirCode = table.Column<string>(nullable: true),
                    NirDate = table.Column<DateTime>(nullable: false),
                    Notes = table.Column<string>(nullable: true),
                    PaymentMethod = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    SupplierId = table.Column<int>(nullable: false),
                    Total = table.Column<double>(nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nir", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Nir_Supplier_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Supplier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SalesItems_NirModelId",
                table: "SalesItems",
                column: "NirModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Nir_SupplierId",
                table: "Nir",
                column: "SupplierId");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesItems_Nir_NirModelId",
                table: "SalesItems",
                column: "NirModelId",
                principalTable: "Nir",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesItems_Nir_NirModelId",
                table: "SalesItems");

            migrationBuilder.DropTable(
                name: "Nir");

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
        }
    }
}
