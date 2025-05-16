using BudgetTrackerAPI.Models;

namespace BudgetTrackerAPI.Interfaces
{
    public interface IBudgetRepository
    {
        Task<List<Budget>> GetBudgetsByUserIdAsync(Guid userId);
        Task<Budget?> GetBudgetAsync(Guid id);
        Task AddBudgetAsync(Budget budget);
        Task DeleteBudgetAsync(Guid id);
        Task SaveChangesAsync();
    }
}
