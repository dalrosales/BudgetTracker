namespace WalletWatch.Models
{
    public class BudgetCategories
    {
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public float AmountBudgeted { get; set; }
        public float AmountExpensed { get; set; }
    }
}
