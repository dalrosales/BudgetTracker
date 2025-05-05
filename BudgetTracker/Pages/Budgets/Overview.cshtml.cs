using BudgetTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BudgetTracker.Pages.Budgets.Overview
{
    public class BudgetsModel : PageModel
    {
        private readonly ILogger<BudgetsModel> _logger;
        public List<BudgetItem> Budgets { get; set; } = new();

        public BudgetsModel(ILogger<BudgetsModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            // Sample Data
            Budgets = new List<BudgetItem>
            {
                new() { Category = "Groceries", BudgetedAmount = 500, ActualAmount = 450 },
                new() { Category = "Entertainment", BudgetedAmount = 200, ActualAmount = 250 },
                new() { Category = "Rent", BudgetedAmount = 1500, ActualAmount = 1500 }
            };
        }
    }
}
