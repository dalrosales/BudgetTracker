using Microsoft.AspNetCore.Mvc;

namespace BudgetTracker.Controllers
{
    public class BudgetsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
