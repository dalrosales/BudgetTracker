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

//// Add services to the container.
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

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
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = $"https://login.microsoftonline.com/{builder.Configuration["AzureAd:TenantId"]}";
        options.Audience = builder.Configuration["AzureAd:ClientId"];
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = $"https://login.microsoftonline.com/{builder.Configuration["AzureAd:TenantId"]}/v2.0",
            ValidateAudience = true,
            ValidAudience = builder.Configuration["AzureAd:ClientId"],
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero //Adjustment for token expiration tolerance
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

//// Enable Swagger for development
//app.UseSwagger();
//app.UseSwaggerUI();

app.UseHttpsRedirection();

// Define the GET /api/budgets endpoint
app.MapGet("/api/budgets", async (BudgetTrackerContext db) =>
{
    var budgets = await db.Budgets.ToListAsync();

    // Mapping the Budget model to BudgetDto
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
