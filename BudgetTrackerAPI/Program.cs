using BudgetTrackerAPI.Models;
using BudgetTrackerAPI.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using Azure.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Load Key Vault into Configuration
builder.Configuration.AddAzureKeyVault(
    new Uri(builder.Configuration["VaultKey"]),
    new DefaultAzureCredential()
);

// Get connection string from Key Vault
var connectionString = builder.Configuration["BudgetTrackerAPISecret1"];
if (string.IsNullOrWhiteSpace(connectionString))
{
    throw new InvalidOperationException("Connection string not found in Key Vault.");
}

builder.Services.AddDbContext<BudgetTrackerContext>(options =>
    options.UseSqlServer(connectionString)
);

// Add authentication and authorization with Azure AD
var tenantId = builder.Configuration["AzureAd:TenantId"];
var clientId = builder.Configuration["AzureAd:ClientId"];

if (string.IsNullOrEmpty(tenantId) || string.IsNullOrEmpty(clientId))
{
    throw new InvalidOperationException("Missing AzureAd:TenantId or AzureAd:ClientId in configuration.");
}

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = $"https://login.microsoftonline.com/{tenantId}";
        options.Audience = clientId;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = $"https://login.microsoftonline.com/{tenantId}/v2.0",
            ValidateAudience = true,
            ValidAudience = clientId,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

// Define the GET /api/budgets endpoint
app.MapGet("/api/budgets", async (BudgetTrackerContext db) =>
{
    var budgets = await db.Budgets.ToListAsync();

    var budgetDtos = budgets.Select(b => new BudgetDto
    {
        BudgetId = b.BudgetId,
        UserId = b.UserId,
        Name = b.Name,
        Amount = b.Amount,
        Period = b.Period,
        StartDate = b.StartDate,
        EndDate = b.EndDate,
        CreatedAt = b.CreatedAt
    }).ToList();

    return Results.Ok(budgetDtos);
}).RequireAuthorization();

app.Run();
