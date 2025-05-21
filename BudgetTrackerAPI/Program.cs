using BudgetTrackerAPI.Models;
using Microsoft.EntityFrameworkCore;
using Azure.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using BudgetTrackerAPI.Interfaces;
using BudgetTrackerAPI.Repositories;
using BudgetTrackerAPI.Services;
using Microsoft.AspNetCore.Identity;

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

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<BudgetTrackerContext>()
    .AddDefaultTokenProviders();

var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>() ?? new JwtSettings();
jwtSettings.SecretKey = builder.Configuration["JwtSecretKey"]; // from Azure Key Vault

builder.Services.Configure<JwtSettings>(options =>
{
    options.SecretKey = jwtSettings.SecretKey;
    options.Issuer = jwtSettings.Issuer;
    options.Audience = jwtSettings.Audience;
    options.ExpiryMinutes = jwtSettings.ExpiryMinutes;
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings.Issuer,
            ValidAudience = jwtSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(
                System.Text.Encoding.UTF8.GetBytes(jwtSettings.SecretKey)
            ),
            ClockSkew = TimeSpan.Zero
        };
    });
builder.Services.AddAuthorization();

//Add interfaces and services
builder.Services.AddScoped<IBudgetRepository, BudgetRepository>();
builder.Services.AddScoped<IBudgetService, BudgetService>();
builder.Services.AddScoped<AuthService>();

builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
