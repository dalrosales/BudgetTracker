using Microsoft.AspNetCore.Mvc;

namespace BudgetTracker.Controllers
{
    public class TransactionsController : Controller
    {
        public IActionResult Transactions()
        {
            return View();
        }
    }
}
