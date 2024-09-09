using Microsoft.AspNetCore.Mvc;

namespace BudgetTracker.Controllers
{
    public class IncomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
