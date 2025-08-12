using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BudgetTracker.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;

        public LoginModel(ILogger<IndexModel> logger, IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Password { get; set; }

        [TempData]
        public string ErrorMessage {  get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var email = (Email ?? string.Empty).Trim();
            var password = (Password ?? string.Empty).Trim();

            var apiBaseUrl = _configuration["ApiBaseUrl"];
            var client = _httpClientFactory.CreateClient();

            var response = await client.PostAsJsonAsync($"{apiBaseUrl}/auth/login", new
            {
                Email = email,
                Password = password
            });

            if (!response.IsSuccessStatusCode)
            {
                ErrorMessage = "Invalid login attempt.";
                return Page();
            }

            var tokenObj = await response.Content.ReadFromJsonAsync<TokenResponse>(
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, Email),
                new("Jwt", tokenObj!.Token) // store JWT as a claim
            };

            var identity = new ClaimsIdentity(claims, "Cookies");
            await HttpContext.SignInAsync("Cookies", new ClaimsPrincipal(identity),
                new AuthenticationProperties { IsPersistent = true, ExpiresUtc = DateTime.UtcNow.AddHours(2) });

            return RedirectToPage("/Dashboard");
        }

        private class TokenResponse
        {
            public string Token { get; set; }
        }
    }
}

