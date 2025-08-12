using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
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
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [BindProperty]
        [Required]
        [MinLength(6)]
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

            var email = (Email ?? string.Empty).Trim();
            var password = (Password ?? string.Empty).Trim();

            var apiBaseUrl = _configuration["ApiBaseUrl"];
            var client = _httpClientFactory.CreateClient();

            var response = await client.PostAsJsonAsync($"{apiBaseUrl}/auth/register", new
            {
                Email = email,
                Password = password
            });

            if (!response.IsSuccessStatusCode)
            {
                ErrorMessage = "Registration failed.";
                return Page();
            }

            var tokenObj = await response.Content.ReadFromJsonAsync<TokenResponse>(
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, Email),
                new Claim("Jwt", tokenObj!.Token)
            };

            var identity = new ClaimsIdentity(claims, "Cookies");

            await HttpContext.SignInAsync("Cookies", new ClaimsPrincipal(identity),
                new AuthenticationProperties { IsPersistent = true, ExpiresUtc = DateTime.UtcNow.AddHours(2) });

            return RedirectToPage("/Dashboard");
        }

        public class TokenResponse
        {
            public string Token { get; set; }
        }
    }
}

