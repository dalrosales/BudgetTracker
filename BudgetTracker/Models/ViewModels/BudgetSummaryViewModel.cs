namespace BudgetTracker.Models.ViewModels
{
    public class BudgetSummaryViewModel
    {
        public Guid BudgetId { get; set; }
        public string Name { get; set; }
        public decimal Budgeted { get; set; }
        public decimal Actual { get; set; }
        public decimal Difference => Budgeted - Actual;
    }
}
