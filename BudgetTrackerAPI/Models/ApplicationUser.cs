using Microsoft.AspNetCore.Identity;

namespace BudgetTrackerAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Budget> Budgets { get; set; }
    }
}
