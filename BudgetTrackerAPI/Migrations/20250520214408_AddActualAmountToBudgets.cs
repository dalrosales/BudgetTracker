using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BudgetTrackerAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddActualAmountToBudgets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "Budgets",
                newName: "BudgetedAmount");

            migrationBuilder.AddColumn<decimal>(
                name: "ActualAmount",
                table: "Budgets",
                type: "decimal(10,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActualAmount",
                table: "Budgets");

            migrationBuilder.RenameColumn(
                name: "BudgetedAmount",
                table: "Budgets",
                newName: "Amount");
        }
    }
}
