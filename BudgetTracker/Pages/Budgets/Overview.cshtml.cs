using BudgetTracker.Helpers;
using BudgetTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;

namespace BudgetTracker.Pages.Budgets
{
    public class OverviewModel : PageModel
    {
        private readonly ILogger<OverviewModel> _logger;
        private readonly IConfiguration _config;
        private readonly HttpClient _httpClient;

        public List<BudgetDto> Budgets { get; set; } = new();

        public OverviewModel(ILogger<OverviewModel> logger, IConfiguration config, IHttpClientFactory clientFactory)
        {
            _logger = logger;
            _config = config;
            _httpClient = clientFactory.CreateClient();
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var token = User.GetJwtToken();
            if (string.IsNullOrEmpty(token))
            {
                _logger.LogWarning("JWT token not found in user claims.");
                return RedirectToPage("/Account/Login");
            }

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            try
            {
                string apiUrl = $"{_config["ApiBaseUrl"]}/budgets";
                var response = await _httpClient.GetFromJsonAsync<List<BudgetDto>>(apiUrl);

                if (response != null)
                {
                    Budgets = response;
                }
                else
                {
                    _logger.LogError("Failed to load budgets.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving budgets.");
            }

            return Page();
        }
    }
}
