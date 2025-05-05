using System.Xml;

namespace BudgetTracker.Models
{
    public class Budget
    {
        public required UniqueId BudgetId { get; set; }
        public required UniqueId UserId { get; set; }
        public List<Category>? Categories { get; set; }
        public required string Name { get; set; }
        public decimal Amount { get; set; }
        public string? Period { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
