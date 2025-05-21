namespace BudgetTrackerAPI.Models
{
    public class JwtSettings
    {
        public string SecretKey { get; set; }
        public string Issuer { get; set; } = "BudgetTrackerAPI";
        public string Audience { get; set; } = "BudgetTrackerFrontend";
        public int ExpiryMinutes { get; set; } = 60;
    }
}
