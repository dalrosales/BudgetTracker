using Microsoft.AspNetCore.Mvc;

namespace BudgetTracker.Controllers
{
    public class BudgetsController : Controller
    {
        public IActionResult Budgets()
        {
            return View();
        }
    }
}
