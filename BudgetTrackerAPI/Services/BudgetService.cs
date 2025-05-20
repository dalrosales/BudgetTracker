using BudgetTrackerAPI.Interfaces;
using BudgetTrackerAPI.Models;
using BudgetTrackerAPI.Models.DTOs;

namespace BudgetTrackerAPI.Services
{
    public class BudgetService : IBudgetService
    {
        private readonly IBudgetRepository _repo;

        public BudgetService(IBudgetRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<BudgetSummaryDto>> GetUserBudgets(string userId)
        {
            var budgets = await _repo.GetBudgetsByUserIdAsync(userId);

            return budgets.Select(b => new BudgetSummaryDto
            {
                BudgetId = b.BudgetId,
                Name = b.Name,
                BudgetedAmount = b.BudgetedAmount,
                ActualAmount = b.ActualAmount,
                Period = b.Period,
                StartDate = b.StartDate,
                EndDate = b.EndDate,
                CreatedAt = b.CreatedAt
            }).ToList();
        }

        public async Task<Budget?> GetBudgetDetails(Guid id)
        {
            return await _repo.GetBudgetAsync(id);
        }

        public async Task CreateBudget(Budget budget)
        {
            await _repo.AddBudgetAsync(budget);
            await _repo.SaveChangesAsync();
        }

        public async Task UpdateBudget(Budget budget)
        {
            await _repo.UpdateBudgetAsync(budget);
            await _repo.SaveChangesAsync();
        }

        public async Task DeleteBudget(Guid id)
        {
            await _repo.DeleteBudgetAsync(id);
            await _repo.SaveChangesAsync();
        }
    }
}
