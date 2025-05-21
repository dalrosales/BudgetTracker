using BudgetTrackerAPI.Interfaces;
using BudgetTrackerAPI.Models;
using BudgetTrackerAPI.Models.DTOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BudgetTrackerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BudgetsController : ControllerBase
    {
        private readonly IBudgetService _budgetService;

        public BudgetsController(IBudgetService budgetService)
        {
            _budgetService = budgetService;
        }

        [HttpGet]
        public async Task<IActionResult> GetBudgets()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("Invalid token or user ID.");
            }

            var budgets = await _budgetService.GetUserBudgets(userId);
            return Ok(budgets);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBudget([FromBody] CreateBudgetDto createDto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("Invalid token or user ID.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var budget = new Budget
            {
                BudgetId = Guid.NewGuid(),
                UserId = userId,
                Name = createDto.Name,
                BudgetedAmount = createDto.BudgetedAmount,
                ActualAmount = createDto.ActualAmount,
                Period = createDto.Period,
                StartDate = createDto.StartDate,
                EndDate = createDto.EndDate,
                CreatedAt = DateTime.UtcNow
            };

            await _budgetService.CreateBudget(budget);

            return CreatedAtAction(nameof(GetBudgetById), new { id = budget.BudgetId }, budget);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBudgetById(Guid id)
        {
            var budget = await _budgetService.GetBudgetDetails(id);

            if (budget == null)
                return NotFound();

            // TODO: check if the budget.UserId matches the logged in user to enforce ownership

            return Ok(budget);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBudget(Guid id, [FromBody] UpdateBudgetDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingBudget = await _budgetService.GetBudgetDetails(id);
            if (existingBudget == null)
                return NotFound();

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId) || existingBudget.UserId != userId)
                return Unauthorized("You are not authorized to update this budget.");

            // Update properties
            existingBudget.Name = updateDto.Name;
            existingBudget.BudgetedAmount = updateDto.BudgetedAmount;
            existingBudget.ActualAmount = updateDto.ActualAmount;
            existingBudget.Period = updateDto.Period;
            existingBudget.StartDate = updateDto.StartDate;
            existingBudget.EndDate = updateDto.EndDate;

            await _budgetService.UpdateBudget(existingBudget);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBudget(Guid id)
        {
            var existingBudget = await _budgetService.GetBudgetDetails(id);
            if (existingBudget == null)
                return NotFound();

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId) || existingBudget.UserId != userId)
                return Unauthorized("You are not authorized to delete this budget.");

            await _budgetService.DeleteBudget(id);

            return NoContent();
        }
    }
}
