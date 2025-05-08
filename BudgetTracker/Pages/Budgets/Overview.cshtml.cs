using BudgetTracker.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

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

        public async Task OnGetAsync()
        {
            string apiUrl = _config["ApiSettings:BaseUrl"];
            var response = await _httpClient.GetFromJsonAsync<List<BudgetDto>>($"{apiUrl}/api/budgets");

            if (response != null)
            {
                Budgets = response;
            }
            else
            {
                _logger.LogError("Failed to retrieve budgets from API.");
            }
        }
    }
}
