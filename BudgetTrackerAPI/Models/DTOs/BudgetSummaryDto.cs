namespace BudgetTrackerAPI.Models.DTOs
{
    public class BudgetSummaryDto
    {
        public Guid BudgetId { get; set; }
        public string Name { get; set; }
        public decimal BudgetedAmount { get; set; }
        public decimal ActualAmount { get; set; }
        public string Period { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
