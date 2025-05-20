using BudgetTrackerAPI.Interfaces;
using BudgetTrackerAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BudgetTrackerAPI.Repositories
{
    public class BudgetRepository : IBudgetRepository
    {
        private readonly BudgetTrackerContext _context;
        
        public BudgetRepository(BudgetTrackerContext context)
        {
            _context = context;
        }

        public async Task<List<Budget>> GetBudgetsByUserIdAsync(string userId)
        {
            return await _context.Budgets
                .Where(b => b.UserId == userId)
                .ToListAsync();
        }

        public async Task<Budget?> GetBudgetAsync(Guid id)
        {
            return await _context.Budgets.FindAsync(id);
        }

        public async Task AddBudgetAsync(Budget budget)
        {
            await _context.Budgets.AddAsync(budget);
        }

        public Task UpdateBudgetAsync(Budget budget)
        {
            _context.Budgets.Update(budget);
            return Task.CompletedTask;
        }

        public async Task DeleteBudgetAsync(Guid id)
        {
            var budget = await _context.Budgets.FindAsync(id);

            if (budget != null)
            {
                _context.Budgets.Remove(budget);
            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
