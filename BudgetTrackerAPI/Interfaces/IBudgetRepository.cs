using BudgetTrackerAPI.Models;

namespace BudgetTrackerAPI.Interfaces
{
    public interface IBudgetRepository
    {
        Task<List<Budget>> GetBudgetsByUserIdAsync(string userId);
        Task<Budget?> GetBudgetAsync(Guid id);
        Task AddBudgetAsync(Budget budget);
        Task UpdateBudgetAsync(Budget budget);
        Task DeleteBudgetAsync(Guid id);
        Task SaveChangesAsync();
    }
}
