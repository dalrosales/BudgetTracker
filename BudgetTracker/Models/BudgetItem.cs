namespace BudgetTracker.Models
{
    public class BudgetItem
    {
        public string Category { get; set; } = string.Empty;
        public decimal BudgetedAmount { get; set; }
        public decimal ActualAmount { get; set; }
    }
}
