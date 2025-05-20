using BudgetTracker.Helpers;
using BudgetTracker.Models;
using BudgetTracker.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;

namespace BudgetTracker.Pages.Budgets
{
    public class ManageModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ILogger<ManageModel> _logger;
        private readonly IConfiguration _config;
        private readonly HttpClient _httpClient;
        
        public List<BudgetSummaryViewModel> Budgets { get; set; }

        public ManageModel(IHttpClientFactory clientFactory, IConfiguration config, ILogger<ManageModel> logger)
        {
            _clientFactory = clientFactory;
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
                var response = await _httpClient.GetFromJsonAsync<List<BudgetSummaryViewModel>>(apiUrl);

                if (response != null)
                {
                    Budgets = response ?? new List<BudgetSummaryViewModel>();
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
