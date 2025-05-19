using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace BudgetTracker.Pages.Account
{
    [ValidateAntiForgeryToken]
    public class LogoutModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public LogoutModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }
        public async Task<IActionResult> OnPostAsync()
        {
            await HttpContext.SignOutAsync("Cookies");
            return RedirectToPage("/Account/Login");
        }
    }
}

