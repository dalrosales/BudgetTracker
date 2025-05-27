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
            var apiBaseUrl = _configuration["ApiBaseUrl"];
            var client = _httpClientFactory.CreateClient();

            var response = await client.PostAsJsonAsync($"{apiBaseUrl}/auth/login", new
            {
                this.Email,
                this.Password
            });

            if (!response.IsSuccessStatusCode)
            {
                ErrorMessage = "Invalid login attempt.";
                return Page();
            }

            var json = await response.Content.ReadAsStringAsync();
            var tokenObj = JsonSerializer.Deserialize<TokenResponse>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, Email),
                new Claim("Jwt", tokenObj.Token) // store JWT as a claim
            };

            var identity = new ClaimsIdentity(claims, "Cookies");
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync("Cookies", principal, new AuthenticationProperties
            {
                IsPersistent = true, //Enables "Remember Me"
                ExpiresUtc = DateTime.UtcNow.AddHours(2)
            });

            _logger.LogInformation($"User '{Email}' logged in successfully.");

            return RedirectToPage("/Dashboard");
        }

        private class TokenResponse
        {
            public string Token { get; set; }
        }
    }
}

