using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;

namespace BudgetTracker.Pages
{
    public class DashboardModel: PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public DashboardModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }
        public void OnGet()
        {
        }
    }
}
