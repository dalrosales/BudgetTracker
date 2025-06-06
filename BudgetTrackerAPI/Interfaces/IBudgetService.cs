﻿using BudgetTrackerAPI.Models;
using BudgetTrackerAPI.Models.DTOs;

namespace BudgetTrackerAPI.Interfaces
{
    public interface IBudgetService
    {
        Task<List<BudgetSummaryDto>> GetUserBudgets(string userId);
        Task<Budget?> GetBudgetDetails(Guid id);
        Task CreateBudget(Budget budget);
        Task UpdateBudget(Budget budget);
        Task DeleteBudget(Guid id);
    }
}
