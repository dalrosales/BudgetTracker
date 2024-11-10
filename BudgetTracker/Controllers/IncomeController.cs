using Microsoft.AspNetCore.Mvc;

namespace BudgetTracker.Controllers
{
    public class IncomeController : Controller
    {
        public IActionResult Income()
        {
            return View();
        }
    }
}
