using ECommerceAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// E-Commerce database setup using the connection string from my appsettings.json
builder.Services.AddDbContext<ECommerceDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add controllers
builder.Services.AddControllers();

// Set up JWT Authentication
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(options =>
    {
        string? secretKey = builder.Configuration["JwtSettings:SecretKey"];

        // Ensure SecretKey is provided in my configuration
        if (string.IsNullOrEmpty(secretKey))
        {
            throw new InvalidOperationException("JWT Secret Key is not configured properly.");
        }

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidIssuer = builder.Configuration["JwtSettings:ValidIssuer"],
            ValidAudience = builder.Configuration["JwtSettings:ValidAudience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
        };
    });

// Set up CORS 
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// Configure Swagger for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Enable CORS
app.UseCors("AllowAll");

app.UseAuthentication(); // Add authentication middleware
app.UseAuthorization();  // Add authorization middleware

app.MapControllers();

app.Run();
