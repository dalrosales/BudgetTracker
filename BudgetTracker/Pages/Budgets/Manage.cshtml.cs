using BudgetTracker.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BudgetTracker.Pages.Budgets
{
    public class ManageModel : PageModel
    {
        private readonly ILogger<ManageModel> _logger;
        private readonly IConfiguration _config;
        private readonly HttpClient _httpClient;

        public List<BudgetDto> Budgets { get; set; } = new();

        public ManageModel(ILogger<ManageModel> logger, IConfiguration config, IHttpClientFactory clientFactory)
        {
            _logger = logger;
            _config = config;
            _httpClient = clientFactory.CreateClient();
        }

        public async Task OnGetAsync()
        {
            string apiUrl = _config["ApiBaseUrl"];
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
