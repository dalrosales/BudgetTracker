using Microsoft.AspNetCore.Mvc;

namespace BudgetTracker.Controllers
{
    public class SettingsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
