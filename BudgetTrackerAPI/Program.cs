using BudgetTrackerAPI.Models;
using BudgetTrackerAPI.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using Azure.Identity;

var builder = WebApplication.CreateBuilder(args);

// Load Key Vault into Configuration
builder.Configuration.AddAzureKeyVault(
    new Uri(builder.Configuration["VaultKey"]),
    new DefaultAzureCredential()
);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Safely get the API Key
var apiKey = builder.Configuration["BudgetTrackerAPIKey1"];
if (string.IsNullOrWhiteSpace(apiKey))
{
    throw new InvalidOperationException("API key not found in Key Vault.");
}

// Safely get the connection string from configuration
var connectionString = builder.Configuration["BudgetTrackerAPISecret1"];
if (string.IsNullOrWhiteSpace(connectionString))
{
    throw new InvalidOperationException("Connection string not found in Key Vault.");
}

builder.Services.AddDbContext<BudgetTrackerContext>(options =>
    options.UseSqlServer(connectionString)
);

var app = builder.Build();

// Require API Key
app.Use(async (context, next) =>
{
    // Only enforce API key check in non-development environments
    if (!app.Environment.IsDevelopment())
    {
        var providedApiKey = context.Request.Headers["x-api-key"].FirstOrDefault();

        if (string.IsNullOrWhiteSpace(providedApiKey) || providedApiKey != apiKey)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Unauthorized: Missing or invalid API key.");
            return;
        }
    }

    await next();
});

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

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
});
app.Run();
