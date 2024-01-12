using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BudgetTrackerAPI.Data;
using BudgetTrackerAPI.Models;
using Microsoft.AspNetCore.Authorization;

namespace BudgetTrackerAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseTagsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ExpenseTagsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ExpenseTags
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExpenseTag>>> GetExpenseTags()
        {
            return await _context.ExpenseTags.ToListAsync();
        }

        // GET: api/ExpenseTags/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExpenseTag>> GetExpenseTag(int id)
        {
            var expenseTag = await _context.ExpenseTags.FindAsync(id);

            if (expenseTag == null)
            {
                return NotFound();
            }

            return expenseTag;
        }

        // POST: api/ExpenseTags
        [HttpPost]
        public async Task<ActionResult<ExpenseTag>> PostExpenseTag(ExpenseTag expenseTag)
        {
            _context.ExpenseTags.Add(expenseTag);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetExpenseTag), new { id = expenseTag.ExpenseId }, expenseTag);
        }

        // PUT: api/ExpenseTags/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExpenseTag(int id, ExpenseTag expenseTag)
        {
            if (id != expenseTag.ExpenseId)
            {
                return BadRequest();
            }

            _context.Entry(expenseTag).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExpenseTagExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/ExpenseTags/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExpenseTag(int id)
        {
            var expenseTag = await _context.ExpenseTags.FindAsync(id);
            if (expenseTag == null)
            {
                return NotFound();
            }

            _context.ExpenseTags.Remove(expenseTag);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ExpenseTagExists(int id)
        {
            return _context.ExpenseTags.Any(e => e.ExpenseId == id);
        }
    }
}