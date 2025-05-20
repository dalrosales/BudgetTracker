namespace BudgetTracker.Models.DTOs
{
    public class CreateBudgetDto
    {
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public string Period { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
