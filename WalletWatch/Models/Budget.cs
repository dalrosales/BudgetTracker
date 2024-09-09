namespace WalletWatch.Models
{
    public class Budget
    {
        public int BudgetId { get; set; }
        public string? BudgetName { get; set; }
        public List<BudgetCategories>? Category {  get; set; }
    }
}
