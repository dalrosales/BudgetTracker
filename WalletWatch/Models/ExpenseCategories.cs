namespace WalletWatch.Models
{
    public class ExpenseCategories
    {
        public int ExpenseCategoryId { get; set; }
        public string? CategoryName { get; set; }
        public List<Expense>? Expenses { get; set; }
    }
}
