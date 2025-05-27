using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using System.Text.Json;

namespace BudgetTracker.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly ILogger<RegisterModel> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public RegisterModel(ILogger<RegisterModel> logger, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Password { get; set; }

        [TempData]
        public string? ErrorMessage { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var apiBaseUrl = _configuration["ApiBaseUrl"];
            var client = _httpClientFactory.CreateClient();

            var response = await client.PostAsJsonAsync($"{apiBaseUrl}/auth/register", new
            {
                this.Email,
                this.Password
            });

            if (!response.IsSuccessStatusCode)
            {
                ErrorMessage = "Registration failed.";
                return Page();
            }

            // On success, assume the API returns the JWT like login
            var json = await response.Content.ReadAsStringAsync();
            var tokenObj = JsonSerializer.Deserialize<TokenResponse>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, Email),
                new Claim("Jwt", tokenObj.Token)
            };

            var identity = new ClaimsIdentity(claims, "Cookies");
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync("Cookies", principal, new AuthenticationProperties
            {
                IsPersistent = false,
                ExpiresUtc = DateTime.UtcNow.AddHours(2)
            });

            _logger.LogInformation($"User '{Email}' logged in successfully.");

            return RedirectToPage("/Dashboard");
        }

        public class TokenResponse
        {
            public string Token { get; set; }
        }
    }
}

