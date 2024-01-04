namespace BudgetTrackerAPI.Models
{
    public class ExpenseTag
    {
        public int ExpenseId { get; set; }
        public int TagId { get; set; }

        // Navigation properties
        public Expense Expense { get; set; }
        public Tag Tag { get; set; }
    }
}
