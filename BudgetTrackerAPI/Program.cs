using BudgetTrackerAPI.Models;
using Microsoft.EntityFrameworkCore;
using Azure.Security.KeyVault.Secrets;
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

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapGet("api/budgets", async (BudgetTrackerContext db) =>
{
    return await db.Budgets.ToListAsync();
});

app.Run();
