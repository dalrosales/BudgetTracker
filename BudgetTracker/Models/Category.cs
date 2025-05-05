using System.Xml;

namespace BudgetTracker.Models
{
    public class Category
    {
        public required UniqueId CategoryId { get; set; }
        public required UniqueId BudgetId { get; set; }
        public required UniqueId UserId { get; set; }
        public required string Name { get; set; }
        public decimal BudgetedAmount { get; set; }
        public decimal ActualAmount { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
