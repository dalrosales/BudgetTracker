using Microsoft.AspNetCore.Mvc;

namespace BudgetTracker.Controllers
{
    public class ExpensesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
