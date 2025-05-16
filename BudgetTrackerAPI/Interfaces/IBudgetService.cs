using BudgetTrackerAPI.Models;
using BudgetTrackerAPI.Models.DTOs;

namespace BudgetTrackerAPI.Interfaces
{
    public interface IBudgetService
    {
        Task<List<BudgetDto>> GetUserBudgets(Guid userId);
        Task<Budget?> GetBudgetDetails(Guid id);
        Task CreateBudget(Budget budget);
        Task DeleteBudget(Guid id);
    }
}
