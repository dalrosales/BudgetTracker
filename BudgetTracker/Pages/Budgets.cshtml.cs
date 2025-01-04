using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BudgetTracker.Pages
{
    public class BudgetsModel : PageModel
    {
        private readonly ILogger<BudgetsModel> _logger; 

        public BudgetsModel(ILogger<BudgetsModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}
