using BudgetTrackerAPI.Interfaces;
using BudgetTrackerAPI.Models;
using BudgetTrackerAPI.Models.DTOs;
using BudgetTrackerAPI.Repositories;

namespace BudgetTrackerAPI.Services
{
    public class BudgetService : IBudgetService
    {
        private readonly IBudgetRepository _repo;

        public BudgetService(IBudgetRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<BudgetDto>> GetUserBudgets(Guid userId)
        {
            var budgets = await _repo.GetBudgetsByUserIdAsync(userId);

            return budgets.Select(b => new BudgetDto
            {
                BudgetId = b.BudgetId,
                Name = b.Name,
                Amount = b.Amount,
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

        public async Task DeleteBudget(Guid id)
        {
            await _repo.DeleteBudgetAsync(id);
            await _repo.SaveChangesAsync();
        }
    }
}
