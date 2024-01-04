namespace BudgetTrackerAPI.Models
{
    public class Income
    {
        public int IncomeId { get; set; }
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public DateTime DateOfIncome { get; set; }

        // Navigation property
        public User User { get; set; }
    }

}
