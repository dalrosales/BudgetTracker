#nullable disable

namespace BudgetTrackerAPI.Models;

public partial class Budget
{
    public Guid BudgetId { get; set; }
    public string UserId { get; set; }
    public string Name { get; set; }
    public decimal BudgetedAmount { get; set; }
    public decimal ActualAmount { get; set; } = 0;
    public string Period { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public DateTime? CreatedAt { get; set; }
    public ApplicationUser User { get; set; }
}