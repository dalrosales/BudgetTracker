using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BudgetTrackerAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddBudgetCategoryRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ActualAmount",
                table: "Categories",
                type: "decimal(10,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<Guid>(
                name: "BudgetID",
                table: "Categories",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<decimal>(
                name: "BudgetedAmount",
                table: "Categories",
                type: "decimal(10,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_BudgetID",
                table: "Categories",
                column: "BudgetID");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Budgets",
                table: "Categories",
                column: "BudgetID",
                principalTable: "Budgets",
                principalColumn: "BudgetID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Budgets",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_BudgetID",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "ActualAmount",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "BudgetID",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "BudgetedAmount",
                table: "Categories");
        }
    }
}
