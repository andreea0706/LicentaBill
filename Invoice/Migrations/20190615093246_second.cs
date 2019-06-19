using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Invoice.Migrations
{
    public partial class second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 5);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "ConfirmPassword",
                table: "Users",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirmName",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "cui",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "scpTVA",
                table: "Users",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "FirmName",
                table: "Customer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "cui",
                table: "Customer",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "scpTVA",
                table: "Customer",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "FirmName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "cui",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "scpTVA",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "FirmName",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "cui",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "scpTVA",
                table: "Customer");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                maxLength: 5,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ConfirmPassword",
                table: "Users",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
