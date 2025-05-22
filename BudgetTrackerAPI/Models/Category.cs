#nullable disable

namespace BudgetTrackerAPI.Models;

public partial class Category
{
    public Guid CategoryId { get; set; }

    public string UserId { get; set; }

    public Guid BudgetId { get; set; }

    public string Name { get; set; }

    public decimal BudgetedAmount { get; set; }

    public decimal ActualAmount { get; set; } = 0;

    public DateTime? CreatedAt { get; set; }

    public virtual Budget Budget { get; set; }

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}