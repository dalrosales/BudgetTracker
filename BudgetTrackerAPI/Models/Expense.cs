namespace BudgetTrackerAPI.Models
{
    public class Expense
    {
        public int ExpenseId { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public DateTime DateOfExpense { get; set; }

        // Navigation properties
        public User User { get; set; }
        public Category Category { get; set; }
        public List<ExpenseTag> ExpenseTags { get; set; }
    }

}
