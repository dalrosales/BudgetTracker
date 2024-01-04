using BudgetTrackerAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BudgetTrackerAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<ExpenseTag> ExpenseTags { get; set; }
        public DbSet<Income> Income { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<User> Users { get; set; }
    }

}
